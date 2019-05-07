-module(sdk_login).

-export([req/1]).

-export([test_req/0]).

-include("err_code.hrl").
-include("com_def.hrl").
-include("up_pb.hrl").
-include("down_pb.hrl").
-include("sdk.hrl").

test_req() ->
  Req = #req_sdk_login{platform = dev, open_id = "open_id_1", token = "token_str", svr_id = 1},
  ?TEST_REQ(?MODULE, req, [Req]).

req(#req_sdk_login{platform = Platform, open_id = OpenID, token = Token, svr_id = SvrID}) ->
  Uin          = tools:get_uin(Platform, OpenID),
  ok           = login_validate(Uin, Platform, OpenID, Token),
  Htoken       = generate_game_token(Token),
  ok           = set_game_token(SvrID, Uin, Htoken),
  #reply_sdk_login{htoken = Htoken}.

%% 1. 免sdk登录检查； 2. 若不通过，则进行sdk登录验证
login_validate(Uin, Platform, OpenID, Token)   ->
  case tools:no_login_check(Uin) of
    true ->
      ok;
    false ->
      login_validate__(Platform, OpenID, Token)
  end.

login_validate__(dev, _OpenID, _Token) ->
  ok;
login_validate__(_, _, _) ->
  ?ERR_SDK_LOGIN_VERIFY_FAILED.

generate_game_token(Token) ->
  lists:concat([Token, random_util:next(?GAME_TOKEN_RANGE)]).

set_game_token(_SvrID, Uin, Token) ->
  % !!! 暂未实现node模块。暂无跨node调用
  % case node:call({svr, SvrID}, token, set, [Uin, Token]) of
  %   true -> ok;
  %   Err -> Err
  % end.
  token:set(Uin, Token),
  ok.
