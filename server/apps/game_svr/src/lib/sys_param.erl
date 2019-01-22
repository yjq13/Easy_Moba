-module(sys_param).

-export([init_ets/0, next/1]).

-include("com_def.hrl").
-include("ets.hrl").
-include("sys_param.hrl").

% =======================================================================
init_ets() ->
  ets:new(?SYS_PARAM_ETS, [set, public, named_table, compressed, {keypos, 1}]),
  ets:insert(?SYS_PARAM_ETS, ?SYS_PARAM_INIT_LIST),
  ?DEBUG("[~p] init ~p", [?MODULE, self()]).

% -----------------------------------------------------------------------
next(uid) ->
  [{_, I}] = ets:lookup(?SYS_PARAM_ETS, uid),
  ets:update_counter(?SYS_PARAM_ETS, uid, {2, 1}).