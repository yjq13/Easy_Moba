using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class ReplayLastCardCurRoundEffect : EffectBase
    {
        // !!! 需要嘉奇给我个接口 拿到本回合上张满足某属性的卡牌
        // 输入参数：卡牌属性维度；属性值 如：element_property;wood 木属性
        // 输出参数：该卡牌
        // 暂未实现
        private int damageCnt;

        protected override void OnInitEffect(params object[] objs)
        {
            damageCnt = CountUtil.CalcCount(objs);
        }

        protected override void OnTakeEffect(GamePlayer source_player, GamePlayer player)
        {
            //player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
            player.Role.ChangeHP(0 - damageCnt);
        }
    }
}
