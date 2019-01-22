-module(cache_msg).

-include("up_pb.hrl").
-include("com_def.hrl").
-include("ets.hrl").
%% API
-export([init_ets/0, get_msg/2, insert/3]).

-record(cache_msg, {uid, up_msg, down_msg}).

init_ets() ->
  ets:new(?CACHE_MSG_ETS, [set, named_table, public, {keypos, #cache_msg.uid}]).

get_msg(UID, RpUpMsg) ->
  UpMsg = RpUpMsg#up_msg{repeat = 0},
  case ets:lookup(?CACHE_MSG_ETS, UID) of
    [#cache_msg{up_msg = UpMsg, down_msg = DownMsg}] ->
      DownMsg;
    _ ->
      no_reply
  end.

insert(UID, UpMsg, DownMsg) ->
  ets:insert(?CACHE_MSG_ETS, #cache_msg{uid = UID, up_msg = UpMsg, down_msg = DownMsg}),
  ok.