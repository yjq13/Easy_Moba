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
        private int BuffID      = 0;
        private uint BuffTimes   = 0;

        protected override void OnInitEffect(params object[] objs)
        {
            BuffID = (int)objs[0];
            BuffTimes = (uint)objs[1];
        }

        protected override void OnTakeEffect(GamePlayer source_player,GamePlayer player)
        {
            if (BuffTimes > 0)
            {
                // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
                player.GainBuff(BuffID, (int)BuffTimes);
            }
        }
    }
}
