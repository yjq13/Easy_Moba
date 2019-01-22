-module(ws_handler).

-include("game_pb.hrl").
-include("up_pb.hrl").
-include("down_pb.hrl").
-include("com_def.hrl").
-include("err_code.hrl").

-define(IDLE_TIME_OUT, 60000).

-export([start_handler/0, init/2, websocket_init/1, websocket_handle/2, websocket_info/2, terminate/3]).

-record(state, {uid, uin, svr_id, session, verified = false, ip, last_op_ts}).

start_handler() ->
  {ok, WsPort} = config:get(game_svr, gs_port),
  Dispatch     = cowboy_router:compile([
    {'_', [
      {"/", ws_handler, []}
    ]}
  ]),
  {ok, Pid} = cowboy:start_clear(http, [{port, WsPort}], #{
    env => #{dispatch => Dispatch}
  }),
  {ok, Pid}.

init(Req, _State) ->
  {IP, _Port} = cowboy_req:peer(Req),
  Opts = #{idle_timeout  => ?IDLE_TIME_OUT},
  {cowboy_websocket, Req, #state{ip = inet:ntoa(IP)}, Opts}.

websocket_init(State) ->
  usocket:set_socket(State#state.uid, self()),
  ?DEBUG("[~p], websockt_init pid ~p", [?MODULE, self()]),
  {ok, State}.

websocket_handle({binary, UpData}, State) ->
  % ?INFO("binary up_data received:~w", [UpData]),
  UpMsg    = up_pb:decode_msg(UpData, up_msg),
  {ok, Mod, _Act, Cmd} = up_util:fetch_cmd(UpMsg),
  _        = do_log_msg(up, UpMsg, State),
  {DownMsg, NewState} = websocket_handle__(UpMsg, Mod, Cmd, State),
  _        = do_log_msg(down, DownMsg, NewState),
  DownData = encode_data(UpMsg, DownMsg, NewState, tools:is_err_code(DownMsg)),
  {reply, DownData, NewState};
websocket_handle(Data, State) ->
  %% !!! tmp
  ?DEBUG("!!!up: ~p", [?PR(Data)]),
  %%
  {ok, State}.

encode_data(_UpMsg, Err, #state{verified = false}, true) ->
  {close, atom_to_list(Err)};
encode_data(UpMsg, Msg, _, _) ->
  DownMsg  = pack_down_msg(UpMsg#up_msg.seq, Msg),
  DownData = down_pb:encode_msg(DownMsg),
  {binary, DownData}.

do_log_msg(up, UpMsg = #up_msg{heartbeat = undefined}, State)  ->
  ?DEBUG("[~p] ~p up msg: ~p, state: ~p", [?MODULE, self(), ?PR(UpMsg), State]);
do_log_msg(down, {reply_heartbeat, _}, _State)   ->
  ok;
do_log_msg(down, DownMsg, State) ->
  ?DEBUG("[~p] ~p down msg: ~p, new state: ~p", [?MODULE, self(), ?PR(DownMsg), State]);
do_log_msg(_, _, _) ->
  ok.

websocket_info({push, PbMsg}, State) ->
  ?DEBUG("[~p] send pb msg: ~p ~p", [?MODULE, self(), PbMsg]),
  {reply, {binary, PbMsg}, State};

websocket_info({_From, another_login}, #state{uid = UID} = State) ->
  ?DEBUG("[~p] ~p another_login, uid ~p", [?MODULE, self(), UID]),
  {reply, {close, atom_to_list(?ERR_LOGIN_ANOTHER_LOGIN)}, State};

websocket_info({timeout, _Ref, Msg}, State) ->
  {reply, {text, Msg}, State};

websocket_info(_Info, State) ->
  {ok, State}.

terminate(_, _, #state{uid = UID, uin = Uin} = State) ->
  _ = logout(UID, Uin),
  ?DEBUG("[~p], websockt_close pid ~p ~p", [?MODULE, self(), State]),
  ok.

logout(UID, Uin) when UID =/= undefined andalso Uin =/= undefined->
  ok;
logout(_, _) ->
  ok.

websocket_handle__(UpMsg, Mod, Cmd, State) ->
  case config:get(game_svr, maintenance, nowarn) of
    {ok, true} ->
      {?ERR_SERVER_MAINTENANCE, State};
    _ ->
      websocket_handle_2(UpMsg, Mod, Cmd, State)
  end.

websocket_handle_2(UpMsg, Mod, Cmd, State) ->
  case tools:safe_apply(fun do_websocket_handle/4, [UpMsg, Mod, Cmd, State]) of
    {ok, Msg, NewState} ->
      {Msg, NewState};
    Err ->
      {Err, State}
  end.

%% heartbeat
do_websocket_handle(_UpMsg, heartbeat, Cmd, #state{uid = UID} = State) ->
  Msg = heartbeat:req(UID, Cmd),
  {ok, Msg, State};
%% sdk login
do_websocket_handle(_UpMsg, sdk_login, Cmd, State) ->
  Msg       = sdk_login:req(Cmd),
  {ok, Msg, State};
%% login
do_websocket_handle(_UpMsg, login, Cmd, #state{ip = IP} = State) ->
  {{UID, Uin, SvrID}, Msg} = login:req(Cmd, IP),
  NewState  = State#state{uid = UID, uin = Uin, svr_id = SvrID, verified = true},
  {ok, Msg, NewState};
do_websocket_handle(#up_msg{repeat = Repeat} = UpMsg, _Mod, _Cmd, #state{uid = UID, verified = true} = State) when Repeat > 0 ->
  Msg       = cache_msg:get_msg(UID, UpMsg),
  {ok, Msg, State};
%% cmd
do_websocket_handle(UpMsg, Mod, Cmd, #state{uid = UID, verified = true} = State) ->
  Msg       = exec_cmd(UID, Mod, Cmd),
  ok        = cache_msg:insert(UID, UpMsg, Msg),
  {ok, Msg, State};
%% unexpect when unverified
do_websocket_handle(_UpMsg, _Mod, _Req, #state{verified = false} = State)->
  ?DEBUG("[~p] ~p unexpect not verfied request ~p", [?MODULE, self(), _UpMsg]),
  {ok, ?ERR_UNEXPECTED_REQUEST, State}.

exec_cmd(_UID, Mod, Cmd) ->
  User      = undefined, 
  Reply     = exec_cmd__(User, Mod, Cmd),
  Reply.

exec_cmd__(User, Mod, Msg) ->
  try
    Mod:req(User, Msg)
  catch
    Class:Reason:Stacktrace ->
      case tools:fetch_err_code(Reason) of
        {ok, Error} ->
          ?INFO("[~p] Uid: ~p, Req Msg: ~p. ~n Error: ~p", [?MODULE, 0, ?PR(Msg), Error]),
          % ?INFO("[~p] Uid: ~p, Req Msg: ~p. ~n Error: ~p", [?MODULE, User#user.uid, ?PR(Msg), Error]),
          Error;
        fail ->
          ?ERROR("[~p] Uid: ~p, Req Msg: ~p.~nStacktrace: ~s",
            [?MODULE, 0, ?PR(Msg), ?PR_ST(Stacktrace, {Class, Reason})]),
            % [?MODULE, User#user.uid, ?PR(Msg), ?PR_ST(Stacktrace, {Class, Reason})]),
          ?ERR_DISPATCH_CMD
      end
  end.

%% 封装下发协议
pack_down_msg(Seq, Msg) ->
  DownMsg = pack_down_msg_(Msg),
  DownMsg#down_msg{seq = Seq, svr_time = ?NOW}.

pack_down_msg_(Error) when is_atom(Error) ->
  #down_msg{err_code = #reply_err_code{err_code = atom_to_list(Error)}};
pack_down_msg_(Msg) when is_record(Msg, down_msg) ->
  Msg;
pack_down_msg_(Msg) ->
  MsgType = tools:cut_atom_head(element(1, Msg), reply_),
  Fields  = record_info(fields, down_msg),
  Index   = tools:index_of(MsgType, Fields),
  erlang:setelement(Index + 1, #down_msg{extra_info = #reply_extra_info{}}, Msg).