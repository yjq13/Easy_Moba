using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class CureEffect : EffectBase
    {
        private int CureCnt = 0;

        protected override void OnInitEffect(params object[] objs)
        {
            CureCnt = CountUtil.CalcCount(objs);
        }
               

        protected override void OnTakeEffect(GamePlayer source_player, GamePlayer player)
        {
            player.Role.ChangeHP(CureCnt);
        }
    }
}
