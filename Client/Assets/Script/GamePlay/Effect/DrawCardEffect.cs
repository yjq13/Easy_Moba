using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class DrawCardEffect : EffectBase
    {
        private int drawCnt = 0;

        protected override void OnInitEffect(params object[] objs)
        {
            drawCnt = CountUtil.CalcCount(objs);
        }

        protected override void OnTakeEffect(GamePlayer source_player, GamePlayer player)
        {
            if (drawCnt > 0)
            {
                // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
                player.Role.GetCard((uint)drawCnt);
            }
        }
    }
}
