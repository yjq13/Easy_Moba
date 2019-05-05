using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class StaySpElmtPropEffect : EffectBase
    {
        private ELEMENT_PROPERTY spElmtProp = ELEMENT_PROPERTY.NONE;

        protected override void OnInitEffect(params object[] objs)
        {
            spElmtProp = (ELEMENT_PROPERTY)Enum.Parse(typeof(ELEMENT_PROPERTY), objs[0].ToString());
        }

        protected override void OnTakeEffect(GamePlayer player)
        {
            // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
            player.Role.ChangeStaySpElmtProp(spElmtProp);
        }
    }
}
