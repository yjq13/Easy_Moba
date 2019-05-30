using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using easy_moba;
using NetWork;
using System.Collections.Generic;
using System;

namespace Test
{
    public class Test_up_msg_Driver : Test_NetWorkManager_Driver
    {
        public override Type SendTestMessage()
        {
            up_msg msg = new up_msg()
            {
                data_op = new req_data_op()
                {
                    op = t_op.add
                }
            };
            NetworkManager.Instance.SendMessage(msg);

            return msg.GetType();
        }
    }

    //[Ignore("no use")]
    //public class Test_down_msg_Driver : Test_NetWorkManager_Driver
    //{
    //    public override Type SendTestMessage()
    //    {
    //        down_msg msg = new down_msg()
    //        {
    //            svr_ts = 1543483207,
    //            seq = 0,
    //            err_code = new reply_err_code()
    //            {
    //                err_code = "Yingjiaqi is your daddy"
    //            }
    //        };
    //        return msg.GetType();
    //    }
    //}

}
