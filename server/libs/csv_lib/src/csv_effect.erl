-module(csv_effect).

-include("csv_effect.hrl").

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

get_(damage) -> 
  #csv_effect{effect = damage};
get_(extra_damage) -> 
  #csv_effect{effect = extra_damage};
get_(multi_damage) -> 
  #csv_effect{effect = multi_damage};
get_(repeat_damage) -> 
  #csv_effect{effect = repeat_damage};
get_(cure) -> 
  #csv_effect{effect = cure};
get_(relive) -> 
  #csv_effect{effect = relive};
get_(stay_sp_element_property) -> 
  #csv_effect{effect = stay_sp_element_property};
get_(change_speed_cnt) -> 
  #csv_effect{effect = change_speed_cnt};
get_(change_speed_percentage) -> 
  #csv_effect{effect = change_speed_percentage};
get_(skip_card_out) -> 
  #csv_effect{effect = skip_card_out};
get_(skip_round) -> 
  #csv_effect{effect = skip_round};
get_(stay_alive) -> 
  #csv_effect{effect = stay_alive};
get_(redirect_to_sp_card) -> 
  #csv_effect{effect = redirect_to_sp_card};
get_(replay_last_card_cur_round) -> 
  #csv_effect{effect = replay_last_card_cur_round};
get_(draw_card) -> 
  #csv_effect{effect = draw_card};
get_(gain_sp_card) -> 
  #csv_effect{effect = gain_sp_card};
get_(discard_card) -> 
  #csv_effect{effect = discard_card};
get_(ignore_damage) -> 
  #csv_effect{effect = ignore_damage};
get_(gain_buff) -> 
  #csv_effect{effect = gain_buff};
get_(clear_buff_left_times) -> 
  #csv_effect{effect = clear_buff_left_times};
get_(ignore_damage_buff) -> 
  #csv_effect{effect = ignore_damage_buff};
get_(ignore_damage_debuff) -> 
  #csv_effect{effect = ignore_damage_debuff};
get_(stole_random_buff) -> 
  #csv_effect{effect = stole_random_buff};
get_(cut_just_acquired_asset) -> 
  #csv_effect{effect = cut_just_acquired_asset};
get_(transfer_just_acquired_asset) -> 
  #csv_effect{effect = transfer_just_acquired_asset};
get_(multi_asset_gain) -> 
  #csv_effect{effect = multi_asset_gain};
get_(gain_asset) -> 
  #csv_effect{effect = gain_asset};
get_(clear_asset) -> 
  #csv_effect{effect = clear_asset};
get_(stole_asset_if_not_have) -> 
  #csv_effect{effect = stole_asset_if_not_have};
get_(sword_sink) -> 
  #csv_effect{effect = sword_sink};
get_(seek_sword_in_boat) -> 
  #csv_effect{effect = seek_sword_in_boat};
get_(_) ->
  err_csv_cfg_not_found.

get_field(Key, Field) ->
  get_field(Key, Field, err_csv_cfg_not_found).

get_field(Key, effect, ErrCode) ->
  case csv_effect:get(Key, ErrCode) of
    {ok, Cfg} -> {ok, Cfg#csv_effect.effect};
    _Err      -> ErrCode
  end.

get_all_keys() -> 
  [damage, extra_damage, multi_damage, repeat_damage, cure, relive, stay_sp_element_property, change_speed_cnt, change_speed_percentage, skip_card_out, skip_round, stay_alive, redirect_to_sp_card, replay_last_card_cur_round, draw_card, gain_sp_card, discard_card, ignore_damage, gain_buff, clear_buff_left_times, ignore_damage_buff, ignore_damage_debuff, stole_random_buff, cut_just_acquired_asset, transfer_just_acquired_asset, multi_asset_gain, gain_asset, clear_asset, stole_asset_if_not_have, sword_sink, seek_sword_in_boat].

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
  seek_sword_in_boat.

dump() -> 
  Keys    = get_all_keys(),
  [io:format("~p~n", [get_(Key)]) || Key <- Keys],
  ok.