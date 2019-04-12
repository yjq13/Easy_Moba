using Common;
using GamePlay;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Test
{
    class Test_ConfigDataManager_Driver :Interface_TestDriver
    {
        public void ClearContext()
        {
            ConfigDataManager.Instance.Cleanup();
        }

        public void InitContext()
        {
            ConfigDataManager.Instance.LoadCSV<RoleData>(ResourceIDDef.GAME_PLAYER_CONFIG);
        }

        public void Test()
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
