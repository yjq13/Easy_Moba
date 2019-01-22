-module(usocket).

-include("up_pb.hrl").
-include("down_pb.hrl").
-include("record.hrl").
-include("ets.hrl").
-include("com_def.hrl").

-export([
  init_ets/0,
  send_pb_msg/2,
  send_pb_msg_local/2,
  get_count/0,
  set_socket/2,
  del_socket/1,
  is_member/1,
  broadcast/1]).
-export([del_notify_socket/1, get_notifies/1]).
%%%===================================================================
%%% API
%%%===================================================================
init_ets() ->
  ets:new(?USOCKET_ETS, [set, public, named_table, compressed, {keypos, #usocket.uid}]),
  ets:new(?USCOKET_NOTIFIES_ETS, [set, public, named_table, compressed, {keypos, #usocket_notify.uid}]).

%%--------------------------------------------------------------------

broadcast(PbMsg) ->
  Fun = fun(UID, []) ->
            send_pb_msg_local(UID, PbMsg)
        end,
  tools:foreach_ets(?USOCKET_ETS, Fun, []).

send_pb_msg(UID, PbMsg)  ->
  node:cast(usocket, send_pb_msg_local, [UID, PbMsg]).

send_pb_msg_local(UID, PbMsg) when is_record(PbMsg, down_msg) ->
  ?DEBUG("[~p] send_pb_msg uid ~p, pbmsg ~p", [?MODULE, UID, PbMsg]),
  case ets:lookup(?USOCKET_ETS, UID)  of
    [#usocket{pid = Pid}]->
      case catch erlang:is_process_alive(Pid) of
        true ->
          NewPbMsg = PbMsg#down_msg{svr_time = time_util:now()},
          EnPbMsg  = down_pb:encode_msg(NewPbMsg),
          Pid!{push, EnPbMsg};
        _ ->
          set_notify_socket(UID, PbMsg#down_msg.notify)
      end;
    _ ->
      set_notify_socket(UID, PbMsg#down_msg.notify)
  end;
send_pb_msg_local(UID, PbMsg) when is_record(PbMsg, reply_notify)->
  DownMsg = #down_msg{notify = PbMsg},
  send_pb_msg_local(UID, DownMsg);
send_pb_msg_local(UID, PbMsg) ->
  ?ERROR("[~p] send_pb_msg msg error, uid ~p, msg ~p", [?MODULE, UID, PbMsg]),
  ok.
%%--------------------------------------------------------------------
get_count() ->
  ets:info(usocket_ets, size).

set_socket(UID, Pid) ->
  ets:insert(?USOCKET_ETS, #usocket{uid = UID, pid = Pid}),
  ok.

del_socket(UID) ->
  ets:delete(?USOCKET_ETS, UID),
  ok.

set_notify_socket(UID, Notify) ->
  case ets:lookup(?USCOKET_NOTIFIES_ETS , UID) of
    [A] ->
      ets:insert(?USCOKET_NOTIFIES_ETS, #usocket_notify{uid = UID, notifies = [Notify|A#usocket_notify.notifies]});
    _ ->
      ets:insert(?USCOKET_NOTIFIES_ETS, #usocket_notify{uid = UID, notifies = [Notify]})
  end,
  ok.

del_notify_socket(UID) ->
  ets:delete(?USCOKET_NOTIFIES_ETS, UID),
  ok.

get_notifies(UID) ->
  case ets:lookup(?USCOKET_NOTIFIES_ETS , UID) of
    [A] ->
      lists:reverse(A#usocket_notify.notifies);
    _ ->
      []
  end.

is_member(UID) ->
  ets:member(?USOCKET_ETS, UID).
%%%===================================================================
%%% Internal
%%%===================================================================
