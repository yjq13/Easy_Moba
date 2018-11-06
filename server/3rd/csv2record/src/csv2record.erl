-module(csv2record).
-author("ltb<lintingbin31@gmail.com>").

-export([main/1, generate/1, generate/2, get_opt/2, camel_to_under/1]).

-define(CSV_IGNORE_FILE, "csv.ignore"). % 该文件放在csv_lib目录下

main(Params) ->
  case Params of
    [Path] ->
      generate(Path);
    [Path, OptStr] ->
      KeyValueStr = re:split(OptStr, ";", [{return,list}, trim]),
      KeyValuePair = [re:split(Str, "=", [{return,list}, trim]) || Str <- KeyValueStr],
      OptList = [{list_to_atom(KeyStr), ValueStr} || [KeyStr, ValueStr] <- KeyValuePair],
      generate(Path, OptList);
    _ ->
      io:format("Error param count~n"),
      halt(1)
  end.

generate(Path) ->
  generate(Path, []).

generate(Path, Opt) ->
  case filelib:is_dir(Path) of
    true ->
      generate_files(Path, Opt);
    false ->
      generate_file(Path, undefined, Opt)
  end.

generate_files(Dir, Opt) ->
  Pid = self(),
  AllCsvFiles = get_all_csv_files(Dir),
  [proc_lib:spawn_link(fun() -> 
    case catch generate_file(File, Pid, Opt) of
      ok -> 
        ok;
      Error ->
        io:format("[~p] error cfged file is ~p ~n", [?MODULE, File]),
        Pid !  Error
    end
  end) || File <- AllCsvFiles],
  loop(length(AllCsvFiles)).

get_all_csv_files(Dir) ->
  AllCsvFiles   = filelib:fold_files(Dir, ".csv$", false, fun(File, Acc) -> [File| Acc] end, []),
  CsvIgnoreFile = lists:concat([string:sub_string(Dir, 1, string:rchr(Dir,$/)), ?CSV_IGNORE_FILE]),
  case file:read_file(CsvIgnoreFile) of
    {ok, BinData} ->
      IgnoreData    = binary_to_list(BinData),
      IgnoredFiles  = [lists:concat([Dir, "/", F]) || F <- re:split(IgnoreData, "\r\n|\n|\r|,", [{return, list}, trim]), string:sub_string(F, 1, 1) =/= "%"],
      [F || F <- AllCsvFiles, not lists:member(F, IgnoredFiles)];
    _ ->
      AllCsvFiles
  end.

loop(0) -> ok;
loop(N) ->
  receive
    {generate_success, File} ->
      io:format("[~p] ~p generate success~n", [?MODULE, File]),
      loop(N - 1);
    Err ->
      exit(Err)
  end.

generate_file(Path, Pid, Opt) ->
  check_and_make_dir(Opt),
  HrlDir = get_opt(hrl_dir, Opt),
  EbinDir = get_opt(ebin_dir, Opt),
  {Data, AttrList} = csv_parser:parse(Path),
  CamelBaseName = filename:rootname(filename:basename(Path)),
  UnderBaseName = camel_to_under(CamelBaseName),
  ErlFile = file_generator:generate(UnderBaseName, Data, AttrList, Opt),
  case compile:file(ErlFile, [{i, HrlDir}, {outdir, EbinDir}, verbose, report_errors, report_warnings]) of
    {ok, _} -> ok;
    Error ->
      throw(Error)
  end,
  case Pid =:= undefined of
    true ->
      ok;
    false ->
      Pid ! {generate_success, Path}
  end,
  ok.

check_and_make_dir(Opt) ->
  HrlDir = get_opt(hrl_dir, Opt),
  SrcDir = get_opt(src_dir, Opt),
  EbinDir = get_opt(ebin_dir, Opt),
  [check_and_make_dir_proc(X) || X <- [HrlDir, SrcDir, EbinDir]].

check_and_make_dir_proc(Dir) ->
  case filelib:is_dir(Dir) of
    true ->
      ok;
    false ->
      file:make_dir(Dir)
  end.

get_opt(Key, Opt) ->
  case lists:keyfind(Key, 1, Opt) of
    false ->
      get_default_opt(Key);
    {Key, Value} ->
      Value
  end.

get_default_opt(hrl_dir) ->
  "csv_hrl_dir";
get_default_opt(src_dir) ->
  "csv_src_dir";
get_default_opt(ebin_dir) ->
  "ebin";
get_default_opt(record_prefix) ->
  "csv".

camel_to_under(Str) ->
  CamelStr = conti_upper_to_camel(Str, false, []),
  Chars = camel_add_split(CamelStr, []),
  case Chars of
    ["\_" | Rest] ->
      Rest;
    Other ->
      Other
  end.

is_upper(C) ->
  (is_integer(C) andalso $A =< C andalso C =< $Z) orelse
    (is_integer(C) andalso 16#C0 =< C andalso C =< 16#D6) orelse
    (is_integer(C) andalso 16#D8 =< C andalso C =< 16#DE).

to_under_char(C) ->
  case is_upper(C) of
    true ->
      C + 32;
    false ->
      C
  end.

conti_upper_to_camel([], _, Str) ->
  lists:reverse(Str);
conti_upper_to_camel([$P, $R | Left], _, Str) ->
  conti_upper_to_camel(Left, false, [$r, $P | Str]);
conti_upper_to_camel([C | Left], IsPrevUp, Str) ->
  case is_upper(C) of
    true when IsPrevUp ->  conti_upper_to_camel(Left, true, [C + 32 | Str]);
    IsUp  ->  conti_upper_to_camel(Left, IsUp, [C | Str])
  end.

camel_add_split([], Str) ->
  lists:reverse(Str);
camel_add_split([C | Left], Str) ->
  case is_upper(C) of 
    true  ->  camel_add_split(Left, [to_under_char(C), "_" | Str]);
    false ->  camel_add_split(Left, [C | Str])
  end.