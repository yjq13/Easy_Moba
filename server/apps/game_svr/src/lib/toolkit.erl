-module(toolkit).

-include("err_code.hrl").
-include("down_pb.hrl").
% -include("redis.hrl").
-include("game_pb.hrl").
-include("com_def.hrl").
-include("ets.hrl").
-include("record.hrl").

%% API
-export([init_ets/0]).
% -export([open_alarm/0, close_alarm/0]).
% -export([open_battle_check/0, close_battle_check/0]).
% -export([open_battle_report/0, close_battle_report/0]).
% -export([get_user/1, get_user_amended/1, set_user/1]).  % 线上修数据专用函数，程序中勿用！
% -export([get_user_id/2]).
% -export([switch_acc_ls/2]).
% -export([transfer_user/2]).
% -export([add_uin_web/1, del_uin_web/1]).
% -export([set_maintenance/0, del_maintenance/0]).
% -export([del_user/1]).
% -export([dump_user/1, load_user/1, settle/1, set_acc_and_uid/3]).
% -export([set_console_level/1]).
% -export([add_diamond/2]).
% -export([kick_all_user/0, kick_local_user/0]).
% -export([do_multi_task/2, do_multi_task/3, do_multi_task_proc/1]).

init_ets() ->
  ets:new(?SDK_FREE_LOGIN_ETS, [set, public, named_table, {keypos, #sdk_fl.key}]).

% open_alarm() ->
%   application:set_env(game_svr, alarm_notify, true).

% close_alarm() ->
%   application:set_env(game_svr, alarm_notify, false).

% get_user(UID) when is_integer(UID) ->
%   case users:get(UID) of
%     {ok, User} ->
%       User;
%     Err ->
%       Err
%   end;

% get_user(Uin) when is_list(Uin) ->
%   case get_user_id(Uin, undefined) of
%     {ok, undefined} ->
%       ?ERR_USER_NOT_FOUND;
%     {ok, UID} when is_integer(UID) ->
%       get_user(UID)
%   end.

% get_user_amended(UIDOrUin) ->
%   case get_user(UIDOrUin) of
%     User when is_record(User, user) ->
%       node:call({user, User#user.uid}, amend, upgrade, [User]);
%     Err ->
%       Err
%   end.

% set_user(User) when is_record(User, user) ->
%   node:call({user, User#user.uid},users, set, [User]).

% get_user_id(Uin, SvrID0) ->
%   {ok, SvrID} = account_cli:get_svr_id(Uin, SvrID0),
%   {ok, UID}   = redis_info:get_user_id(Uin, SvrID),
%   {ok, UID}.

% open_battle_check() ->
%   application:set_env(game_svr, bt_mode, prod).

% close_battle_check() ->
%   application:set_env(game_svr, bt_mode, debug).

% open_battle_report() ->
%   application:set_env(game_svr, battle_report, true),
%   DownMsg = #down_msg{reply_notify = #reply_notify{battle_report=true}},
%   usocket:broadcast_pb_msg(DownMsg),
%   ok.

% close_battle_report() ->
%   application:set_env(game_svr, battle_report, false),
%   DownMsg = #down_msg{reply_notify = #reply_notify{battle_report=false}},
%   usocket:broadcast_pb_msg(DownMsg),
%   ok.

% switch_acc_ls(Acc, SvrID) ->
%   redis_cli:hset(?REDIS_ACC_POOL, ?REDIS_KEY_ACCOUNT(Acc), ?REDIS_FIELD_OPEN_ID_LAST_LOGIN_SVR, SvrID).
% %交换两个账号的位置(归属于那个玩家的交换)
% transfer_user(UID1, UID2) ->
%   U1 = get_user_amended(UID1),
%   #user{uin = UIN1, uid = UID1, svr_id = Svr1} = U1,
%   U2 = get_user_amended(UID2),
%   #user{uin = UIN2, uid = UID2, svr_id = Svr2} = U2,
%   %交换缓存
%   ?INFO("toolkit:transfer_user(~w, ~w),transfer redis start!", [UID1, UID2]),
%   {ok, <<"1">>} = redis_script:run(?REDIS_TRANSFER_USER, [?REDIS_FIELD_OPEN_ID_LAST_LOGIN_SVR], [?REDIS_KEY_ACCOUNT(UIN1), UID1, Svr1, ?REDIS_KEY_ACCOUNT(UIN2), UID2, Svr2]),
%   ?INFO("toolkit:transfer_user(~w, ~w) transfer redis end!", [UID1, UID2]),
%   %%以下所有内容都要跨节点操作
%   ?INFO("toolkit:transfer_user(~w, ~w) transfer ets start!", [UID1, UID2]),
%   node:call({svr, Svr1}, ets, delete, [?ACC_ETS,{UIN1, Svr1}]),
%   node:call({svr, Svr2}, ets, delete, [?ACC_ETS,{UIN2, Svr2}]),
%   %这里不需要重新生成ets缓存,会自动生成
%   ?INFO("toolkit:transfer_user(~w, ~w) transfer ets end!", [UID1, UID2]),
%   ?INFO("toolkit:transfer_user(~w, ~w),transfer db start!", [UID1, UID2]),
%   NU1 = U1#user{uin = UIN2},
%   NU2 = U2#user{uin = UIN1},
%   set_user(NU1),
%   set_user(NU2),
%   ?INFO("toolkit:transfer_user(~w, ~w),transfer db end!", [UID1, UID2]),
%   ok.
% %添加账号权限->免sdk登录,暂时无跨服务器的操作
% add_uin_web(UIN) ->
%   ets:insert(?SDK_FREE_LOGIN, #sdk_fl{key = UIN}).

% del_uin_web(UIN) ->
%   ets:delete(?SDK_FREE_LOGIN, UIN).


% set_maintenance() ->
%   application:set_env(game_svr, maintenance, true).

% del_maintenance() ->
%   application:unset_env(game_svr, maintenance).

% del_user(UID) ->
%   User = get_user(UID),
%   #user{uin = UIN, uid = UID, svr_id = Svr} = User,
%   node:call({svr, Svr}, ets, delete, [?ACC_ETS,{UIN, Svr}]),
%   redis_cli:hdel(?REDIS_ACC_POOL, ?REDIS_KEY_ACCOUNT(UIN), Svr),
%   ok.

% dump_user(U)->
%   Bin = game_pb:encode_msg(U),
%   tools:dump_data_to(Bin, dump_user).

% load_user(only_in_dev)->
%   Bin   = tools:load_data_from(dump_user),
%   User  = game_pb:decode_msg(Bin, user),
%   User2 = settle(User),
%   User2.

% settle(User) ->
%   [SvrID|_] = node:get_svr_ids(),
%   ok        = set_acc_and_uid(User#user.uin, SvrID, User#user.uid),
%   User2     = create_relations(User, SvrID),
%   User3     = users:set(User2),
%   User3.

% create_relations(#user{uid = UID, uin = Uin} = User, SvrID)->
%   [Plat, _]  = tools:split_uin(Uin),
%   _          = uext:create(UID, Plat),
%   _          = mail_box:create(UID),
%   _          = intl_mail:create(UID),
%   _          = friend:create(UID),
%   _          = ublacklist:create(UID),
%   _          = set_acc_and_uid(Uin, SvrID, UID),
%   User2      = User#user{svr_id = SvrID},
%   User3      = amend:upgrade(User2),
%   {User4, _} = users:do_interval_update(User3),
%   User4.

% set_acc_and_uid(Uin, SvrID, UID) ->
%   {ok, <<"1">>} = redis_script:run(?REDIS_SET_USER, [?REDIS_FIELD_OPEN_ID_LAST_LOGIN_SVR], [?REDIS_KEY_ACCOUNT(Uin), UID, SvrID]),
%   ok.

% set_console_level(Level) ->
%   lager:set_loglevel(lager_console_backend, Level).

% add_diamond(UID, Diamond) ->
%   Assets = [#asset{type = currency, id = diamond, amount = Diamond}],
%   User   = toolkit:get_user(UID),
%   bonus:add_asset(User, Assets).  %% bonus 中的add_asset中特殊处理了日志的问题

% kick_all_user() ->
%   center_cli:call_game_nodes(?MODULE, kick_local_user, []).

% kick_local_user() ->
%   htoken:delete_all(),
%   usocket:broadcast_msg({close, ?ERR_HTOKEN_EMPTY}).


% do_multi_task(self, FunParam) ->
%   do_multi_task([node()], FunParam);

% do_multi_task(other_game, FunParam) ->
%   GameNodes = center_cli:get_game_nodes(),
%   do_multi_task(GameNodes -- [node()], FunParam);

% do_multi_task(Nodes, FunParam) ->
%   do_multi_task(Nodes, FunParam, false).

% do_multi_task(Nodes, FunParam, Return) ->
%   Father = self(),
%   [proc_lib:spawn(fun() ->
%     Rslt = rpc:call(Node, ?MODULE, do_multi_task_proc, [FunParam]),
%     Father ! {task_res, {Node, Rslt}}
%                   end) || Node <- Nodes],
%   Len    = length(Nodes),
%   AllRes = multi_task_collect_res(Len, Return, []),
%   case Return of
%     true -> AllRes;
%     false ->
%       {all_server_cnt, length(AllRes)}
%   end.

% multi_task_collect_res(0, _Return, Done) -> Done;
% multi_task_collect_res(Len, Return, Done) ->
%   receive
%     {task_res, {Node, {badrpc,nodedown}}} ->
%       io:format("~w~n", [{Node, nodedown}]),
%       multi_task_collect_res(Len - 1, Return, [{Node, nodedown}| Done]);
%     {task_res, {Node, Rslt}} ->
%       Return orelse io:format("~w~n", [{Node, Rslt}]),
%       multi_task_collect_res(Len - 1, Return, [{Node, Rslt}| Done]);
%     _ ->
%       multi_task_collect_res(Len, Return, Done)
%   end.

% do_multi_task_proc(Fun) when is_function(Fun) ->
%   Fun();
% do_multi_task_proc({M, F, A}) ->
%   apply(M, F, A);
% do_multi_task_proc(_) ->
%   invalid_command.