
using System.Collections.Generic;
using GamePlay;
using UnityEditor;
using UnityEngine;

namespace Test
{
    class AutoTestLancher
    {
        public static List<Interface_TestDriver> driverList = new List<Interface_TestDriver>();
        [MenuItem("Tools/AutoTest")]
        public static void TestLancherStart()
        {
            driverList.Clear();
            AddDirvers();
            foreach (var test in driverList)
            {
                test.Test();
            }
        }


        public static void AddDirvers()
        {
            driverList.Add(new Test_ConfigDataManager_Driver());
        }
    }
}
