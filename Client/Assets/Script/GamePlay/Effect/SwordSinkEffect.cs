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
            targetUID = 0;
        }

        protected override void OnTakeEffect(GamePlayer source_player,GamePlayer player)
        {
            if (isSwordSink && cardID > 0)
            {
                cardID      = player.Role.GetLastCardID();
                actProgress = player.Role.GetActProgress();
                // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
                source_player.Role.SetSwordSink(cardID, targetUID, actProgress);
            }
        }
    }
}
