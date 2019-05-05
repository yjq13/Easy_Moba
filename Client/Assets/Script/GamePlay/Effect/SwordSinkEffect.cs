using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class SwordSinkEffect : EffectBase
    {
        private bool    isSwordSink = false;
        private int     cardID      = 0;
        private int     targetUID   = 0;
        private float   actProgress = 0;

        protected override void OnInitEffect(params object[] objs)
        {
            isSwordSink = true;
            cardID      = player.Role.GetLastCardID();
            // 需要记录选定的对手id及对手当前行动条进度
            targetUID   = 0;
            act_prog    = 0;
        }

        protected override void OnTakeEffect(GamePlayer player)
        {
            if (isSwordSink && cardID > 0)
            {
                // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
                player.Role.SwordSink(cardID, targetUID, actProgress);
            }
        }
    }
}
