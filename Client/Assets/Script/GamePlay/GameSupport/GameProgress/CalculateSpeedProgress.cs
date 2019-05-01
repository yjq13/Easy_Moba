using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;

namespace GamePlay
{
    public class CalculateSpeedProgress : GameProgressBase
    {
        private const float RACE_LOAD_LENGTH = 100;
        private Dictionary<PlayerID, float> m_players_race_flag = new Dictionary<PlayerID, float>();
        private Dictionary<PlayerID, float> m_need_time_list = new Dictionary<PlayerID, float>();
        private GamePlayer m_getRoundPlayer = null;

        public void FirstStartProgress()
        {
            StartProgress();
        }

        private void CalculateSpeedEffect()
        {
            float temp_load = 0;
            float temp_min_time = float.MaxValue;

            PlayerID get_round_id = 0;
            GamePlayer getRoundPlayer = null;
            List<GamePlayer> PlayerList = GameFacade.GetCurrentCardGame().GetAllGamePlayers();
            m_need_time_list.Clear();


            foreach (var player in PlayerList)
            {
                if (m_players_race_flag[player.PlayerID] >= 100)
                {
                    m_players_race_flag[player.PlayerID] = 0;
                    getRoundPlayer = player;

                    m_getRoundPlayer = getRoundPlayer;
                    SetProgressEnd();
                    return;
                }

                temp_load = RACE_LOAD_LENGTH - m_players_race_flag[player.PlayerID];
                m_need_time_list.Add(player.PlayerID,temp_load / player.Role.CurrentSpeed);
            }


            foreach (var number in m_need_time_list)
            {
                if(number.Value <= temp_min_time)
                {
                    temp_min_time = number.Value;
                    get_round_id = number.Key;
                }
            }

            foreach(var player in PlayerList)
            {
                if(get_round_id == player.PlayerID)
                {
                    m_players_race_flag[player.PlayerID] = 0;
                    getRoundPlayer = player;
                }
                else
                {
                    m_players_race_flag[player.PlayerID] += temp_min_time * player.Role.CurrentSpeed;
                }
            }

            m_getRoundPlayer = getRoundPlayer;
            SetProgressEnd();
        }

        public override void OnEndProgress()
        {
            GameFacade.GetCurrentCardGame().SetAuthorization(m_getRoundPlayer);
        }

        public override void OnInitProgress()
        {
            if (GameFacade.GetCurrentCardGame().GetAllGamePlayers() != null)
            {
                foreach(var player in GameFacade.GetCurrentCardGame().GetAllGamePlayers())
                {
                    m_players_race_flag.Add(player.PlayerID, 0);
                }
            }
            else
            {
                Debug.LogError("CalculateSpeedProgress:playerList is null,InitProgress faild");
            }
        }

        public override void OnStartProgress()
        {
            if (GameFacade.GetCurrentCardGame().GetCurrentAuthorizationPlayer() == GamePlayer.GAME_MANAGER)
            {
                CalculateSpeedEffect();
            }
        }

        public override void OnClearGameProgress()
        {
            m_players_race_flag.Clear();
            m_need_time_list.Clear();
            m_getRoundPlayer = null;
        }

        public override ProgressType GetProgressState()
        {
            return ProgressType.CalculateSpeed;
        }
    }
}
