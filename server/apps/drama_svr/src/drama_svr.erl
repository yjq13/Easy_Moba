-module(drama_svr).

-export([init/2]).

-include("com_def.hrl").

%% --------------------------------------------------------------------
init(Req0, Opts) ->
  Method  = cowboy_req:method(Req0),
  Res     = proc_req(Method, Req0),
  {ok, Res, Opts}.  

%% --------------------------------------------------------------------
proc_req(<<"GET">>, Req) ->
  ReqParams = parse_req_params(Req),
  Result = do_cmd(ReqParams),
  cowboy_req:reply(200,  #{
    <<"content-type">> => <<"text/plain; charset=utf-8">>
  }, Result, Req);
proc_req(_, Req) ->
  cowboy_req:reply(500, Req).

%% --------------------------------------------------------------------
parse_req_params(Req) ->
  parse_req_params_i(cowboy_req:parse_qs(Req), []).

parse_req_params_i([], Res) ->
  Res;
parse_req_params_i([{BinK, BinV} | Left], Res) ->
  parse_req_params_i(Left, [{erlang:binary_to_list(BinK), erlang:binary_to_list(BinV)} | Res]).

%% --------------------------------------------------------------------
do_cmd(ReqParams) ->
  GameID    = proplists:get_value("game_id", ReqParams),
  UID       = proplists:get_value("uid", ReqParams),
  PassWord  = proplists:get_value("password", ReqParams),
  Cmd       = proplists:get_value("cmd", ReqParams),
  Spot      = proplists:get_value("spot", ReqParams),
  case check_param_valid([GameID, UID, PassWord, Spot]) of
    ok  ->
      do_cmd_proc(Cmd, GameID, UID, Spot);
    Err ->
      Err
  end.

%% !!! not completed !!!
do_cmd_proc("search", _GameID, _UID, _Spot) ->
  ok;
do_cmd_proc(_, _GameID, _UID, _Spot) ->
  err_cmd_not_supported.



%% --------------------------------------------------------------------
check_param_valid([GameID, UID, PassWord, _Spot] = List) ->
  case check_param_not_undefined(List) of
    ok  ->
      check_game_user_valid(GameID, UID, PassWord);
    Err ->
      Err
  end.

check_param_not_undefined([]) ->
  ok;
check_param_not_undefined([undefined | _Left]) ->
  err_required_param_undefined;
check_param_not_undefined([_ | Left]) ->
  check_param_not_undefined(Left).

check_game_user_valid(GameID, UID, PassWord) ->
  case csv_game_role:get_field({GameID, UID}, password) of
    {ok, PassWord0} when PassWord0 =:= PassWord ->
      ok;
    {ok, _Password0} ->
      err_password_mismatch;
    _ ->
      err_game_role_not_exist
  end.
