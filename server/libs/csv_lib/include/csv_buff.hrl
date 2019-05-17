-ifndef(CSV_BUFF_HRL_FILE).
-define(CSV_BUFF_HRL_FILE, csv_buff_hrl_file).

-record(csv_buff, {type = undefined, is_sub_buff = false, element_property = undefined, sub_buffs = undefined, effects = undefined, trigger_time = undefined, cut_count_time = undefined}).

-endif.

