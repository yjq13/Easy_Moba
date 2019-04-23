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
                        effect.InitDamageEffect();
                        return effect;
                    }
            }
            return null;
        }
    }
}
