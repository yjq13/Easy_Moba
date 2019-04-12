using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void InitDamageEffect(DAMAGE_TYPE d_Type, uint damageCount)
        {
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
