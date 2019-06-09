using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class MultiAssetGainEffect : EffectBase
    {
        // !!! 6.9 跟嘉奇商量了一下 没确定好怎么搞 待定吧
        // 暂未实现
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
