using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class ClearBuffLeftTimesEffect : EffectBase
    {
        private int BuffID      = 0;
        private int BuffTimes   = 0;

        protected override void OnInitEffect(params object[] objs)
        {
            BuffID  = StringConverter.ToInt(objs[0].ToString(), 0);
            BuffTimes = player.GetBuffTimes(BuffID);
        }

        protected override void OnTakeEffect(GamePlayer player)
        {

            if (BuffTimes > 0)
            {
                // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
                player.GainBuff(BuffID, 0 - BuffTimes);
            }
        }
    }
}
