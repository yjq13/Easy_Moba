%% -*- coding: utf-8 -*-
%% Automatically generated, do not edit
%% Generated by gpb_compile version 4.2.1

-ifndef(bt_down_pb).
-define(bt_down_pb, true).

-define(bt_down_pb_gpb_version, "4.2.1").

-ifndef('BT_DOWN_MSG_PB_H').
-define('BT_DOWN_MSG_PB_H', true).
-record(bt_down_msg,
        {svr_time = 0           :: non_neg_integer() | undefined, % = 1, 32 bits
         seq = 0                :: non_neg_integer() | undefined, % = 2, 32 bits
         err_code = undefined   :: bt_down_pb:reply_err_code() | undefined, % = 3
         room_id = 0            :: non_neg_integer() | undefined, % = 4, 32 bits
         init = undefined       :: bt_down_pb:reply_bt_init() | undefined, % = 5
         bt_seqs = []           :: [bt_down_pb:bt_seq()] | undefined % = 6
        }).
-endif.

-ifndef('REPLY_BT_INIT_PB_H').
-define('REPLY_BT_INIT_PB_H', true).
-record(reply_bt_init,
        {progresses = []        :: [{non_neg_integer(), non_neg_integer()}] | undefined % = 1
        }).
-endif.

-ifndef('DOWN_MSG_PB_H').
-define('DOWN_MSG_PB_H', true).
-record(down_msg,
        {svr_time = 0           :: non_neg_integer() | undefined, % = 1, 32 bits
         seq = 0                :: non_neg_integer() | undefined, % = 2, 32 bits
         err_code = undefined   :: bt_down_pb:reply_err_code() | undefined, % = 3
         notify = undefined     :: bt_down_pb:reply_notify() | undefined, % = 4
         extra_info = undefined :: bt_down_pb:reply_extra_info() | undefined, % = 5
         heartbeat = undefined  :: bt_down_pb:reply_heartbeat() | undefined, % = 6
         gm = undefined         :: bt_down_pb:reply_user() | undefined, % = 7
         sdk_login = undefined  :: bt_down_pb:reply_sdk_login() | undefined, % = 8
         login = undefined      :: bt_down_pb:reply_login() | undefined, % = 9
         reconnect = undefined  :: bt_down_pb:reply_reconnect() | undefined, % = 10
         tutorial = undefined   :: bt_down_pb:reply_tutorial() | undefined, % = 11
         settings = undefined   :: bt_down_pb:reply_settings() | undefined, % = 12
         data_op = undefined    :: bt_down_pb:reply_data_op() | undefined % = 99
        }).
-endif.

-ifndef('REPLY_USER_PB_H').
-define('REPLY_USER_PB_H', true).
-record(reply_user,
        {uid = 0                :: non_neg_integer() | undefined, % = 1, 32 bits
         name = []              :: iolist() | undefined, % = 2
         level = 0              :: non_neg_integer() | undefined, % = 3, 32 bits
         exp = 0                :: non_neg_integer() | undefined, % = 4, 32 bits
         red_dots = []          :: [bt_down_pb:red_dot()] | undefined, % = 5
         settings = []          :: [bt_down_pb:setting()] | undefined, % = 6
         tutorials = []         :: [non_neg_integer()] | undefined, % = 7, 32 bits
         lang = cn              :: 'cn' | 'en' | integer() | undefined, % = 8, enum t_lang
         time_zone = 0          :: integer() | undefined, % = 9, 32 bits
         avatar = []            :: iolist() | undefined, % = 10
         frame = 0              :: non_neg_integer() | undefined, % = 11, 32 bits
         gender = none          :: 'none' | 'male' | 'female' | integer() | undefined, % = 12, enum t_gender
         country = 0            :: non_neg_integer() | undefined, % = 13, 32 bits
         city = 0               :: non_neg_integer() | undefined, % = 14, 32 bits
         desc = []              :: iolist() | undefined, % = 15
         create_time = 0        :: non_neg_integer() | undefined % = 16, 32 bits
        }).
-endif.

-ifndef('REPLY_MSG_PB_H').
-define('REPLY_MSG_PB_H', true).
-record(reply_msg,
        {
        }).
-endif.

