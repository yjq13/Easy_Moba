-module(csv_game_spot_role).

-include("csv_game_spot_role.hrl").

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
  #csv_game_spot_role{game_id = 1, role_id = 0, pass_word = "0", max_point = 9999};
get_({1, 1}) -> 
  #csv_game_spot_role{game_id = 1, role_id = 1, pass_word = "1", max_point = 5};
get_({1, 2}) -> 
  #csv_game_spot_role{game_id = 1, role_id = 2, pass_word = "2", max_point = 5};
get_({1, 3}) -> 
  #csv_game_spot_role{game_id = 1, role_id = 3, pass_word = "3", max_point = 5};
get_({1, 4}) -> 
  #csv_game_spot_role{game_id = 1, role_id = 4, pass_word = "4", max_point = 5};
get_({1, 5}) -> 
  #csv_game_spot_role{game_id = 1, role_id = 5, pass_word = "5", max_point = 5};
get_({1, 6}) -> 
  #csv_game_spot_role{game_id = 1, role_id = 6, pass_word = "6", max_point = 5};
get_({1, 7}) -> 
  #csv_game_spot_role{game_id = 1, role_id = 7, pass_word = "7", max_point = 5};
get_({1, 8}) -> 
  #csv_game_spot_role{game_id = 1, role_id = 8, pass_word = "8", max_point = 5};
get_(_) ->
  err_csv_cfg_not_found.

get_field(Key, Field) ->
  get_field(Key, Field, err_csv_cfg_not_found).

get_field(Key, game_id, ErrCode) ->
  case csv_game_spot_role:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_game_spot_role.game_id};
    _Err -> ErrCode
  end;
get_field(Key, role_id, ErrCode) ->
  case csv_game_spot_role:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_game_spot_role.role_id};
    _Err -> ErrCode
  end;
get_field(Key, pass_word, ErrCode) ->
  case csv_game_spot_role:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_game_spot_role.pass_word};
    _Err -> ErrCode
  end;
get_field(Key, max_point, ErrCode) ->
  case csv_game_spot_role:get(Key, ErrCode) of
    {ok, Cfg} -> {ok, Cfg#csv_game_spot_role.max_point};
    _Err      -> ErrCode
  end.

get_all_keys() -> 
  [{1, 0}, {1, 1}, {1, 2}, {1, 3}, {1, 4}, {1, 5}, {1, 6}, {1, 7}, {1, 8}].

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
  {1, 8}.

dump() -> 
  Keys    = get_all_keys(),
  [io:format("~p~n", [get_(Key)]) || Key <- Keys],
  ok.