using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    public enum DAMAGE_TYPE
    {
        ANY,
    }

    class DamageEffect : EffectBase
    {
        private int damageCnt = 0;
        public DAMAGE_TYPE EffectType = DAMAGE_TYPE.ANY;

        protected override void OnInitEffect(params object[] objs)
        {
            damageCnt = CountUtil.CalcCount(objs);
        }

        protected override void OnTakeEffect(GamePlayer source_player, GamePlayer player)
        {
            player.SendGameBuffTriggerEvent(Buff_NOTIFY_TYPE.GET_DAMAGE, this);
            player.Role.ChangeHP(0 - damageCnt);
        }
    }
}
