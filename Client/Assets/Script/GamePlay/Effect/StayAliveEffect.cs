using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class StayAliveEffect : EffectBase
    {
        private int stayHp = 0;

        protected override void OnInitEffect(params object[] objs)
        {
            stayHp = 1
        }

        protected override void OnTakeEffect(GamePlayer player)
        {
            if (Role.CurrentHP <= 0 && stayHp > 0)
            {
                // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
                player.Role.ChangeHP(stayHp);
            }
        }
    }
}
