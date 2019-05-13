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

        protected override void OnInitEffect(params object[] objs)
        {
            BuffID  = StringConverter.ToInt(objs[0].ToString(), 0);
        }

        protected override void OnTakeEffect(GamePlayer source_player,GamePlayer player)
        {
            int buffTimes = player.GetBuffTimes(BuffID);
            if (buffTimes > 0)
            {
                // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
                if (buffTimes > 0)
                {
                    player.GainBuff(BuffID, 0 - buffTimes);
                }
            }
        }
    }
}
