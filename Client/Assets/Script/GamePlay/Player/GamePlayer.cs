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

        private GameBuffManager m_buffManager;

        public static GamePlayer GAME_MANAGER = null;

        public GamePlayer(RoleBase role,uint id)
        {
            PlayerID = id;
            Role = role;
            RoleType = Role.GetRoleType();
            m_buffManager = new GameBuffManager();
            m_buffManager.Init();
        }


        public void SendGameBuffTriggerEvent(Buff_NOTIFY_TYPE triggerType, params object[] data)
        {
            m_buffManager.DispatchBuffTrigger(triggerType, data);
        }

        // ---------------------------------------------------------
        public void GainBuff(int buff_id, uint buff_times)
        {
            // !!! 暂未实现
            OnGainBuff(buff_id, buff_times);
        }

        protected virtual void OnGainBuff(int buff_id, uint buff_times)
        {

        }

        // ---------------------------------------------------------
        // buff剩余次数
        public int GetBuffTimes(int buff_id)
        {
            // !!! 暂未实现
            return 0;
        }

        // ---------------------------------------------------------
        public void StoleRandomBuff(GamePlayer target_player, uint buff_cnt)
        {
            // !!! 暂未实现
            OnStoleRandomBuff(target_player, buff_cnt);
        }

        protected virtual void OnStoleRandomBuff(GamePlayer target_player, uint buff_cnt)
        {

        }


    }
}
