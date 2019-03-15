using System.Collections;
using Common;
using UnityEngine;

namespace GamePlay
{
    public class GameCardManager : SingletonModule<GameCardManager>
    {
        private Player m_authorizationPlayer = null;
        public Player GetCurrentAuthorizationPlayer()
        {
            return m_authorizationPlayer;
        }

        protected override void OnInit()
        {

        }

        public void SetAuthorization(Player player)
        {
            m_authorizationPlayer = player;
        }

        public void GetBackAuthorization()
        {
            m_authorizationPlayer = null;
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
