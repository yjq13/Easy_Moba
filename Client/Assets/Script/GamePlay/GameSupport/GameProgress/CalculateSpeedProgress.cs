using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;

namespace GamePlay
{
    public class CalculateSpeedProgress : InterfaceGameProgress
    {
        private List<Player> m_playerList;
        private const float RACE_LOAD_LENGTH = 100;
        private Dictionary<PlayerID, float> m_players_race_flag = new Dictionary<PlayerID, float>();
        private Dictionary<PlayerID, float> m_need_time_list = new Dictionary<PlayerID, float>();

        public void DuringProgress()
        {



        }

        private void CalculateSpeedEffect()
        {
            float temp_load = 0;
            float temp_min_time = 0;
            PlayerID get_round_id = 0;
            Player getRoundPlayer = null;
            foreach (var player in m_playerList)
            {
                temp_load = RACE_LOAD_LENGTH - m_players_race_flag[player.PlayerID];
                m_need_time_list[player.PlayerID] = temp_load / player.Role.Speed;
            }


            foreach (var number in m_need_time_list)
            { 
                if (number.Value == temp_min_time)
                {
                    get_round_id = number.Key;
                }
                else if(number.Value < temp_min_time)
                {
                    temp_min_time = number.Value;
                    get_round_id = number.Key;
                }
            }

            foreach(var player in m_playerList)
            {
                if(get_round_id == player.PlayerID)
                {
                    m_players_race_flag[player.PlayerID] = 0;
                    getRoundPlayer = player;
                }
                else
                {
                    m_need_time_list[player.PlayerID] += temp_min_time * player.Role.Speed;
                }
            }

            GameCardManager.Instance.SetAuthorization(getRoundPlayer);
        }

        public void EndProgress()
        {
            //暂时不知道要做啥
        }

        public void InitProgress(List<Player> playerList)
        {
            if(playerList != null)
            {
                m_playerList = playerList;
                foreach(var player in m_playerList)
                {
                    m_players_race_flag.Add(player.PlayerID, 0);
                    m_need_time_list.Add(player.PlayerID, 0);
                }
            }
            else
            {
                Debug.LogError("CalculateSpeedProgress:playerList is null,InitProgress faild");
            }
        }

        public void StartProgress()
        {
           //暂时不知道要做啥
        }
    }
}
