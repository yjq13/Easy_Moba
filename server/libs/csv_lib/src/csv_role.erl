-module(csv_role).

-include("csv_role.hrl").

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

get_(magician) -> 
  #csv_role{type = magician, race = [human, dwarf, goblin, elf, undead], hp = 80, cards_out_count_per_round = 4, speed = 100, initial_cards_count = 3, initial_assets = [], cards_in_count_per_round = 1, assets_in_per_round = [], cards_max_count_during_round = 10, cards_max_count_after_round = 8, cards_max_count_beyond_round = 10, assets_limit_during_round = [{asset,bt_asset,magic_point,0}, {asset,bt_asset,action_point,0}], assets_limit_after_round = [{asset,bt_asset,magic_point,0}, {asset,bt_asset,action_point,0}], assets_limit_beyond_round = [{asset,bt_asset,magic_point,0}, {asset,bt_asset,action_point,0}]};
get_(archer) -> 
  #csv_role{type = archer, race = [], hp = 100, cards_out_count_per_round = 2, speed = 120, initial_cards_count = 0, initial_assets = [], cards_in_count_per_round = 1, assets_in_per_round = [{asset,bt_asset,action_point,1}], cards_max_count_during_round = 10, cards_max_count_after_round = 4, cards_max_count_beyond_round = 10, assets_limit_during_round = [{asset,bt_asset,magic_point,0}, {asset,bt_asset,action_point,999}], assets_limit_after_round = [{asset,bt_asset,magic_point,0}, {asset,bt_asset,action_point,999}], assets_limit_beyond_round = [{asset,bt_asset,magic_point,0}, {asset,bt_asset,action_point,999}]};
get_(soldier) -> 
  #csv_role{type = soldier, race = [], hp = 120, cards_out_count_per_round = 2, speed = 80, initial_cards_count = 0, initial_assets = [], cards_in_count_per_round = 0, assets_in_per_round = [], cards_max_count_during_round = 0, cards_max_count_after_round = 0, cards_max_count_beyond_round = 0, assets_limit_during_round = [], assets_limit_after_round = [], assets_limit_beyond_round = []};
get_(_) ->
  err_csv_cfg_not_found.

get_field(Key, Field) ->
  get_field(Key, Field, err_csv_cfg_not_found).

get_field(Key, type, ErrCode) ->
  case csv_role:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_role.type};
    _Err -> ErrCode
  end;
get_field(Key, race, ErrCode) ->
  case csv_role:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_role.race};
    _Err -> ErrCode
  end;
get_field(Key, hp, ErrCode) ->
  case csv_role:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_role.hp};
    _Err -> ErrCode
  end;
get_field(Key, cards_out_count_per_round, ErrCode) ->
  case csv_role:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_role.cards_out_count_per_round};
    _Err -> ErrCode
  end;
get_field(Key, speed, ErrCode) ->
  case csv_role:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_role.speed};
    _Err -> ErrCode
  end;
get_field(Key, initial_cards_count, ErrCode) ->
  case csv_role:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_role.initial_cards_count};
    _Err -> ErrCode
  end;
get_field(Key, initial_assets, ErrCode) ->
  case csv_role:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_role.initial_assets};
    _Err -> ErrCode
  end;
get_field(Key, cards_in_count_per_round, ErrCode) ->
  case csv_role:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_role.cards_in_count_per_round};
    _Err -> ErrCode
  end;
get_field(Key, assets_in_per_round, ErrCode) ->
  case csv_role:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_role.assets_in_per_round};
    _Err -> ErrCode
  end;
get_field(Key, cards_max_count_during_round, ErrCode) ->
  case csv_role:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_role.cards_max_count_during_round};
    _Err -> ErrCode
  end;
get_field(Key, cards_max_count_after_round, ErrCode) ->
  case csv_role:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_role.cards_max_count_after_round};
    _Err -> ErrCode
  end;
get_field(Key, cards_max_count_beyond_round, ErrCode) ->
  case csv_role:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_role.cards_max_count_beyond_round};
    _Err -> ErrCode
  end;
get_field(Key, assets_limit_during_round, ErrCode) ->
  case csv_role:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_role.assets_limit_during_round};
    _Err -> ErrCode
  end;
get_field(Key, assets_limit_after_round, ErrCode) ->
  case csv_role:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_role.assets_limit_after_round};
    _Err -> ErrCode
  end;
get_field(Key, assets_limit_beyond_round, ErrCode) ->
  case csv_role:get(Key, ErrCode) of
    {ok, Cfg} -> {ok, Cfg#csv_role.assets_limit_beyond_round};
    _Err      -> ErrCode
  end.

get_all_keys() -> 
  [magician, archer, soldier].

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
  soldier.

dump() -> 
  Keys    = get_all_keys(),
  [io:format("~p~n", [get_(Key)]) || Key <- Keys],
  ok.