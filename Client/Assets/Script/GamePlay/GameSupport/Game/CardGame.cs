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
        private GamePlayer m_GamePlayer = null;

        public uint GetCurrentRoundCount()
        {
            return GameProgressManager.Instance.RoundCount;
        }

        public void SetMaxGameRoundCount()
        {

        }

        public GameProgressBase GetCurrenProgress()
        {
            return GameProgressManager.Instance.GetCurrenProgress();
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
            GameProgressManager.Instance.StartProgress();
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
    }
}
