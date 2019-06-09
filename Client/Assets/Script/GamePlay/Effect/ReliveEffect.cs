using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class ReliveEffect : EffectBase
    {
        private int HP;

        protected override void OnInitEffect(params object[] objs)
        {
            HP = CountUtil.CalcCount(objs);
        }

        protected override void OnTakeEffect(GamePlayer source_player, GamePlayer player)
        {
            //player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
            player.Role.CurrentHP = HP;
        }
    }
}
