using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class GainSpCardEffect : EffectBase
    {
        private int cardID  = 0;
        private int cardCnt = 0;

        protected override void OnInitEffect(params object[] objs)
        {
            cardID  = StringConverter.ToInt(objs[0].ToString(), 0);
            cardCnt = StringConverter.ToInt(objs[1].ToString(), 0);
        }

        protected override void OnTakeEffect(GamePlayer source_player,GamePlayer player)
        {
            if (cardCnt > 0)
            {
                // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
                player.Role.GainSpCard(cardID, cardCnt);
            }
        }
    }
}
