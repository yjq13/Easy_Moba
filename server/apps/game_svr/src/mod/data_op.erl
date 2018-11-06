-module(data_op).
%%
%% API
-export([req/2]).

-include("up_pb.hrl").
-include("down_pb.hrl").
-include("com_def.hrl").
-include("err_code.hrl").

%%% ================================ export ================================
req(_, #req_data_op{op = Op, data = Data, data_list = DataList}) when Op =/= undefined ->
  data_op(Op, Data, DataList);
req(_, _Req) ->
	?ERR_UNEXPECTED_REQUEST.

data_op(_Op, Data, []) ->
	#reply_data_op{result = success, data = Data};
data_op(add, Data, [Data_2 | Left]) ->
	data_op(add, Data + Data_2, Left);
data_op(sub, Data, [Data_2 | Left]) ->
	data_op(sub, Data - Data_2, Left);
data_op(mul, Data, [Data_2 | Left]) ->
	data_op(mul, Data * Data_2, Left);
data_op('div', Data, [Data_2 | Left]) ->
	data_op('div', Data div Data_2, Left).