-module(file_generator).
-author("ltb<lintingbin31@gmail.com>").

-export([generate/4]).

-include("record.hrl").

-define(ERR_CSV_CFG_NOT_FOUND, err_csv_cfg_not_found).
-define(ERR_INVALID_CFG_KEY, err_invalid_cfg_key).


generate(BaseName, Data, Attrs, Opt) ->
  BaseNameLower = string:to_lower(BaseName),
  Prefix = csv2record:get_opt(record_prefix, Opt),
  RecordName = Prefix ++ "_" ++ BaseNameLower,
  generate_hrl_file(RecordName, Attrs, Opt),
  generate_erl_file(RecordName, Data, Attrs, Opt).

generate_hrl_file(RecordName, Attrs, Opt) ->
  FileName = RecordName ++ ".hrl",
  Dir = csv2record:get_opt(hrl_dir, Opt),
  Path = filename:join([Dir, FileName]),
  Str = build_hrl_file_str(RecordName, Attrs),
  {ok, Fd} = file:open(Path, [write]),
  io:format(Fd, Str, []),
  file:close(Fd).

build_hrl_file_str(RecordName, Attrs) ->
  Macro = RecordName ++ "_hrl_file",
  UpperMacro = string:to_upper(Macro),
  Fields = build_record_field_str(Attrs, []),
  "-ifndef(" ++ UpperMacro ++ ").\n"
  "-define(" ++ UpperMacro ++ ", " ++ Macro ++ ").\n\n"
  "-record(" ++ RecordName ++ ", {" ++ Fields ++ "}).\n\n"
  "-endif.\n\n".

build_record_field_str([], Fields) -> 
  string:join(lists:reverse(Fields), ", ");
