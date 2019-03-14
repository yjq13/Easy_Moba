using System.Collections;
using Common;
using UnityEngine;

namespace GamePlay
{
    public class GameCardManager : SingletonModule<GameCardManager>
    {

        protected override void OnInit()
        {

        }

        private void SetAuthorization(RoleBase player)
        {
            
        }

        private void GetBackAuthorization()
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
