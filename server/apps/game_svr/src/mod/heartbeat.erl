-module(heartbeat).
%%
%% API
-export([req/2]).

-include("up_pb.hrl").
-include("down_pb.hrl").
-include("com_def.hrl").

%%% ================================ export ================================
req(_UID, #req_heartbeat{time = Req}) when Req =/= undefined  ->
  #reply_heartbeat{delay = ?HEARTBEAT_DELAY_TIME}.