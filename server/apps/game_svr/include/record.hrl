-record(acc, {uin, roles = [], create_ts = 0}).	%%	账号 roles	=	{svr_id, uid}
-record(usocket, {uid, pid, last_hb_time}).
-record(usocket_notify,{uid, notifies}).
-record(sdk_fl, {key}).
