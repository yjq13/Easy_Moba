-module(csv_buff).

-include("csv_buff.hrl").

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

get_(dry_weather) -> 
  #csv_buff{type = dry_weather, is_sub_buff = false, element_property = fire, sub_buffs = [], effects = [self, damage, 1, undefined], trigger_time = [get_damage], cut_count_time = [after_round]};
get_(fire_weapon) -> 
  #csv_buff{type = fire_weapon, is_sub_buff = false, element_property = fire, sub_buffs = [], effects = [self, extra_damage, 1], trigger_time = [cause_damage, fire], cut_count_time = [after_round]};
get_(fire_destroy) -> 
  #csv_buff{type = fire_destroy, is_sub_buff = false, element_property = fire, sub_buffs = [], effects = [self, multi_damage, 2], trigger_time = [cause_damage, fire], cut_count_time = [after_round]};
get_(spark_1) -> 
  #csv_buff{type = spark_1, is_sub_buff = false, element_property = fire, sub_buffs = [], effects = [oppo_all, damage, 1, self, gain_buff, spark_2, 1], trigger_time = [after_round], cut_count_time = [after_round]};
get_(spark_2) -> 
  #csv_buff{type = spark_2, is_sub_buff = false, element_property = fire, sub_buffs = [], effects = [oppo_all, damage, 2, self, gain_buff, spark_3, 1], trigger_time = [after_round], cut_count_time = [after_round]};
get_(spark_3) -> 
  #csv_buff{type = spark_3, is_sub_buff = false, element_property = fire, sub_buffs = [], effects = [oppo_all, damage, 3, undefined], trigger_time = [after_round], cut_count_time = [after_round]};
get_(ash_relive) -> 
  #csv_buff{type = ash_relive, is_sub_buff = false, element_property = fire, sub_buffs = [], effects = [self, relive, cur_damage, undefined], trigger_time = [die], cut_count_time = [do_effect]};
get_(endless_fire) -> 
  #csv_buff{type = endless_fire, is_sub_buff = false, element_property = fire, sub_buffs = [], effects = [self, stay_sp_element_property, fire, undefined], trigger_time = [use_card], cut_count_time = [after_round]};
get_(down_stream) -> 
  #csv_buff{type = down_stream, is_sub_buff = false, element_property = water, sub_buffs = [], effects = [self, change_speed_percentage, 50, undefined], trigger_time = [immediately], cut_count_time = [before_round]};
get_(water_rise) -> 
  #csv_buff{type = water_rise, is_sub_buff = false, element_property = water, sub_buffs = [], effects = [self, extra_damage, magic_point_cnt, water], trigger_time = [cause_damage], cut_count_time = [do_effect]};
get_(water_prison) -> 
  #csv_buff{type = water_prison, is_sub_buff = false, element_property = water, sub_buffs = [], effects = [self, skip_card_out, undefined, undefined], trigger_time = [card_out], cut_count_time = [do_effect]};
get_(water_coffin) -> 
  #csv_buff{type = water_coffin, is_sub_buff = false, element_property = water, sub_buffs = [skip_rounds, stay_alive], effects = [], trigger_time = [], cut_count_time = [before_round]};
get_(skip_round) -> 
  #csv_buff{type = skip_round, is_sub_buff = true, element_property = none, sub_buffs = [], effects = [self, skip_round, undefined, undefined], trigger_time = [before_round], cut_count_time = []};
get_(stay_alive) -> 
  #csv_buff{type = stay_alive, is_sub_buff = true, element_property = none, sub_buffs = [], effects = [self, stay_alive, undefined, undefined], trigger_time = [die], cut_count_time = []};
get_(against_stream) -> 
  #csv_buff{type = against_stream, is_sub_buff = false, element_property = water, sub_buffs = [], effects = [self, change_speed_percentage, -50, undefined], trigger_time = [immediately], cut_count_time = [before_round]};
get_(endless_spring) -> 
  #csv_buff{type = endless_spring, is_sub_buff = false, element_property = water, sub_buffs = [], effects = [self, draw_card, 1, undefined], trigger_time = [after_round], cut_count_time = [after_round]};
get_(into_the_earth) -> 
  #csv_buff{type = into_the_earth, is_sub_buff = false, element_property = solid, sub_buffs = [into_the_earth_ignore_damage, into_the_earth_rm_buff], effects = [], trigger_time = [], cut_count_time = [after_round]};
