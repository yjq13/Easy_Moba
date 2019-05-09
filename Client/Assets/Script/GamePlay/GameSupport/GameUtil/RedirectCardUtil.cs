using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GamePlay
{
    public static class RedirectCardUtil
    {
        public static int RedirectCardID(EFFECT_TYPE type, params object[] objs)
        {
            // !!! 暂未实现
            switch (type)
            {
                case EFFECT_TYPE.RedirectToSpCardEffect:
                    return 0;
                case EFFECT_TYPE.ReplayLastCardCurRoundEffect:
                    return 0;
                default:
                    Debug.LogError("redirect card id error with first param: " + type);
                    break;
            }
            return -1;
        }

        // !!! 暂未实现
        public static void ReplayCard(int card_id)
        {

        } 

    }
}
