using Common;
using GamePlay;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Test
{
    class Test_ConfigDataManager_Driver : DriverBase
    {
        public override void ClearContext()
        {
            ConfigDataManager.Instance.Cleanup();
        }

        public override void InitContext()
        {
            ConfigDataManager.Instance.LoadCSV<RoleData>(ResourceIDDef.GAME_PLAYER_CONFIG);
        }

        public override void Test()
        {
            List<CSVBaseData> dataList = ConfigDataManager.Instance.GetDataList<RoleData>();
            Assert.IsNotNull(dataList);
            Assert.IsTrue(dataList != null && dataList.Count > 0);
            uint id = 1;
            RoleData data = ConfigDataManager.Instance.GetData<RoleData>(id.ToString());
            Assert.IsNotNull(data);
            Assert.IsTrue(data != null && data.Speed == 30f);
            id = 2;
            data = ConfigDataManager.Instance.GetData<RoleData>(id.ToString());
            Assert.IsNotNull(data);
            Assert.IsTrue(data != null && data.Speed == 20f);
        }
    }
}