get_(into_the_earth_ignore_damage) -> 
  #csv_buff{type = into_the_earth_ignore_damage, is_sub_buff = true, element_property = none, sub_buffs = [], effects = [self, ignore_damage, undefined, undefined], trigger_time = [get_damage_except, water], cut_count_time = []};
get_(into_the_earth_rm_buff) -> 
  #csv_buff{type = into_the_earth_rm_buff, is_sub_buff = true, element_property = none, sub_buffs = [], effects = [self, clear_buff_left_times, into_the_earth, undefined], trigger_time = [get_damage, water], cut_count_time = []};
get_(greedy_for_solid) -> 
  #csv_buff{type = greedy_for_solid, is_sub_buff = false, element_property = solid, sub_buffs = [], effects = [self, transfer_just_acquired_asset, magic_point, solid], trigger_time = [gain_asset, solid], cut_count_time = [after_round]};
get_(solid_as_rock) -> 
  #csv_buff{type = solid_as_rock, is_sub_buff = false, element_property = solid, sub_buffs = [solid_as_rock_cut_damage, solid_as_rock_cut_speed], effects = [], trigger_time = [], cut_count_time = [before_round]};
get_(solid_as_rock_cut_damage) -> 
  #csv_buff{type = solid_as_rock_cut_damage, is_sub_buff = true, element_property = none, sub_buffs = [], effects = [self, multi_damage, 0, undefined], trigger_time = [get_damage], cut_count_time = []};
get_(solid_as_rock_cut_speed) -> 
  #csv_buff{type = solid_as_rock_cut_speed, is_sub_buff = true, element_property = none, sub_buffs = [], effects = [self, change_speed_cnt, -20, undefined, undefined], trigger_time = [immediately], cut_count_time = []};
get_(stubborn_stone) -> 
  #csv_buff{type = stubborn_stone, is_sub_buff = false, element_property = solid, sub_buffs = [], effects = [self, ignore_damage_buff, undefined, undefined, self, ignore_damage_debuff, undefined, undefined], trigger_time = [get_damage], cut_count_time = [after_round]};
get_(mudslide) -> 
  #csv_buff{type = mudslide, is_sub_buff = false, element_property = solid, sub_buffs = [], effects = [self, damage, 2, undefined, self, cut_just_acquired_asset, magic_point, solid], trigger_time = [gain_asset, solid], cut_count_time = [after_round]};
get_(retreat_and_multi) -> 
  #csv_buff{type = retreat_and_multi, is_sub_buff = false, element_property = wood, sub_buffs = [retreat_and_multi_cure], effects = [], trigger_time = [], cut_count_time = [after_round]};
get_(retreat_and_multi_cure) -> 
  #csv_buff{type = retreat_and_multi_cure, is_sub_buff = true, element_property = none, sub_buffs = [], effects = [self, cure, 2, undefined], trigger_time = [after_round], cut_count_time = []};
get_(retreat_and_multi_rm_buff) -> 
  #csv_buff{type = retreat_and_multi_rm_buff, is_sub_buff = true, element_property = none, sub_buffs = [], effects = [self, clear_buff_left_times, retreat_and_multi], trigger_time = [change_element_property_except, wood], cut_count_time = []};
get_(dry_and_lush_by_turns) -> 
  #csv_buff{type = dry_and_lush_by_turns, is_sub_buff = false, element_property = wood, sub_buffs = [dry_and_lush_by_turns_draw, dry_and_lush_by_turns_discard, dry_and_lush_by_turns_rm_buff], effects = [], trigger_time = [], cut_count_time = [after_round]};
get_(dry_and_lush_by_turns_draw) -> 
  #csv_buff{type = dry_and_lush_by_turns_draw, is_sub_buff = true, element_property = none, sub_buffs = [], effects = [self, draw_card, 1, undefined], trigger_time = [card_in], cut_count_time = []};
get_(dry_and_lush_by_turns_discard) -> 
  #csv_buff{type = dry_and_lush_by_turns_discard, is_sub_buff = true, element_property = none, sub_buffs = [], effects = [self, discard_card, 1], trigger_time = [card_out], cut_count_time = []};
get_(dry_and_lush_by_turns_rm_buff) -> 
  #csv_buff{type = dry_and_lush_by_turns_rm_buff, is_sub_buff = true, element_property = none, sub_buffs = [], effects = [self, clear_buff_left_times, dry_and_lush_by_turns], trigger_time = [change_element_property_except, wood], cut_count_time = []};
