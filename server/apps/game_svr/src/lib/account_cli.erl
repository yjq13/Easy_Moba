-module(account_cli).

-export([init_ets/0, get_or_create_user_id/2, get_uin_roles/1]).

-include("err_code.hrl").
-include("com_def.hrl").
-include("ets.hrl").
-include("record.hrl").

% =======================================================================
init_ets() ->
  ets:new(?ACC_ETS, [set, public, named_table, compressed, {keypos, #acc.uin}]),
  ?DEBUG("[~p] init ~p", [?MODULE, self()]).

% -----------------------------------------------------------------------
get_or_create_user_id(Uin, undefined) ->
  {ok, MaxSvrID}  = config:get(game_svr, max_svr_id),
  get_or_create_user_id(Uin, MaxSvrID);
get_or_create_user_id(Uin, SvrID) when is_integer(SvrID) ->
  {IsNewAcc, Acc} = case ets:lookup(?ACC_ETS, Uin) of
    [A] ->
      {false, A};
    _ ->
      {true, #acc{uin = Uin, create_ts = ?NOW}}
  end,
  {IsNewRole, UID}  = case proplists:get_value(SvrID, Acc#acc.roles, undefined) of
    I when is_integer(I) ->
      {false, I};
    undefined ->
      {ok, I} = sys_param:next(uid),
      {true, I}
  end,
  case IsNewAcc or IsNewAcc of
    true  ->
      NewAcc  = Acc#acc{roles = [{SvrID, UID} | Acc#acc.roles]},
      ets:insert(?ACC_ETS, NewAcc);
    false ->
      ok
  end,
  {ok, SvrID, UID}.

% -----------------------------------------------------------------------
get_uin_roles(Uin) ->
  case ets:lookup(?ACC_ETS, Uin) of
    [#acc{roles = Roles}] ->
      Roles;
    _ ->
      []
  end.