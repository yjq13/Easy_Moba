using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GamePlay
{
    public class GamePlayer
    {
        public RoleBase Role;
        public RoleType RoleType;
        public PlayerID PlayerID;
        static uint id_index_for_test = 1;

        public static GamePlayer GAME_MANAGER = null;

        public GamePlayer(RoleBase role)
        {
            PlayerID = id_index_for_test++;
            Role = role;
            RoleType = Role.GetRoleType();
        }
    }
}
