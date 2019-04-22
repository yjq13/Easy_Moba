using System;
using System.Collections.Generic;
using Common;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public interface ITriggerCondition
    {
        bool GetTriggerCondition(string triggerParam,params object[] param);
    }

    public class Buff
    {
        private BuffData m_BuffData = null;
        private uint m_BuffCount = 0;
        private string m_BuffID = string.Empty;
        private ITriggerCondition triggerCondition;
        private GamePlayer OwnPlayer = null;

        public Buff(string buffID)
        {
            m_BuffID = buffID;
            m_BuffData = ConfigDataManager.Instance.GetData<BuffData>(ResourceIDDef.GAME_BUFF_CONFIG);
            triggerCondition = BuffConditionFactory.CreateBuffCondition(m_BuffData.TriggerTime);
        }

        public void BundleBuff(GamePlayer player)
        {
            OwnPlayer = player;
        }

        public IEnumerator OnTriggerBuff(params object[] param)
        {
            bool TriggerEffect = true;
            if (triggerCondition != null)
            {
                TriggerEffect = triggerCondition.GetTriggerCondition(m_BuffData.TriggerParam, param);
            }
            if (TriggerEffect)
            {
                foreach(EffectInfoData effect_info in m_BuffData.EffectList)
                {
                    EffectBase effect = EffectFactory.CreateEffect(effect_info.EffectID);
                    List<GamePlayer> targets = null;
                    yield return GameEngine.Instance.StartCoroutine(GameTargetManager.Instance.StartGetTarget(OwnPlayer, effect_info.TargetType));
                    targets = GameTargetManager.Instance.GetChoosedTarget();
                    if (targets != null)
                    {
                        effect.TakeEffect(targets);
                    }
                }
            }
        }

        public bool OnCutCountTime()
        {
            if(m_BuffCount != 0)
            {
                m_BuffCount--;
            }
            else
            {
                m_BuffCount = 0;
                return false;
            }
            return true;
        }

        public Buff_NOTIFY_TYPE GetTriggerTime()
        {
            return m_BuffData.TriggerTime;
        }

        public Buff_NOTIFY_TYPE GetCuntCountTime()
        {
            return m_BuffData.CutCountTime;
        }
    }
}
