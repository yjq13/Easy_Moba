using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class IgnoreDamageDebuffEffect : EffectBase
    {
        private int damageCnt;

        protected override void OnInitEffect(params object[] objs)
        {
            damageCnt = CountUtil.CalcCount(objs);
        }

        protected override void OnTakeEffect(GamePlayer source_player, GamePlayer player)
        {
            //player.SendGameBuffTriggerEvent(Buff_NOTIFY_TYPE.GET_DAMAGE, this);
            player.Role.ChangeHP(0 - damageCnt);
        }
    }
}