get_(plant_soldiers) -> 
  #csv_buff{type = plant_soldiers, is_sub_buff = false, element_property = wood, sub_buffs = [], effects = [self, ignore_damage, undefined, undefined], trigger_time = [get_damage, direct], cut_count_time = [do_effect]};
get_(wooden_cow) -> 
  #csv_buff{type = wooden_cow, is_sub_buff = false, element_property = wood, sub_buffs = [], effects = [self, multi_asset_gain, 2, undefined], trigger_time = [gain_asset, magic_point], cut_count_time = [before_round]};
get_(silence_is_gold) -> 
  #csv_buff{type = silence_is_gold, is_sub_buff = false, element_property = gold, sub_buffs = [], effects = [self, gain_asseet, gold, 1], trigger_time = [after_round], cut_count_time = [use_card]};
get_(seek_sword_in_boat) -> 
  #csv_buff{type = seek_sword_in_boat, is_sub_buff = false, element_property = gold, sub_buffs = [], effects = [self, seek_sword_in_boat, undefined, undefined], trigger_time = [before_round], cut_count_time = [undo_effect]};
get_(retire_from_battle) -> 
  #csv_buff{type = retire_from_battle, is_sub_buff = false, element_property = gold, sub_buffs = [], effects = [], trigger_time = [], cut_count_time = [after_round]};
get_(retire_from_battle_ignore_damage) -> 
  #csv_buff{type = retire_from_battle_ignore_damage, is_sub_buff = true, element_property = undefined, sub_buffs = [], effects = [self, ignore_damage, undefined, undefined], trigger_time = [get_damage, direct], cut_count_time = []};
get_(retire_from_battle_forbid_gold) -> 
  #csv_buff{type = retire_from_battle_forbid_gold, is_sub_buff = true, element_property = undefined, sub_buffs = [], effects = [self, forbid_sp_element_card, gold, undefined], trigger_time = [use_card], cut_count_time = []};
get_(_) ->
  err_csv_cfg_not_found.

get_field(Key, Field) ->
  get_field(Key, Field, err_csv_cfg_not_found).

get_field(Key, type, ErrCode) ->
  case csv_buff:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_buff.type};
    _Err -> ErrCode
  end;
get_field(Key, is_sub_buff, ErrCode) ->
  case csv_buff:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_buff.is_sub_buff};
    _Err -> ErrCode
  end;
get_field(Key, element_property, ErrCode) ->
  case csv_buff:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_buff.element_property};
    _Err -> ErrCode
  end;
get_field(Key, sub_buffs, ErrCode) ->
  case csv_buff:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_buff.sub_buffs};
    _Err -> ErrCode
  end;
get_field(Key, effects, ErrCode) ->
  case csv_buff:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_buff.effects};
    _Err -> ErrCode
  end;
get_field(Key, trigger_time, ErrCode) ->
  case csv_buff:get(Key) of
    {ok, Cfg} -> {ok, Cfg#csv_buff.trigger_time};
    _Err -> ErrCode
  end;
get_field(Key, cut_count_time, ErrCode) ->
  case csv_buff:get(Key, ErrCode) of
    {ok, Cfg} -> {ok, Cfg#csv_buff.cut_count_time};
    _Err      -> ErrCode
  end.

get_all_keys() -> 
  [dry_weather, fire_weapon, fire_destroy, spark_1, spark_2, spark_3, ash_relive, endless_fire, down_stream, water_rise, water_prison, water_coffin, skip_round, stay_alive, against_stream, endless_spring, into_the_earth, into_the_earth_ignore_damage, into_the_earth_rm_buff, greedy_for_solid, solid_as_rock, solid_as_rock_cut_damage, solid_as_rock_cut_speed, stubborn_stone, mudslide, retreat_and_multi, retreat_and_multi_cure, retreat_and_multi_rm_buff, dry_and_lush_by_turns, dry_and_lush_by_turns_draw, dry_and_lush_by_turns_discard, dry_and_lush_by_turns_rm_buff, plant_soldiers, wooden_cow, silence_is_gold, seek_sword_in_boat, retire_from_battle, retire_from_battle_ignore_damage, retire_from_battle_forbid_gold].

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
  retire_from_battle_forbid_gold.

dump() -> 
  Keys    = get_all_keys(),
  [io:format("~p~n", [get_(Key)]) || Key <- Keys],
  ok.