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
        private BUFF_TYPE   BuffID  = BUFF_TYPE.NONE;

        protected override void OnInitEffect(params object[] objs)
        {
            BuffID  = (BUFF_TYPE)Enum.Parse(typeof(BUFF_TYPE), objs[0].ToString());
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
