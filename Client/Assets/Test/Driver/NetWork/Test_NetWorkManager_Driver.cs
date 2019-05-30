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
        private static int FrameForWait_ForNetWork = 300;

        public override void ClearContext()
        {
            NetWorkUtil.CheckAndCloseNetWorkContext();
        }

        public override void InitContext()
        {

        }

        public override IEnumerator Test()
        {
            if (!NetWorkUtil.NetWorkConnect)
            {
                yield return NetWorkUtil.NetWorkContextCreate();
            }
            Type driverType = SendTestMessage();
            yield return CheckMsgResult(driverType);
        }

        public abstract Type SendTestMessage();
           
        public IEnumerator CheckMsgResult(Type driver)
        {
            int frameForWait = FrameForWait_ForNetWork;
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
