using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class SkipCardOutEffect : EffectBase
    {
        private int SkipTimes = 0;

        protected override void OnInitEffect(params object[] objs)
        {
            SkipTimes += Convert.ToInt32(objs[0]);
        }

        protected override void OnTakeEffect(GamePlayer source_player,GamePlayer player)
        {
            // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
            player.Role.AddSkipCardOutTimes(SkipTimes);
        }
    }
}
