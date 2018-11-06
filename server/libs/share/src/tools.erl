-module(tools).

-include("com_def.hrl").

-export([child_spec/3, child_spec/4]).
-export([is_err_code/1]).
-export([safe_apply/2, safe_apply/3]).
-export([fetch_err_code/1]).

child_spec(Mod, Function, Param) ->
  child_spec(Mod, Mod, Function, Param).

child_spec(Id, Mod, Function, Param) ->
  {Id, 
    {Mod, Function, Param},
    permanent, 1000, worker, [Mod]
  }.

%% 是不是游戏错误码
is_err_code({error, Error}) ->
  is_err_code(Error);
is_err_code(Error) when is_atom(Error) ->
  ErrStr = atom_to_list(Error),
  lists:prefix("err_", ErrStr);
is_err_code(_) ->
  false.

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

%% 解析错误码 参数是Class:Reason:Stacktrace的Reason
fetch_err_code({badmatch, {error, Error}}) ->
  ?IF(is_err_code(Error), {ok, Error}, fail);
fetch_err_code({badmatch, Error}) ->
  ?IF(is_err_code(Error), {ok, Error}, fail);
fetch_err_code(_) ->
  fail.