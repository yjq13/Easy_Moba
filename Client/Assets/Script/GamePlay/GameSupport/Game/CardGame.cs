using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace GamePlay
{
    public class CardGame: MyGameBase
    {
        private GamePlayer m_authorizationPlayer = null;
        private List<GamePlayer> m_gamePlayerList;
        private Dictionary<CampType, List<GamePlayer>> m_campPlayerList;
        private GamePlayer m_GamePlayer = null;
        private GameProgressManager m_prgressManager = null;

        public uint GetCurrentRoundCount()
        {
            return m_prgressManager.RoundCount;
        }

        public void StartNextProgress()
        {
            m_prgressManager.StartNextProgress();
        }

        public void JumpToLastProgress()
        {
            m_prgressManager.JumpToLastProgress();
        }

        public void SetMaxGameRoundCount()
        {

        }

        public GameProgressBase GetCurrenProgress()
        {
            return m_prgressManager.GetCurrenProgress();
        }

        public override void OnAwake()
        {
            base.OnAwake();
            GameType = GameType.CARD_GAME;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            CheckAuthorizationOperation();
        }

        private void CheckAuthorizationOperation()
        {
            if (m_authorizationPlayer == m_GamePlayer
                && GetCurrenProgress().GetProgressState() == ProgressType.RoundPlaying
                && GetCurrenProgress().GetProgressState() == ProgressType.ExtraPlayerOperation)
            {

            }
        }

        public void StartTargetGetProgress()
        {

        }

        public GamePlayer GetCurrentAuthorizationPlayer()
        {
            return m_authorizationPlayer;
        }

        public List<GamePlayer> GetAllGamePlayers()
        {
            return new List<GamePlayer>(m_gamePlayerList);
        }

        public void SetGameInfo(List<GamePlayer> players,GamePlayer myGamePlayer)
        {
            m_gamePlayerList = players;
            m_GamePlayer = myGamePlayer;
            foreach(var player in m_gamePlayerList)
            {
                List<GamePlayer> temp_list;
                if (m_campPlayerList.TryGetValue(player.CampType, out temp_list))
                {
                    temp_list.Add(player);
                }
                else
                {
                    temp_list = new List<GamePlayer>();
                    temp_list.Add(player);
                    m_campPlayerList.Add(player.CampType, temp_list);
                }
            }
            m_prgressManager = new GameProgressManager();
            m_prgressManager.Init();
            StartGame();
        }

        public void SetAuthorization(GamePlayer player)
        {
            m_authorizationPlayer = player;
        }

        public void GetBackAuthorization()
        {
            m_authorizationPlayer = GamePlayer.GAME_MANAGER;
        }

        private void StartGame()
        {
            m_prgressManager.StartProgress();
        }

        public bool CheckIsHappened(int probability)
        {
            int number = Random.Range(0,100);
            return number < probability;
        }

        public GamePlayer GetMyPlayer()
        {
            return m_GamePlayer;
        }

        public List<GamePlayer> GetSelfCampPlayers()
        {
            return GetGamePlayersByCamp(m_GamePlayer.CampType);
        }

        public List<GamePlayer> GetOppoCampPlayers()
        {
            return GetGamePlayersByCamp(GetOppoType());
        }

        public CampType GetOppoType()
        {
            if(m_GamePlayer.CampType == CampType.Angle)
            {
                return CampType.Devil;
            }
            else
            {
                return CampType.Angle;
            }
        }

        private List<GamePlayer> GetGamePlayersByCamp(CampType campType)
        {
            List<GamePlayer> list;
            m_campPlayerList.TryGetValue(campType,out list);
            return list;
        }
    }
}
