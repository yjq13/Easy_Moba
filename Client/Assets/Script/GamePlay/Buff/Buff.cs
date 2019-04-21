using System;
using System.Collections.Generic;
using Common;
using System.Linq;
using System.Text;
using UnityEngine;

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

        public Buff(string buffID)
        {
            m_BuffID = buffID;
            m_BuffData = ConfigDataManager.Instance.GetData<BuffData>(ResourceIDDef.GAME_BUFF_CONFIG);
            triggerCondition = BuffConditionFactory.CreateBuffCondition(m_BuffData.TriggerTime);
        }

        public void OnTriggerBuff(params object[] param)
        {
            if (triggerCondition != null)
            {
                triggerCondition.GetTriggerCondition(m_BuffData.TriggerParam, param);
            }
            else
            {
                Debug.LogError("Condition is not create successfully！please check it");
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
