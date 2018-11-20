using System.IO;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;
using System.Text;
using ProtoBuf;
using Common;
using easy_moba;

namespace NetWork
{
    public class NetworkManager : SingleMode<NetworkManager>
    {
        private SocketIOComponent socketIO;
        private bool login;

        private Guid guid = Guid.NewGuid();

        protected override void OnInit()
        {

        }

        public void InitComponent(GameObject go)
        {
            socketIO = go.AddComponent<SocketIOComponent>();
        }

        public void Connect()
        {
            socketIO.Connect();
            up_msg msg = new up_msg()
            {
                data_op = new req_data_op()
                {
                    op = t_op.add,
                    data = 30,
                    data_list = new uint[3] { 1, 2, 3 }
                }
            };
            byte[] databytes = Encoding.Default.GetBytes(msg.ToString());

            socketIO.Emit(databytes);
            /*socketIO.On(SocketIOProtocol.ProtocolLogin, (date) =>
            {
                JsonData jsonData = JsonMapper.ToObject(date.data.ToString());
                string message = string.Format("{0} : {1}", jsonData["nickName"], jsonData["chatMessage"]);
                chatContent += message + "\r\n";
                login = true;
            });

            socketIO.On(SocketIOProtocol.ProtocolChat, (date) =>
            {
                JsonData jsonData = JsonMapper.ToObject(date.data.ToString());
                string message = string.Format("{0} : {1}", jsonData["nickName"], jsonData["chatMessage"]);
                chatContent += message + "\r\n";
            });

            socketIO.On(SocketIOProtocol.ProtocolInfo, (date) =>
            {
                Debug.Log(date.data);
            });*/
        }

        public void TestOpen(SocketIOEvent e)
        {
            Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
        }


        void OnRemoveEvent()
        {
            //socketIO.Off(SocketIOProtocol.ProtocolLogin);
            //socketIO.Off(SocketIOProtocol.ProtocolChat);
            //socketIO.Off(SocketIOProtocol.ProtocolInfo);
        }

    }
}

