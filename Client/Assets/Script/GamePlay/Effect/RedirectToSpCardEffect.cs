using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class RedirectToSpCardEffect : EffectBase
    {
        private int RedirectCardID = 0;

        protected override void OnInitEffect(params object[] objs)
        {
            RedirectCardID = RedirectCardUtil.RedirectCardID(EFFECT_TYPE.RedirectToSpCardEffect, objs);
        }

        protected override void OnTakeEffect(GamePlayer player)
        {
            // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
            // !!! 暂未实现
            RedirectCardUtil.ReplayCard(RedirectCardID);
        }
    }
}