-ifndef('REPLY_ERR_CODE_PB_H').
-define('REPLY_ERR_CODE_PB_H', true).
-record(reply_err_code,
        {err_code = []          :: iolist() | undefined % = 1
        }).
-endif.

-ifndef('REPLY_NOTIFY_PB_H').
-define('REPLY_NOTIFY_PB_H', true).
-record(reply_notify,
        {red_dot = undefined    :: bt_down_pb:red_dot() | undefined % = 1
        }).
-endif.

-ifndef('REPLY_EXTRA_INFO_PB_H').
-define('REPLY_EXTRA_INFO_PB_H', true).
-record(reply_extra_info,
        {
        }).
-endif.

-ifndef('REPLY_HEARTBEAT_PB_H').
-define('REPLY_HEARTBEAT_PB_H', true).
-record(reply_heartbeat,
        {delay = 0              :: non_neg_integer() | undefined % = 1, 32 bits
        }).
-endif.

-ifndef('REPLY_SDK_LOGIN_PB_H').
-define('REPLY_SDK_LOGIN_PB_H', true).
-record(reply_sdk_login,
        {htoken = []            :: iolist() | undefined % = 1
        }).
-endif.

-ifndef('REPLY_LOGIN_PB_H').
-define('REPLY_LOGIN_PB_H', true).
-record(reply_login,
        {user = undefined       :: bt_down_pb:reply_user() | undefined, % = 1
         svr_time_zone = 0      :: non_neg_integer() | undefined, % = 2, 32 bits
         region = dev           :: 'dev' | integer() | undefined % = 3, enum t_region
        }).
-endif.

-ifndef('REPLY_RECONNECT_PB_H').
-define('REPLY_RECONNECT_PB_H', true).
-record(reply_reconnect,
        {notifies = []          :: [bt_down_pb:reply_notify()] | undefined % = 1
        }).
-endif.

-ifndef('REPLY_TUTORIAL_PB_H').
-define('REPLY_TUTORIAL_PB_H', true).
-record(reply_tutorial,
        {new_tutorial = undefined :: bt_down_pb:reply_new_tutorial() | undefined % = 1
        }).
-endif.

-ifndef('REPLY_NEW_TUTORIAL_PB_H').
-define('REPLY_NEW_TUTORIAL_PB_H', true).
-record(reply_new_tutorial,
        {result = success       :: 'success' | 'fail' | integer() | undefined, % = 1, enum t_result
         tutorials = 0          :: non_neg_integer() | undefined % = 2, 32 bits
        }).
-endif.

-ifndef('REPLY_SETTINGS_PB_H').
-define('REPLY_SETTINGS_PB_H', true).
-record(reply_settings,
        {result = success       :: 'success' | 'fail' | integer() | undefined % = 1, enum t_result
        }).
-endif.

-ifndef('REPLY_DATA_OP_PB_H').
-define('REPLY_DATA_OP_PB_H', true).
-record(reply_data_op,
        {result = success       :: 'success' | 'fail' | integer() | undefined, % = 1, enum t_result
         data = 0               :: non_neg_integer() | undefined % = 2, 32 bits
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

-ifndef('BT_ACTION_PB_H').
-define('BT_ACTION_PB_H', true).
-record(bt_action,
        {type = nothing         :: 'nothing' | 'move' | 'hit' | 'eat' | 'back_home' | integer() | undefined, % = 1, enum t_bt_action
         op_slot = 0            :: non_neg_integer() | undefined, % = 2, 32 bits
         op_param = 0           :: non_neg_integer() | undefined % = 3, 32 bits
        }).
-endif.

-ifndef('BT_CMD_PB_H').
-define('BT_CMD_PB_H', true).
-record(bt_cmd,
        {uid = 0                :: non_neg_integer() | undefined, % = 1, 32 bits
         client_seq = 0         :: non_neg_integer() | undefined, % = 2, 32 bits
         actions = []           :: [bt_down_pb:bt_action()] | undefined % = 3
        }).
-endif.

-ifndef('BT_SEQ_PB_H').
-define('BT_SEQ_PB_H', true).
-record(bt_seq,
        {seq = 0                :: non_neg_integer() | undefined, % = 1, 32 bits
         cmds = []              :: [bt_down_pb:bt_cmd()] | undefined % = 2
        }).
-endif.

-endif.
