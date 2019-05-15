-module(tools).

-include("com_def.hrl").
-include("ets.hrl").

%% =================================================================
-export([child_spec/3, child_spec/4]).
-export([safe_apply/2, safe_apply/3]).
-export([is_err_code/1, fetch_err_code/1]).

%% -----------------------------------------------------------------
-export([index_of/2, cut_atom_head/2, if_then/3]).

%% -----------------------------------------------------------------
-export([get_uin/2, no_login_check/1]).

%% =================================================================
child_spec(Mod, Function, Param) ->
  child_spec(Mod, Mod, Function, Param).

child_spec(Id, Mod, Function, Param) ->
  {Id, 
    {Mod, Function, Param},
    permanent, 1000, worker, [Mod]
  }.

%% safe apply
-spec safe_apply(Fun::fun(), Parmas:: [any()]) -> any().
safe_apply(Fun, Args) ->
  try
    erlang:apply(Fun, Args)
  catch
    error:{badmatch, {error, Err}} when is_atom(Err) -> Err;
    error:{badmatch, Err}:Stacktrace when is_atom(Err)->
      ?DEBUG("[~p] safe_apply, Stacktrace: ~s", [?MODULE, lager:pr_stacktrace(Stacktrace)]),
      Err
  end.

-spec safe_apply(Mod::atom(), Fun::atom(), Parmas::[any()]) -> any().
safe_apply(Mod, Fun, Args) ->
  try
    erlang:apply(Mod, Fun, Args)
  catch
    error:{badmatch, {error, Err}} when is_atom(Err) -> Err;
    error:{badmatch, Err}:Stacktrace when is_atom(Err)->
      ?DEBUG("[~p] safe_apply, Stacktrace: ~s", [?MODULE, lager:pr_stacktrace(Stacktrace)]),
      Err
  end.

%% 是不是游戏错误码
is_err_code({error, Error}) ->
  is_err_code(Error);
is_err_code(Error) when is_atom(Error) ->
  ErrStr = atom_to_list(Error),
  lists:prefix("err_", ErrStr);
is_err_code(_) ->
  false.

%% 解析错误码 参数是Class:Reason:Stacktrace的Reason
fetch_err_code({badmatch, {error, Error}}) ->
  ?IF(is_err_code(Error), {ok, Error}, fail);
fetch_err_code({badmatch, Error}) ->
  ?IF(is_err_code(Error), {ok, Error}, fail);
fetch_err_code(_) ->
  fail.

%% -----------------------------------------------------------------
index_of(Item, List) -> index_of(Item, List, 1).

index_of(_, [], _) -> undefined;
index_of(Item, [Item|_], Index) -> Index;
index_of(Item, [_|TL], Index) -> index_of(Item, TL, Index + 1).

cut_atom_head(Atom, Head) ->
  Str1 = atom_to_list(Atom),
  Str2 = atom_to_list(Head),
  case string:str(Str1, Str2) of
    1 ->
      Str3 = string:sub_string(Str1, length(Str1) - length(Str2)),
      erlang:list_to_atom(Str3);
    _ ->
      Atom
  end.

if_then(true, V1, _V2) ->
  V1;
if_then(_, _V1, V2) ->
  V2.

%% -----------------------------------------------------------------
get_uin(Platform, OpenID) ->
  lists:concat([Platform, "-", OpenID]).

no_login_check(Uin) ->
  ets:member(?SDK_FREE_LOGIN_ETS, Uin).