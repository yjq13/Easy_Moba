%% -*- coding: utf-8 -*-
%% Automatically generated, do not edit
%% Generated by gpb_compile version 4.2.1

-ifndef(bt_up_pb).
-define(bt_up_pb, true).

-define(bt_up_pb_gpb_version, "4.2.1").

-ifndef('BT_UP_MSG_PB_H').
-define('BT_UP_MSG_PB_H', true).
-record(bt_up_msg,
        {room_id = 0            :: non_neg_integer() | undefined, % = 1, 32 bits
         uid = 0                :: non_neg_integer() | undefined, % = 2, 32 bits
         token = []             :: iolist() | undefined, % = 3
         init = undefined       :: bt_up_pb:req_bt_init() | undefined, % = 4
         cmd = undefined        :: bt_up_pb:req_bt_cmd() | undefined, % = 5
         query_cmd = undefined  :: bt_up_pb:req_bt_query_cmd() | undefined % = 6
        }).
-endif.

-ifndef('REQ_BT_INIT_PB_H').
-define('REQ_BT_INIT_PB_H', true).
-record(req_bt_init,
        {progress = 0           :: non_neg_integer() | undefined % = 1, 32 bits
        }).
-endif.

-ifndef('REQ_BT_CMD_PB_H').
-define('REQ_BT_CMD_PB_H', true).
-record(req_bt_cmd,
        {client_seq = 0         :: non_neg_integer() | undefined, % = 1, 32 bits
         actions = []           :: [bt_up_pb:bt_action()] | undefined % = 2
        }).
-endif.

-ifndef('REQ_BT_QUERY_CMD_PB_H').
-define('REQ_BT_QUERY_CMD_PB_H', true).
-record(req_bt_query_cmd,
        {seq_from = 0           :: non_neg_integer() | undefined, % = 1, 32 bits
         seq_to = 0             :: non_neg_integer() | undefined % = 2, 32 bits
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
         actions = []           :: [bt_up_pb:bt_action()] | undefined % = 3
        }).
-endif.

-ifndef('BT_SEQ_PB_H').
-define('BT_SEQ_PB_H', true).
-record(bt_seq,
        {seq = 0                :: non_neg_integer() | undefined, % = 1, 32 bits
         cmds = []              :: [bt_up_pb:bt_cmd()] | undefined % = 2
        }).
-endif.

-endif.
