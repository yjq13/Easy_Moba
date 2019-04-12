
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
                test.InitContext();
                test.Test();
                test.ClearContext();
            }

            Debug.Log("Auto test is finished! please check the error and fix it");
        }


        public static void AddDirvers()
        {
            driverList.Add(new Test_ConfigDataManager_Driver());
            driverList.Add(new Test_CalculateSpeedProgress_Driver());
            driverList.Add(new Test_AllProgress_Driver());
        }
    }
}
