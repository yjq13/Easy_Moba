-module(token).

-include("err_code.hrl").
-include("ets.hrl").

-export([init_ets/0]).
-export([get/1, set/2, delete/1, delete_all/0]).
-export([match_delete/1]).

init_ets() ->
  ets:new(?TOKEN_ETS, [set, named_table, public]).

get(Key) ->
  case ets:lookup(?TOKEN_ETS, Key) of
    [] ->
      ?ERR_TOKEN_EMPTY;
    [{Key, Value}] ->
      {ok, Value}
  end.

delete_all() ->
  ets:delete_all_objects(?TOKEN_ETS).

delete(Key) ->
  ets:delete(?TOKEN_ETS, Key).

set(Key, Value) ->
  ets:insert(?TOKEN_ETS, {Key, Value}).

match_delete(Match) ->
  ets:match_delete(?TOKEN_ETS, Match).