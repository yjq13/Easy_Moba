using System;
using System.Collections.Generic;
using Common;
using System.Linq;
using System.Text;

namespace GamePlay
{
    public class CauseDamageCondition : ITriggerCondition
    {
        public bool GetTriggerCondition(string triggerParam , params object[] param)
        {
            DAMAGE_TYPE damageType = (DAMAGE_TYPE)Enum.Parse(typeof(DAMAGE_TYPE), triggerParam); 
            if(param.Length > 0)
            {
                DamageEffect effect = (DamageEffect)param[0];
                if(effect.EffectType == damageType || damageType == DAMAGE_TYPE.ANY)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
