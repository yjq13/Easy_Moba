-module(csv_role_race).

-include("csv_role_race.hrl").

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

get_(human) -> 
  #csv_role_race{type = human, skills = []};
get_(dwarf) -> 
  #csv_role_race{type = dwarf, skills = []};
get_(goblin) -> 
  #csv_role_race{type = goblin, skills = []};
get_(elf) -> 
  #csv_role_race{type = elf, skills = []};
get_(undead) -> 
  #csv_role_race{type = undead, skills = []};
get_(_) ->
  err_csv_cfg_not_found.

get_field(Key, Field) ->
  get_field(Key, Field, err_csv_cfg_not_found).

get_field(Key, type, ErrCode) ->
  case csv_role_race:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_role_race.type};
    _Err -> ErrCode
  end;
get_field(Key, skills, ErrCode) ->
  case csv_role_race:get(Key, ErrCode) of
    {ok, Cfg} -> {ok, Cfg#csv_role_race.skills};
    _Err      -> ErrCode
  end.

get_all_keys() -> 
  [human, dwarf, goblin, elf, undead].

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
  undead.

dump() -> 
  Keys    = get_all_keys(),
  [io:format("~p~n", [get_(Key)]) || Key <- Keys],
  ok.