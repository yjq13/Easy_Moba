using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class ReplayLastCardCurRoundEffect : EffectBase
    {
        private int damageCnt;

        protected override void OnInitEffect(params object[] objs)
        {
            damageCnt = CountUtil.CalcCnt(objs)
        }

        protected override void OnTakeEffect(GamePlayer player)
        {
            player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
            player.Role.ChangeHP(0 - damageCnt);
        }
    }
}
