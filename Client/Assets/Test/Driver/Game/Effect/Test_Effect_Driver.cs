using Common;
using GamePlay;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Test
{
    abstract class Test_Effect_Driver : DriverBase
    {
        public List<GamePlayer> test_players;

        public override void ClearContext()
        {
            ConfigDataManager.Instance.Cleanup();
        }

        public override void InitContext()
        {
            ConfigDataManager.Instance.LoadCSV<RoleData>(ResourceIDDef.GAME_ROLE_CONFIG);
            test_players = null;
        }

        public override void Test()
        {
            InitPlayers();
        }

        public abstract void InitPlayers();
    }
}
