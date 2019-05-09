using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class StoleRandomBuffEffect : EffectBase
    {
        private uint BuffCnt  = 0;

        protected override void OnInitEffect(params object[] objs)
        {
            BuffCnt = (uint)CountUtil.CalcCount(objs);
        }

        protected override void OnTakeEffect(GamePlayer source_player, GamePlayer target_player)
        {
            if (BuffCnt > 0)
            {
                // 这个effect是偷取target的buff加到自己身上，传参需要在target_player的基础上再加一个source_player
                // !!! 暂未实现
                // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
                source_player.StoleRandomBuff(target_player, BuffCnt);
            }
        }
    }
}
