using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GamePlay
{
    public enum CampType
    {
        Angle = 0,
        Devil = 1
    }

    public class GamePlayer
    {
        public RoleBase Role;
        public RoleType RoleType;
        public PlayerID PlayerID;
        public CampType CampType;

        public static GamePlayer GAME_MANAGER = null;

        public GamePlayer(RoleBase role,uint id)
        {
            PlayerID = id;
            Role = role;
            RoleType = Role.GetRoleType();
        }
    }
}
