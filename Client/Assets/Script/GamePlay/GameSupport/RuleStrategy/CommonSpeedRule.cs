using UnityEngine;
using System.Collections;


namespace GamePlay
{
    public class CommonSpeedRule:SpeedRuleStrategy
    {
        public RoleBase GetNextGamePlayer()
        {
            return new RoleBase(null,null);
        }
    }
}
