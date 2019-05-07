-module(users).

-include("game_pb.hrl").
-include("ets.hrl").
-include("record.hrl").
-include("err_code.hrl").
-include("up_pb.hrl").
-include("down_pb.hrl").
-include("com_def.hrl").
-include("user.hrl").

-include_lib("stdlib/include/ms_transform.hrl").

-export([init_ets/0]).
-export([get_or_create/2, get/1, set/1, pack_user/1]).

%% ====================================================================
init_ets() ->
  ets:new(?USER_ETS, [set, public, named_table, compressed, {keypos, #user.uid}]),
  ?DEBUG("[~p] init ~p", [?MODULE, self()]).

%% --------------------------------------------------------------------
%% 玩家自己调用
get_or_create(Uin, SvrID) ->
  {ok, SvrID, UID}  = account_cli:get_or_create_user_id(Uin, SvrID),
  case users:get(UID) of
    {ok, User} ->
      {ok, User};
    _ ->
      User  = init_user(UID, Uin, SvrID),
      {ok, User}
  end.

%% --------------------------------------------------------------------
get(UID) ->
  case ets:lookup(?USER_ETS, UID) of
    [User] ->
      {ok, User};
    _ ->
      ?ERR_USER_NOT_FOUND
  end.

%% --------------------------------------------------------------------
set(User) when is_record(User, user) ->
  ets:insert(?USER_ETS, User),
  User.

%% --------------------------------------------------------------------
pack_user(User) when is_record(User, user)  ->
  #reply_user{
    uid             = User#user.uid,
    name            = User#user.name,
    level           = User#user.level,
    exp             = User#user.exp,
    red_dots        = User#user.red_dots,
    settings        = User#user.settings,
    tutorials       = User#user.tutorials,
    lang            = User#user.lang,
    time_zone       = User#user.time_zone,
    avatar          = User#user.avatar,
    frame           = User#user.frame,
    gender          = User#user.gender,
    country         = User#user.country,
    city            = User#user.city,
    desc            = User#user.desc,
    create_time     = User#user.create_time
  }.
%% ====================================================================
init_user(UID, Uin, SvrID) ->
  #user{
      uid     = UID
    , uin     = Uin
    , svr_id  = SvrID
  }.
