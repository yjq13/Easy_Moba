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
        private float   act_prog = 0;//SZY ERROR 这个是我加的，你看看到底是啥类型

        protected override void OnInitEffect(params object[] objs)
        {
            isSwordSink = true;
            //cardID      = player.Role.GetLastCardID();//SZY ERROR Player 是怎么拿到的
            // 需要记录选定的对手id及对手当前行动条进度
            targetUID = 0;
            act_prog    = 0;
        }

        protected override void OnTakeEffect(GamePlayer source_player,GamePlayer player)
        {
            if (isSwordSink && cardID > 0)
            {
                // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
                player.Role.SetSwordSink(cardID, targetUID, actProgress);
            }
        }
    }
}
