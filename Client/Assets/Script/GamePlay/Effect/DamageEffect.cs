using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GamePlay
{
    public enum DAMAGE_TYPE
    {
        NORMAL = 0,
    }

    class DamageEffect : EffectBase
    {
        private DAMAGE_TYPE m_type;
        private uint m_damageCount;

        public void InitDamageEffect(params object[] param)
        {
            DAMAGE_TYPE d_Type = DAMAGE_TYPE.NORMAL;
            uint damageCount = 1;
            if (param.Length == 2)
            {
                d_Type = (DAMAGE_TYPE)param[0];
                damageCount = (uint)param[1];
            }
            else
            {
                Debug.LogError("wrong param count input: DamageEffect" );
            }

            m_type = d_Type;
            m_damageCount = damageCount;
        }

        protected override void OnTakeEffect(GamePlayer target)
        {
            base.OnTakeEffect(target);
            target.Role.ChangeHP(m_damageCount);
        }
    }
}
