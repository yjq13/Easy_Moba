-module(login).

-include("game_pb.hrl").
-include("ets.hrl").
-include("record.hrl").
-include("err_code.hrl").
-include("up_pb.hrl").
-include("down_pb.hrl").
-include("com_def.hrl").

-export([req/2, check_login/5]).

-export([test_req/0]).

%% ====================================================================
test_req() ->
  Req = #req_login{
    platform    = dev,
    open_id     = "open_id_1",
    svr_id      = 1,
    token       = "token_str59757990",
    os          = web,
    push_id     = "push_id_1",
    cli_version = "1.1.0",
    lang        = en,
    time_zone   = 0,
    name        = "name_1"
  },
  IP = "192.168.5.5",
  ?TEST_REQ(?MODULE, req, [Req, IP]).

req(Msg, IP) ->
  #req_login{
    platform    = Platform,
    open_id     = OpenID,
    svr_id      = SvrID,
    token       = Token,
    os          = OS,
    push_id     = PushID,
    cli_version = CliVersion,
    lang        = Lang,
    time_zone   = TimeZone,
    name        = Name
  } = Msg,
  ok            = check_time_zone(TimeZone),
  ok            = check_os(OS),
  ok            = check_platform(Platform),
  ok            = check_lang(Lang),
  ok            = check_login(OpenID, Platform, SvrID, Token, CliVersion),
  Uin           = tools:get_uin(Platform, OpenID),
  % !!! 唯一登陆验证，暂未实现
  % ok            = sso:auth(Uin),
  {ok, User}    = users:get_or_create(Uin, SvrID),
  %% sso之后就可以设置usocket了，保证下面的notify能正常发送
  ok            = usocket:set_socket(User#user.uid, self()),
  % !!! 玩家数据升级，暂未实现
  % User1         = amend:upgrade(User),
  NewUser       = users:set(User),
  % !!! 暂时没理清楚这里为什么要del usocket
  ok            = usocket:del_notify_socket(User#user.uid),
  {ok, Region}  = config:get(game_svr, region),
  ReplyLogin    = #reply_login{
    user          = users:pack_user(NewUser),
    svr_time_zone = time_util:time_zone(),
    region        = Region
  },
  #user{uid = UID, uin = Uin, svr_id = SvrID} = NewUser,
  {{UID, Uin, SvrID}, ReplyLogin}.

%% ====================================================================
check_time_zone(TimeZone) ->
  ?IF(TimeZone >= -12 andalso TimeZone =< 12, ok, ?ERR_LOGIN_TIME_ZONE).

check_os(OS) ->
  proto:check_valid_enum(common, t_os, OS, ?ERR_LOGIN_OS).

check_platform(Platform) ->
  proto:check_valid_enum(common, t_platform, Platform, ?ERR_LOGIN_PLATFORM).

check_lang(Lang) ->
  proto:check_valid_enum(common, t_lang, Lang, ?ERR_LOGIN_LANGUAGE).

%% --------------------------------------------------------------------
check_login(OpenID, Platform, SvrID, Token, CliVersion) ->
  Uin = tools:get_uin(Platform, OpenID),
  ok  = check_client_version(Uin, CliVersion),
  ok  = check_svr_id(SvrID),
  ok  = check_game_token(Uin, Token),
  ok.

check_client_version(Uin, CliVersion) ->
  case tools:no_login_check(Uin) of
    true ->
      ok;
    false ->
      check_client_version__(CliVersion)
  end.

check_client_version__(CliVersion) ->
  {ok, CheckClientVersion} = config:get(game_svr, check_client_version),
  case CheckClientVersion of
    true ->
      {ok, EnvVersion} = config:get(game_svr, cli_version),
      ?IF(CliVersion =:= EnvVersion, ok, ?ERR_LOGIN_CLIENT_VERSION);
    false ->
      ok
  end.

check_svr_id(SvrID) ->
  {ok, MaxSvrID}  = config:get(game_svr, max_svr_id),
  ?IF(SvrID =< MaxSvrID, ok, ?ERR_LOGIN_SVR_ID).
% !!! node获取服务器列表，暂未实现
% check_svr_id(SvrID) ->
%   SvrIDs = node:get_svr_ids(),
%   ?IF(lists:member(SvrID, SvrIDs), ok, ?ERR_LOGIN_SVR_ID).

check_game_token(Uin, Token0) ->
  {ok, Token} = token:get(Uin),
  ?IF(Token0 =:= Token, ok, ?ERR_LOGIN_GAME_TOKEN).