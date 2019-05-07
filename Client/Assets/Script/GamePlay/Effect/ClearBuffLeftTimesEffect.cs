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

        protected override void OnTakeEffect(GamePlayer source_player,GamePlayer player)
        {

            if (BuffTimes > 0)
            {
                // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
                //player.GainBuff(BuffID, 0 - (int)BuffTimes);//SZY ERROR 你这个方法第二个参数是uint，结果你传了一个int…………还是个负数
            }
        }
    }
}
