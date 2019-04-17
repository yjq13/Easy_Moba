
using easy_moba;
using NetWork;

namespace Test
{
    class Test_up_msg_Driver : Test_NetWorkManager_Driver
    {
        public override void SendTestMessage()
        {
            {
                up_msg msg = new up_msg()
                {
                    data_op = new req_data_op()
                    {
                        op = t_op.add
                    }
                };
                NetworkManager.Instance.SendMessage(msg);
            }
        }
    }


    class Test_down_msg_Driver : Test_NetWorkManager_Driver
    {
        public override void SendTestMessage()
        {
            {
                down_msg msg = new down_msg()
                {
                    svr_ts = 1543483207,
                    seq = 0,
                    err_code = new reply_err_code()
                    {
                        err_code = "Yingjiaqi is your daddy"
                    }
                };
                NetworkManager.Instance.SendMessage(msg);
            }
        }
    }

}
