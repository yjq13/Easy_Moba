%%%-------------------------------------------------------------------
%%% @doc
%%% @end
%%%-------------------------------------------------------------------
-module(up_util).

-include("up_pb.hrl").
-include("gpb.hrl").
-include("err_code.hrl").

%% API
-export([fetch_cmd/1, rm_prefix/1]).

fetch_cmd(UpMsg) when is_record(UpMsg, up_msg) ->
  [up_msg, _Seq, _Repeat|Cmds] = tuple_to_list(UpMsg),
  case lists:search(fun(V) -> V =/= undefined end, Cmds) of
    {value, Cmd} ->
      fetch_cmd__(Cmd);
    false ->
      ?ERR_UNEXPECTED_REQUEST
  end.

fetch_cmd__(Cmd) ->
  L1     = erlang:tuple_to_list(Cmd),
  L2     = erlang:tl(L1),
  ReqMod = element(1, Cmd),
  DL     = up_pb:find_msg_def(ReqMod),
  L3     = lists:zip(L2, DL),
  L4     = [D || {A, D} <- L3, A =/= undefined andalso A =/= []],
  ReqAct = to_act(ReqMod, L4),
  Mod    = rm_prefix(ReqMod),
  Act    = rm_prefix(ReqAct),
  {ok, Mod, Act, Cmd}.

to_act(ReqMod, ActMsg) ->
  case ActMsg of
    [D] ->
      D#?gpb_field.name;
    _ ->
      ReqMod
  end.

rm_prefix(Atom) ->
  Str = atom_to_list(Atom),
  case Str of
    [$r, $e, $q, $_ | Left] ->
      list_to_atom(Left);
    _ ->
      Atom
  end.