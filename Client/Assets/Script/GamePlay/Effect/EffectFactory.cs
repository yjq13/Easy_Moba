using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    public static class EffectFactory
    {
        public static EffectBase CreateEffect(string effectID)
        {
            switch (effectID)
            {
                case "Damage":
                    {
                        return new DamageEffect();
                    }
            }
            return null;
        }
    }
}
