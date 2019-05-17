-ifndef(CSV_BUFF_HRL_FILE).
-define(CSV_BUFF_HRL_FILE, csv_buff_hrl_file).

-record(csv_buff, {type = undefined, is_sub_buff = false, element_property = undefined, sub_buffs = undefined, effects = undefined, trigger_time = undefined, cut_count_time = undefined}).

-endif.

-ifndef(CSV_EFFECT_HRL_FILE).
-define(CSV_EFFECT_HRL_FILE, csv_effect_hrl_file).

-record(csv_effect, {effect = undefined}).

-endif.

-ifndef(CSV_EQUIP_HRL_FILE).
-define(CSV_EQUIP_HRL_FILE, csv_equip_hrl_file).

-record(csv_equip, {id = 0, role_class = undefined, type = undefined, initial_element_property = undefined, magic_point_count = 0, magic_point_list = undefined, initial_magic_points_count = 0, magic_points_in_count_per_round = 0}).

-endif.

-ifndef(CSV_ROLE_HRL_FILE).
-define(CSV_ROLE_HRL_FILE, csv_role_hrl_file).

-record(csv_role, {type = undefined, race = undefined, hp = 0, cards_out_count_per_round = 0, speed = 0, initial_cards_count = 0, initial_assets = undefined, cards_in_count_per_round = 0, assets_in_per_round = undefined, cards_max_count_during_round = 0, cards_max_count_after_round = 0, cards_max_count_beyond_round = 0, assets_limit_during_round = undefined, assets_limit_after_round = undefined, assets_limit_beyond_round = undefined}).

-endif.

-ifndef(CSV_ROLE_RACE_HRL_FILE).
-define(CSV_ROLE_RACE_HRL_FILE, csv_role_race_hrl_file).

-record(csv_role_race, {type = undefined, skills = 0}).

-endif.

-ifndef(CSV_TARGET_HRL_FILE).
-define(CSV_TARGET_HRL_FILE, csv_target_hrl_file).

-record(csv_target, {type = undefined, is_include_self = false, is_single = false, is_falsene = false}).

-endif.

