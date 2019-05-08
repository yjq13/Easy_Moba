using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    public static class EffectFactory
    {
        public static EffectBase CreateEffect(EffectInfoData data)
        {
            switch (data.EffectID)
            {
                case "Damage":
                    {
                        var effect = new DamageEffect();
                        effect.InitEffect(ELEMENT_PROPERTY.NONE,data.EffectParam1,data.EffectParam2);
                        return effect;
                    }
            }
            return null;
        }
    }
}
