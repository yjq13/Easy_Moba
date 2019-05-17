-module(csv_equip).

-include("csv_equip.hrl").

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

get_(1) -> 
  #csv_equip{id = 1, role_class = magician, type = magic_wand, initial_element_property = metal, magic_point_count = 9, magic_point_list = ['metal,wood,water,fire,soil,metal,wood,water,fire'], initial_magic_points_count = 5, magic_points_in_count_per_round = 4};
get_(2) -> 
  #csv_equip{id = 2, role_class = magician, type = magic_wand, initial_element_property = soil, magic_point_count = 8, magic_point_list = ['soil,soil,metal,soil,wood,soil,fire,soil'], initial_magic_points_count = 4, magic_points_in_count_per_round = 4};
get_(_) ->
  err_csv_cfg_not_found.

get_field(Key, Field) ->
  get_field(Key, Field, err_csv_cfg_not_found).

get_field(Key, id, ErrCode) ->
  case csv_equip:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_equip.id};
    _Err -> ErrCode
  end;
get_field(Key, role_class, ErrCode) ->
  case csv_equip:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_equip.role_class};
    _Err -> ErrCode
  end;
get_field(Key, type, ErrCode) ->
  case csv_equip:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_equip.type};
    _Err -> ErrCode
  end;
get_field(Key, initial_element_property, ErrCode) ->
  case csv_equip:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_equip.initial_element_property};
    _Err -> ErrCode
  end;
get_field(Key, magic_point_count, ErrCode) ->
  case csv_equip:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_equip.magic_point_count};
    _Err -> ErrCode
  end;
get_field(Key, magic_point_list, ErrCode) ->
  case csv_equip:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_equip.magic_point_list};
    _Err -> ErrCode
  end;
get_field(Key, initial_magic_points_count, ErrCode) ->
  case csv_equip:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_equip.initial_magic_points_count};
    _Err -> ErrCode
  end;
get_field(Key, magic_points_in_count_per_round, ErrCode) ->
  case csv_equip:get(Key, ErrCode) of
    {ok, Cfg} -> {ok, Cfg#csv_equip.magic_points_in_count_per_round};
    _Err      -> ErrCode
  end.

get_all_keys() -> 
  [1, 2].

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
  2.

dump() -> 
  Keys    = get_all_keys(),
  [io:format("~p~n", [get_(Key)]) || Key <- Keys],
  ok.