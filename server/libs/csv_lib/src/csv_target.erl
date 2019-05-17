-module(csv_target).

-include("csv_target.hrl").

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

get_(falsene) -> 
  #csv_target{type = falsene, is_include_self = false, is_single = false, is_falsene = true};
get_(self) -> 
  #csv_target{type = self, is_include_self = true, is_single = true, is_falsene = false};
get_(self_one) -> 
  #csv_target{type = self_one, is_include_self = true, is_single = true, is_falsene = false};
get_(self_all) -> 
  #csv_target{type = self_all, is_include_self = true, is_single = false, is_falsene = false};
get_(self_one_except_self) -> 
  #csv_target{type = self_one_except_self, is_include_self = false, is_single = true, is_falsene = false};
get_(self_all_except_self) -> 
  #csv_target{type = self_all_except_self, is_include_self = false, is_single = false, is_falsene = false};
get_(oppo_one) -> 
  #csv_target{type = oppo_one, is_include_self = false, is_single = true, is_falsene = false};
get_(oppo_all) -> 
  #csv_target{type = oppo_all, is_include_self = false, is_single = false, is_falsene = false};
get_(all) -> 
  #csv_target{type = all, is_include_self = true, is_single = false, is_falsene = false};
get_(all_except_self) -> 
  #csv_target{type = all_except_self, is_include_self = false, is_single = false, is_falsene = false};
get_(_) ->
  err_csv_cfg_not_found.

get_field(Key, Field) ->
  get_field(Key, Field, err_csv_cfg_not_found).

get_field(Key, type, ErrCode) ->
  case csv_target:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_target.type};
    _Err -> ErrCode
  end;
get_field(Key, is_include_self, ErrCode) ->
  case csv_target:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_target.is_include_self};
    _Err -> ErrCode
  end;
get_field(Key, is_single, ErrCode) ->
  case csv_target:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_target.is_single};
    _Err -> ErrCode
  end;
get_field(Key, is_falsene, ErrCode) ->
  case csv_target:get(Key, ErrCode) of
    {ok, Cfg} -> {ok, Cfg#csv_target.is_falsene};
    _Err      -> ErrCode
  end.

get_all_keys() -> 
  [falsene, self, self_one, self_all, self_one_except_self, self_all_except_self, oppo_one, oppo_all, all, all_except_self].

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
  all_except_self.

dump() -> 
  Keys    = get_all_keys(),
  [io:format("~p~n", [get_(Key)]) || Key <- Keys],
  ok.