%% -*- coding: utf-8 -*-
%% Automatically generated, do not edit
%% Generated by gpb_compile version 4.2.1

-ifndef(game_pb).
-define(game_pb, true).

-define(game_pb_gpb_version, "4.2.1").

-ifndef('USER_PB_H').
-define('USER_PB_H', true).
-record(user,
        {uid = 0                :: non_neg_integer() | undefined, % = 1, 32 bits
         uin = []               :: iolist() | undefined, % = 2
         union_id = []          :: iolist() | undefined, % = 3
         svr_id = 0             :: non_neg_integer() | undefined, % = 4, 32 bits
         version = 0            :: non_neg_integer() | undefined, % = 5, 32 bits
         name = []              :: iolist() | undefined, % = 6
         level = 0              :: non_neg_integer() | undefined, % = 7, 32 bits
         exp = 0                :: non_neg_integer() | undefined, % = 8, 32 bits
         login_time = 0         :: non_neg_integer() | undefined, % = 9, 32 bits
         login_ip = []          :: iolist() | undefined, % = 10
         logout_time = 0        :: non_neg_integer() | undefined, % = 11, 32 bits
         update_time = undefined :: game_pb:update_time() | undefined, % = 12
         is_online = false      :: boolean() | 0 | 1 | undefined, % = 13
         red_dots = []          :: [game_pb:red_dot()] | undefined, % = 14
         settings = []          :: [game_pb:setting()] | undefined, % = 15
         tutorials = []         :: [non_neg_integer()] | undefined, % = 16, 32 bits
         lang = cn              :: 'cn' | 'en' | integer() | undefined, % = 17, enum t_lang
         time_zone = 0          :: integer() | undefined, % = 18, 32 bits
         avatar = []            :: iolist() | undefined, % = 19
         frame = 0              :: non_neg_integer() | undefined, % = 20, 32 bits
         gender = none          :: 'none' | 'male' | 'female' | integer() | undefined, % = 21, enum t_gender
         country = 0            :: non_neg_integer() | undefined, % = 22, 32 bits
         city = 0               :: non_neg_integer() | undefined, % = 23, 32 bits
         desc = []              :: iolist() | undefined, % = 24
         create_time = 0        :: non_neg_integer() | undefined, % = 25, 32 bits
         runtime_funs = []      :: [non_neg_integer()] | undefined % = 26, 32 bits
        }).
-endif.

-ifndef('UPDATE_TIME_PB_H').
-define('UPDATE_TIME_PB_H', true).
-record(update_time,
        {last_hourly_time = 0   :: non_neg_integer() | undefined, % = 1, 32 bits
         last_daily_time = 0    :: non_neg_integer() | undefined, % = 2, 32 bits
         last_weekly_time = 0   :: non_neg_integer() | undefined % = 3, 32 bits
        }).
-endif.

-ifndef('RED_DOT_PB_H').
-define('RED_DOT_PB_H', true).
-record(red_dot,
        {type = none            :: 'none' | integer() | undefined, % = 1, enum t_red_dot
         id = 0                 :: non_neg_integer() | undefined, % = 2, 32 bits
         start_time = 0         :: non_neg_integer() | undefined, % = 3, 32 bits
         expire_time = 0        :: non_neg_integer() | undefined % = 4, 32 bits
        }).
-endif.

-ifndef('SETTING_PB_H').
-define('SETTING_PB_H', true).
-record(setting,
        {key = sound            :: 'sound' | 'music' | integer() | undefined, % = 1, enum t_setting
         value = 0              :: non_neg_integer() | undefined % = 2, 32 bits
        }).
-endif.

-endif.
