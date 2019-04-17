using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using easy_moba;
using NetWork;
using System.Collections.Generic;

namespace Test
{
    public abstract class Test_NetWorkManager_Driver : UnityDriverBase
    {
        public static int frameForWait_ForNetWork = 300;

        public override IEnumerator ClearContext()
        {
            NetworkManager.Instance.CloseConnect();
            yield return null;
        }

        public override IEnumerator InitContext()
        {
            yield return NetworkManager.Instance.StartConnect();
        }

        public override IEnumerator Test()
        {
            SendTestMessage();
            int frameForWait = frameForWait_ForNetWork;
            bool isOk = false;
            while (frameForWait > 0)
            {
                var data = NetworkManager.Instance.ReceiveMessage();
                if (data != null)
                {
                    isOk = true;
                    break;
                }
                yield return null;
                frameForWait--;
            }
            Assert.IsTrue(isOk,"Connect is failed");
        }

        public abstract void SendTestMessage();
    }
}
