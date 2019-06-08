using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GamePlay
{
    public static class EffectFactory
    {
        public static EffectBase CreateEffect(EffectInfoData data)
        {
            Type effectClass = Type.GetType("GamePlay."+data.EffectID);
            object effect_obj = Activator.CreateInstance(effectClass);
            EffectBase effect = null;
            if(effect_obj != null)
            {
                effect = (EffectBase)effect_obj;
                effect.InitEffect(data.elementPtoprtty,data.EffectParam1,data.EffectParam2);
            }
            else
            {
                Debug.LogError("effect id can not be find!");
            }
            return effect;
        }
    }
}
