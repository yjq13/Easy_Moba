using Common;
using GamePlay;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Test
{
    class Test_ConfigDataManager_Driver :Interface_TestDriver
    {
        public void Test()
        {
            ConfigDataManager.Instance.LoadCSV<RoleData>(ResourceIDDef.GAME_PLAYER_CONFIG);
            List<CSVBaseData> dataList = ConfigDataManager.Instance.GetDataList<RoleData>();
            Assert.IsNotNull(dataList);
            Assert.IsTrue(dataList != null && dataList.Count > 0);
            uint id = 1;
            RoleData data = ConfigDataManager.Instance.GetData<RoleData>(id.ToString());
            Assert.IsNotNull(data);
            Assert.IsTrue(data != null && data.Speed == 50);
            ConfigDataManager.Instance.Cleanup();
        }
    }
}
