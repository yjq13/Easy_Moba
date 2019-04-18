using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using easy_moba;
using NetWork;

using System;
using System.Linq;
using System.Collections.Generic;

namespace Test
{
    public abstract class Test_NetWorkManager_Driver : UnityDriverBase
    {
        private static int frameForWait_ForNetWork = 300;

        public override IEnumerator ClearContext()
        {
            NetWorkUtil.CheckAndCloseNetWorkContext();
            yield return null;
        }

        public override IEnumerator InitContext()
        {
            yield return NetWorkUtil.NetWorkContextCreate();
        }

        public override IEnumerator Test()
        {
            Type driverType = SendTestMessage();
            yield return CheckMsgResult(driverType);
        }

        public abstract Type SendTestMessage();
           

        public IEnumerator CheckMsgResult(Type driver)
        {
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
            Assert.IsTrue(isOk, "Connect is failed: " + driver.Name);
        }
    }
}
