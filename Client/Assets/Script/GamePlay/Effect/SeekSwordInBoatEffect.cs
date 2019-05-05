using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class SeekSwordInBoatEffect : EffectBase
    {
        protected override void OnInitEffect(params object[] objs)
        {
        }

        protected override void OnTakeEffect(GamePlayer player)
        {
            // 需要判断行动进度
            // !!! 暂未实现
            // if ()
            // {
            //     // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
            //     player.Role.GainCard(cardID, 1);
            // }
            // else
            // {
            //     移除刻舟求剑buff
            //     !!! 暂无实现
            //     player.Role.
            // }
        }
    }
}
