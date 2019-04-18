using NetWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Test
{
    public static class NetWorkUtil
    {
        public static bool NetWorkConnect = false;
        public static int testCount = 0;
        public static IEnumerator NetWorkContextCreate()
        {
            if (NetWorkConnect)
            {
                yield return null;
            }
            yield return NetworkManager.Instance.StartConnect();
            List<Test_NetWorkManager_Driver> drivers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(item =>
item.GetTypes()
)
.Where(item =>
item.IsSubclassOf(typeof(Test_NetWorkManager_Driver))
)
.Select(type =>
(Test_NetWorkManager_Driver)Activator.CreateInstance(type)
)
.ToList();
            testCount = drivers.Count;
            NetWorkConnect = true;
        }


        public static void CheckAndCloseNetWorkContext()
        {
            testCount--;
            if (testCount <= 0)
            {
                return;
            }
            NetworkManager.Instance.CloseConnect();
            NetWorkConnect = false;
        }
    }
}
