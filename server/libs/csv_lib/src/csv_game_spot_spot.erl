-module(csv_game_spot_spot).

-include("csv_game_spot_spot.hrl").

-export([get/1, get/2, get_all_keys/0, get_max_key/0, get_field/2, get_field/3,check_valid_key/1, check_valid_key/2, dump/0]).

get(Key) ->
  case get_(Key) of
    Cfg when is_tuple(Cfg) ->
      {ok, Cfg};
    Error ->
      lager:error("[~p]csv cfg not found: ~p", [?MODULE, Key]),
      Error
  end.

get(Key, nowarn) ->
  case get_(Key) of
    Cfg when is_tuple(Cfg) ->
      {ok, Cfg};
    Error ->
      Error
  end;
get(Key, ErrCode) ->
  case get_(Key) of
    Cfg when is_tuple(Cfg) ->
      {ok, Cfg};
    _Err ->
      lager:error("[~p]csv cfg not found: ~p", [?MODULE, Key]),
      ErrCode
  end.

get_({1, 0}) -> 
  #csv_game_spot_spot{game_id = 1, spot_id = 0, point_consume = 0, search_max_time = 9999, point_reward = 0, clue = "好的"};
get_({1, 1}) -> 
  #csv_game_spot_spot{game_id = 1, spot_id = 1, point_consume = 1, search_max_time = 3, point_reward = 0, clue = "好的"};
get_({1, 2}) -> 
  #csv_game_spot_spot{game_id = 1, spot_id = 2, point_consume = 1, search_max_time = 3, point_reward = 0, clue = "好的"};
get_({1, 3}) -> 
  #csv_game_spot_spot{game_id = 1, spot_id = 3, point_consume = 1, search_max_time = 3, point_reward = 0, clue = "好的"};
get_({1, 4}) -> 
  #csv_game_spot_spot{game_id = 1, spot_id = 4, point_consume = 1, search_max_time = 3, point_reward = 0, clue = "好的"};
get_({1, 5}) -> 
  #csv_game_spot_spot{game_id = 1, spot_id = 5, point_consume = 1, search_max_time = 3, point_reward = 0, clue = "好的"};
get_({1, 6}) -> 
  #csv_game_spot_spot{game_id = 1, spot_id = 6, point_consume = 1, search_max_time = 3, point_reward = 0, clue = "好的"};
get_({1, 7}) -> 
  #csv_game_spot_spot{game_id = 1, spot_id = 7, point_consume = 1, search_max_time = 3, point_reward = 0, clue = "好的"};
get_({1, 8}) -> 
  #csv_game_spot_spot{game_id = 1, spot_id = 8, point_consume = 1, search_max_time = 3, point_reward = 0, clue = "好的"};
get_({3, 26}) -> 
  #csv_game_spot_spot{game_id = 3, spot_id = 26, point_consume = 0, search_max_time = 9999, point_reward = 1, clue = "好的"};
get_(_) ->
  err_csv_cfg_not_found.

get_field(Key, Field) ->
  get_field(Key, Field, err_csv_cfg_not_found).

get_field(Key, game_id, ErrCode) ->
  case csv_game_spot_spot:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_game_spot_spot.game_id};
    _Err -> ErrCode
  end;
get_field(Key, spot_id, ErrCode) ->
  case csv_game_spot_spot:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_game_spot_spot.spot_id};
    _Err -> ErrCode
  end;
get_field(Key, point_consume, ErrCode) ->
  case csv_game_spot_spot:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_game_spot_spot.point_consume};
    _Err -> ErrCode
  end;
get_field(Key, search_max_time, ErrCode) ->
  case csv_game_spot_spot:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_game_spot_spot.search_max_time};
    _Err -> ErrCode
  end;
get_field(Key, point_reward, ErrCode) ->
  case csv_game_spot_spot:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_game_spot_spot.point_reward};
    _Err -> ErrCode
  end;
get_field(Key, clue, ErrCode) ->
  case csv_game_spot_spot:get(Key, ErrCode) of
    {ok, Cfg} -> {ok, Cfg#csv_game_spot_spot.clue};
    _Err      -> ErrCode
  end.

get_all_keys() -> 
  [{1, 0}, {1, 1}, {1, 2}, {1, 3}, {1, 4}, {1, 5}, {1, 6}, {1, 7}, {1, 8}, {3, 26}].

check_valid_key(Key) -> 
  check_valid_key(Key, err_invalid_cfg_key).

check_valid_key(Key, ErrCode) -> 
  case lists:member(Key, get_all_keys()) of 
    true  -> ok;
    false ->
      lager:debug("[~p] check valid key: ~p error: ~p", [?MODULE, Key, ErrCode]),
      ErrCode
  end.

get_max_key() -> 
  {3, 26}.

dump() -> 
  Keys    = get_all_keys(),
  [io:format("~p~n", [get_(Key)]) || Key <- Keys],
  ok.