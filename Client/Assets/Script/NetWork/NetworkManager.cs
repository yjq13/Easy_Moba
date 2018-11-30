using System.IO;
using UnityEngine;
using System;
using ProtoBuf;
using Common;
using easy_moba;
using System.Collections;

namespace NetWork
{
    public class NetworkManager : SingleMode<NetworkManager>
    {
        private WebSocket web_socket;
        private bool login;

        private Guid guid = Guid.NewGuid();

        protected override void OnInit()
        {
            web_socket = new WebSocket(new Uri("ws://114.116.91.235:30000"));
        }


        public IEnumerator StartConnect()
        {
            return web_socket.Connect();
        }

        public void SendMessage<T>(T msg)
        {
            MemoryStream memory_stream = new MemoryStream();
            Serializer.Serialize(memory_stream, msg);
            byte[] binary_msg = new byte[memory_stream.Length];
            memory_stream.Position = 0;
            memory_stream.Read(binary_msg, 0, binary_msg.Length);

            web_socket.Send(binary_msg);
        }

        //public void Connect()
        //{
        //    down_msg msg = new down_msg()
        //    {
        //        svr_ts = 1543483207,
        //        seq = 0,
        //        err_code = new reply_err_code()
        //        {
        //            err_code = "Yingjiaqi is your daddy"
        //        }
        //    };
        //    up_msg msg2 = new up_msg()
        //    {
        //        data_op = new req_data_op()
        //        {
        //            op = t_op.add
        //        }
        //    };
        //    MemoryStream ms = new MemoryStream();
        //    Serializer.Serialize(ms, msg2);

        //    byte[] result = new byte[ms.Length];
        //    ms.Position = 0;
        //    ms.Read(result, 0, result.Length);
        //    for(int i = 0; i < result.Length; i++)
        //    {
        //        Debug.Log(result[i]);
        //    }

        //    byte[] test_result = new byte[] { 8, 199, 222, 254, 223, 5, 26, 19, 10, 17, 119, 104, 111, 32, 105, 115, 32, 121, 111, 117, 114, 32, 100, 97, 100, 100, 121 };

        //    MemoryStream ms2 = new MemoryStream(result);

        //    up_msg result2 = Serializer.Deserialize<up_msg>(ms2);

        //    web_socket.Send(result);

        //}

        public down_msg ReceiveMessage()
        {
            byte[] recv_msg = web_socket.Recv();
            if(recv_msg != null)
            {
                MemoryStream memory_stream = new MemoryStream(recv_msg);
                down_msg rec_message = Serializer.Deserialize<down_msg>(memory_stream);
                return rec_message;
            }
            return null;
        }



    }
}

