using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public interface SpeedRuleStrategy
    {
        PlayerBase GetNextGamePlayer();
    } 
}

