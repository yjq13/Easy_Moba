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
            //player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);//SZY Error 这个方法是用来触发伤害buff用的，这里需要出发嘛？
            player.Role.ChangeHP(HP);
        }
    }
}