build_record_field_str([#column_attr{name = Name, type = {_, Default}}| Tail], Fields) ->
  Field = lists:concat([Name, " = ", Default]),
  build_record_field_str(Tail, [Field| Fields]).

generate_erl_file(RecordName, Data, Attrs, Opt) ->
  FileName = RecordName ++ ".erl",
  Dir = csv2record:get_opt(src_dir, Opt),
  Path = filename:join([Dir, FileName]),
  Str = build_erl_file_str(RecordName, Attrs, Data),
  {ok, Fd} = file:open(Path, [write]),
  io:format(Fd, "~s", [Str]),
  file:close(Fd),
  Path.

build_erl_file_str(RecordName, Attrs, Data) ->
  {GetFunStr_, AllKey} = build_get_fun_str(Data, Attrs, [], [], RecordName),
  GetFunStr        = build_export_get_fun_str(),
  GetFieldFunStr   = build_get_field_fun_str(),
  GetFieldWithErrCodeFunStr   = build_get_field_with_errcode_fun_str(RecordName, Attrs, []),
  GetAllKeysFunStr = build_get_all_key_fun_str(AllKey),
  {GetIndexFunStr, IndexFunName} = build_index_fun_strs(),
  DumpFunStr       = build_dump_fun_str(),
  FileStr =
  "-module(" ++ RecordName ++ ").\n\n"
  "-include(\"" ++ RecordName ++ ".hrl\").\n\n"
  "-export([get/1, get/2, get_all_keys/0, get_max_key/0, get_field/2, get_field/3" ++ IndexFunName ++ ",check_valid_key/1, check_valid_key/2, dump/0]).\n\n" ++ 
  GetFunStr ++
  GetFunStr_ ++ 
  GetFieldFunStr ++
  GetFieldWithErrCodeFunStr ++
  GetAllKeysFunStr ++ 
  GetIndexFunStr ++ 
  DumpFunStr,
  format_file_str(FileStr).

% 删除最后的换行符
format_file_str(FileStr) ->
  FileStr.

build_export_get_fun_str() ->
  "get(Key) ->\n"
  "  case get_(Key) of\n"
  "    Cfg when is_tuple(Cfg) ->\n"
  "      {ok, Cfg};\n"
  "    Error ->\n"
  "      lager:error(\"[~p]csv cfg not found: ~p\", [?MODULE, Key]),\n"
  "      Error\n"
  "  end.\n\n"
  "get(Key, nowarn) ->\n"
  "  case get_(Key) of\n"
  "    Cfg when is_tuple(Cfg) ->\n"
  "      {ok, Cfg};\n"
  "    Error ->\n"
  "      Error\n"
  "  end;\n"
  "get(Key, ErrCode) ->\n"
  "  case get_(Key) of\n"
  "    Cfg when is_tuple(Cfg) ->\n"
  "      {ok, Cfg};\n"
  "    _Err ->\n"
  "      lager:error(\"[~p]csv cfg not found: ~p\", [?MODULE, Key]),\n"
  "      ErrCode\n"
  "  end.\n\n".

build_get_field_fun_str() ->
  "\n\n" ++
  "get_field(Key, Field) ->\n" ++
  lists:concat(["  get_field(Key, Field, ", ?ERR_CSV_CFG_NOT_FOUND, ")."]).

build_get_field_with_errcode_fun_str(Name, [#column_attr{name = Field}], Funs) ->
  "\n\n" ++ lists:reverse(Funs) ++
  lists:concat(["get_field(Key, ", Field]) ++ ", ErrCode) ->\n" ++
  "  case " ++ Name ++ ":get(Key, ErrCode) of\n" ++
  "    {ok, Cfg} -> {ok, Cfg#" ++ lists:concat([Name, ".", Field, "};\n"]) ++
  "    _Err      -> ErrCode\n" ++
  "  end.";
build_get_field_with_errcode_fun_str(Name, [#column_attr{name = Field} | Left], Funs) ->
  FunStr  = lists:concat(["get_field(Key, ", Field]) ++ ", ErrCode) ->\n" ++
  "  case " ++ Name ++ ":get(Key) of\n" ++
  "    {ok, Cfg} -> {ok, Cfg#" ++ lists:concat([Name, ".", Field, "};\n"]) ++
  "    _Err -> ErrCode\n" ++
  "  end;\n",
  build_get_field_with_errcode_fun_str(Name, Left, [FunStr | Funs]).
  
build_get_fun_str([], _, [], Keys, _) ->
  {"\nget_(_) ->\n"
  "  " ++ atom_to_list(?ERR_CSV_CFG_NOT_FOUND) ++ ".", lists:reverse(Keys)};
build_get_fun_str([], _, Funs, Keys, _) ->
  FunStr = string:join(lists:reverse(Funs), ";\n"),
  GetFunStr = FunStr ++ ";\nget_(_) ->\n"
  "  " ++ atom_to_list(?ERR_CSV_CFG_NOT_FOUND) ++ ".",
  {GetFunStr, lists:reverse(Keys)};
build_get_fun_str([Line| Tail], Attrs, Funs, Keys, Name) ->
  {Key, Index, ValueStr} = build_value_str(Line, Attrs, [], [], []),
  FunStr = "get_(" ++ Key ++ ") -> \n  #" ++ Name ++ ValueStr,
  set_index(Index, Key),
  build_get_fun_str(Tail, Attrs, [FunStr| Funs], [Key| Keys], Name).

build_value_str(Line, Field, Done, KeyList, Index) when Line =:= []; Field =:= [] ->
  Str = string:join(lists:reverse(Done), ", "),
  RealKey = 
    case length(KeyList) =:= 1 of
      true ->
        hd(KeyList);
      false ->
        KeyStr = string:join(lists:reverse(KeyList), ", "),
        lists:concat(["{", KeyStr, "}"])
    end,
  {RealKey, Index, "{" ++ Str ++ "}"};
build_value_str([Value| VTail], [Attr| CTail], Done, Key, Index) ->
  #column_attr{
    name = Name, 
    is_key = IsKey, 
    is_index = IsIndex, 
    is_array = IsArray,
    type = Type
  } = Attr,
  RealValue =  build_array_str(IsArray, Type, Value, []),
  NewOne = lists:concat([Name, " = ", RealValue]),
  NewKey = case IsKey of true -> [lists:concat([RealValue])| Key]; false -> Key end,
  NewIndex = case IsIndex of true -> [{IsArray, Name, Value}| Index]; false -> Index end,
  build_value_str(VTail, CTail, [NewOne| Done], NewKey, NewIndex).

build_array_str(false, {integer, _}, Value, []) ->
  Value;
build_array_str(true, {integer, _}, Value, []) ->
  "[" ++ string:join(Value, ",") ++ "]";
build_array_str(false, _, Value, []) ->
  Value;
build_array_str(true, _, Value, []) ->
  build_array_str_(Value, []).

build_array_str_([], List) ->
  ListStr = string:join(lists:reverse(List), ", "),
  "[" ++ ListStr ++ "]";
build_array_str_([X| Tail], List) ->
  build_array_str_(Tail, [io_lib:format("~w", [X])| List]).

set_index([],  _) -> ok;
set_index([{IsArray, IndexName, IndexValue}| Tail], Key) ->
  Dict = case get({dict, IndexName}) of
           undefined ->
             dict:new();
           D ->
             D
         end,
  Dict1 = set_index_one(Dict, IsArray, IndexValue, Key),
  put({dict, IndexName}, Dict1),
  set_index(Tail, Key).

set_index_one(Dict, false, IndexValue, Key) ->
  dict:append(IndexValue, Key, Dict);

set_index_one(Dict, true, [], _Key) ->
  Dict;
set_index_one(Dict, true, [IndexValue | Left], Key) ->
  Dict1 = dict:append(IndexValue, Key, Dict),
  set_index_one(Dict1, true, Left, Key).
%%set_index_one(Dict, IndexValue, Key) ->
%%  Dict1 = dict:append(IndexValue, Key, Dict),
%%  Dict1.


build_get_all_key_fun_str(AllKey) ->
  KeysStr = string:join(AllKey, ", "),
  MaxKey  = case AllKey of
    []  ->
      "undefined";
    _   ->
      lists:last(AllKey)
  end,
  "\n\nget_all_keys() -> \n  [" ++ KeysStr ++ "]." ++
  lists:concat(["\n\ncheck_valid_key(Key) -> \n  check_valid_key(Key, ", ?ERR_INVALID_CFG_KEY, ")."]) ++
  "\n\ncheck_valid_key(Key, ErrCode) -> \n  case lists:member(Key, get_all_keys()) of \n" ++
    "    true  -> ok;\n" ++
    "    false ->\n" ++
    "      lager:debug(\"[~p] check valid key: ~p error: ~p\", [?MODULE, Key, ErrCode]),\n"
    "      ErrCode\n" ++
    "  end." ++
  "\n\nget_max_key() -> \n  " ++ MaxKey ++ ".".

build_index_fun_strs() ->
  DictList = [begin
    erase({dict, IndexName}),
    {IndexName, dict:to_list(Dict)} 
  end || {{dict, IndexName}, Dict} <- get()],
  build_index_fun_strs(DictList, [], []).

build_index_fun_strs([], FunStrList, FunNameList) ->
  FunNameStr = 
    case FunNameList of
      [] ->
        FunStr = "",
        "";
      _ ->
        FunStr = string:join(FunStrList, ""),
        ", " ++ string:join(FunNameList, ", ")
    end,
  {FunStr, FunNameStr};
build_index_fun_strs([{IndexName, DictList}| Tail], FunStrList, FunNameList) ->
  FunName = lists:concat(["get_", IndexName, "_keys"]),
  SortDictList = lists:sort(DictList),
  FunStr = build_index_fun_str(SortDictList, FunName, []),
  AllIndexFunName = lists:concat(["get_all_", IndexName]),
  AllIndex = [lists:concat([Key]) || {Key, _} <- SortDictList],
  AllIndexStr = string:join(AllIndex, ", "),
  AllIndexFunStr = lists:concat(["\n\n", AllIndexFunName, "() -> \n  [", AllIndexStr, "]."]),
  IndexFunStr = FunStr ++ AllIndexFunStr,
  MaxIndexStr    = case AllIndex of
    [] ->
      "undefined";
    _  ->
      lists:last(AllIndex)
  end,
  MaxIndexFunName   = lists:concat(["get_max_", IndexName]),
  MaxIndexFunStr    = lists:concat(["\n", MaxIndexFunName, "() -> \n  ", MaxIndexStr, "."]),
  CheckIndexFunName = lists:concat(["check_valid_", IndexName]),
  CheckIndexFunStr  = "\n\n" ++ CheckIndexFunName ++ "(Key) -> \n" ++
    "  " ++ CheckIndexFunName ++ "(Key, " ++ atom_to_list(?ERR_INVALID_CFG_KEY) ++ ")." ++
    "\n\n" ++ CheckIndexFunName ++ "(Key, ErrCode) -> \n" ++
    "  case lists:member(Key, "++ AllIndexFunName ++"()) of \n" ++
    "    true  -> ok;\n" ++
    "    false -> ErrCode\n" ++
    "  end.",
  build_index_fun_strs(Tail, [IndexFunStr, MaxIndexFunStr, CheckIndexFunStr | FunStrList], [FunName ++ "/1, " ++ AllIndexFunName ++ "/0, " ++ MaxIndexFunName ++ "/0, " ++ CheckIndexFunName ++ "/1, " ++ CheckIndexFunName ++ "/2" | FunNameList]).

build_index_fun_str([], FunName, FunStrList) ->
  FunStr = string:join(FunStrList, ";\n"),
  "\n\n" ++ FunStr ++ ";\n" ++ FunName ++ "(_) ->\n  [].";
build_index_fun_str([{Key, List}| Tail], FunName, FunStrList) ->
  ListStr = string:join(List, ", "),
  FunStr = lists:concat([FunName, "(", Key, ") -> \n  [", ListStr, "]"]),
  build_index_fun_str(Tail, FunName, [FunStr| FunStrList]).

build_dump_fun_str() ->
  "\n\ndump() -> \n  Keys    = get_all_keys(),\n  [io:format(\"~p~n\", [get_(Key)]) || Key <- Keys],\n  ok.".