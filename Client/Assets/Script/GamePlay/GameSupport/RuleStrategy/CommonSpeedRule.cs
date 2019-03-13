using UnityEngine;
using System.Collections;


namespace GamePlay
{
    public class CommonSpeedRule:SpeedRuleStrategy
    {
        public PlayerBase GetNextGamePlayer()
        {
            return new PlayerBase(null,null);
        }
    }
}
