using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class DiscardCardEffect : EffectBase
    {
        private int cardCnt;

        protected override void OnInitEffect(params object[] objs)
        {
            cardCnt = CountUtil.CalcCount(objs);
        }


        protected override void OnTakeEffect(GamePlayer source_player, GamePlayer player)
        {
            if (cardCnt > 0)
            {
                // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
                //player.Role.GetCard(0 - cardCnt);//SZY Error 这里弃卡还没实现，不能传入负数，会报错
            }
        }
    }
}
