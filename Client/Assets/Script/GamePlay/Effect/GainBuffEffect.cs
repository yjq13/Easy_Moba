using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class GainBuffEffect : EffectBase
    {
        private BUFF_TYPE   BuffID      = BUFF_TYPE.NONE;
        private int         BuffTimes   = 0;

        protected override void OnInitEffect(params object[] objs)
        {
            BuffID      = (BUFF_TYPE)Enum.Parse(typeof(BUFF_TYPE), objs[0].ToString());
            // !!!
            // 求问：为什么下面不能写作 =int(objs[1])
            BuffTimes   = Convert.ToInt32(objs[1]);
        }

        protected override void OnTakeEffect(GamePlayer source_player,GamePlayer player)
        {
            if (BuffTimes > 0)
            {
                // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
                player.GainBuff(BuffID, BuffTimes);
            }
        }
    }
}
