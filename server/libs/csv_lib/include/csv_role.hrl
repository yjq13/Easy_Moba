-ifndef(CSV_ROLE_HRL_FILE).
-define(CSV_ROLE_HRL_FILE, csv_role_hrl_file).

-record(csv_role, {type = undefined, race = undefined, hp = 0, cards_out_count_per_round = 0, speed = 0, initial_cards_count = 0, initial_assets = undefined, cards_in_count_per_round = 0, assets_in_per_round = undefined, cards_max_count_during_round = 0, cards_max_count_after_round = 0, cards_max_count_beyond_round = 0, assets_limit_during_round = undefined, assets_limit_after_round = undefined, assets_limit_beyond_round = undefined}).

-endif.

