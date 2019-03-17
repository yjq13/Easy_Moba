using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace GamePlay
{
    public class GameManager : SingletonModule<GameManager>
    {
        private GamePlayer m_authorizationPlayer = null;
        private List<GamePlayer> m_gamePlayerList;


        protected override void OnInit()
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

        public void SetGameInfo(List<GamePlayer> players)
        {
            m_gamePlayerList = players;
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
            
        }

        public bool CheckIsHappened(int probability)
        {
            int number = Random.Range(0,100);
            return number < probability;
        }

        protected override void OnCleanup()
        {
            
        }
    }
}
