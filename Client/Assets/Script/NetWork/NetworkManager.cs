using System.IO;
using UnityEngine;
using System;
using ProtoBuf;
using Common;
using easy_moba;
using System.Collections;

namespace NetWork
{
    public class NetworkManager : SingletonModule<NetworkManager>
    {
        private WebSocket web_socket;
        private bool login;

        private Guid guid = Guid.NewGuid();

        protected override void OnInit()
        {
            web_socket = new WebSocket(new Uri("ws://114.116.91.235:26666"));
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

        protected override void OnCleanup()
        {
            
        }
    }
}

