using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    class RedirectCardUtil
    {
        public int RedirectCardID(EFFECT_TYPE type, params object[] objs)
        {
            // !!! 暂未实现
            switch (type)
            {
                case EFFECT_TYPE.RedirectToSpCardEffect:
                    return 0;
                    break;
                case EFFECT_TYPE.ReplayLastCardCurRoundEffect:
                    return 0;
                    break;
                default:
                    Debug.LogError("redirect card id error with first param: " + firstParam);
                    break;
            }
        }

        // !!! 暂未实现
        public void ReplayCard(int card_id)
        {

        } 

    }
}
