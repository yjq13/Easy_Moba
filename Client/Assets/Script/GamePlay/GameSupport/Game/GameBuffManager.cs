using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    public enum Buff_NOTIFY_TYPE
    {
        IMMEDIATELY,
        BEFORE_ROUND,
        CARD_IN,
        CARD_OUT,
        CARD_DISCARD,
        AFTER_ROUND,
        DO_EFFECT,
        UNDO_EFFECT,
        GET_DAMAGE,
        // GET_DAMAGE,
        // GET_DAMAGE,
        GET_DAMAGE_EXCEPT,
        CAUSE_DAMAGE,
        // CAUSE_DAMAGE,
        DIE,
        USE_CARD,
        GAIN_ASSET,
        // GAIN_ASSET,
        // CHANGE_ELEMENT_PROPERTY_EXCEPT
    }
    public class GameBuffManager
    {
        public delegate IEnumerator BuffDelegate(params object[] param);
        private Dictionary<Buff_NOTIFY_TYPE, BuffDelegate> m_BuffTriggerEventDic;
        private Dictionary<string, List<Buff>> m_BuffDic;

        public void Init()
        {
            m_BuffTriggerEventDic = new Dictionary<Buff_NOTIFY_TYPE, BuffDelegate>();
            m_BuffDic = new Dictionary<string, List<Buff>>();
        }

        public void Clear()
        {
            m_BuffDic = null;
            m_BuffTriggerEventDic = null;
        }

        public void DispatchBuffTrigger(Buff_NOTIFY_TYPE triggerType, params object[] data)
        {
            BuffDelegate handler;
            if (m_BuffTriggerEventDic.TryGetValue(triggerType, out handler))
            {
                handler.Invoke(data);
            }
        }

        public void AddBuff(Buff buff)
        {
            List<Buff> buffList = null;
            if (m_BuffDic.TryGetValue(buff.BuffID,out buffList))
            {
                if (!buff.Compositable)
                {
                    if(buffList.Count > 0)
                    {
                        foreach(Buff oldBuff in buffList)
                        {
                            UnRegisterTrigger(oldBuff.GetTriggerTime(), oldBuff.OnTriggerBuff);
                            UnRegisterTrigger(oldBuff.GetCuntCountTime(), oldBuff.OnCutCountTime);
                        }
                        buffList.Clear();
                    }
                }
                buffList.Add(buff);
            }
            else
            {
                m_BuffDic[buff.BuffID] = new List<Buff>
                {
                    buff
                };
            }
            RegisterTrigger(buff.GetTriggerTime(), buff.OnTriggerBuff);
            RegisterTrigger(buff.GetCuntCountTime(), buff.OnCutCountTime);
        }

        private void RegisterTrigger(Buff_NOTIFY_TYPE triggerType, BuffDelegate buff_delegate)
        {
            BuffDelegate handler;
            if (m_BuffTriggerEventDic.TryGetValue(triggerType, out handler))
            {
                handler += buff_delegate;
            }
            else
            {
                m_BuffTriggerEventDic[triggerType] = handler;
            }
        }

        private void UnRegisterTrigger(Buff_NOTIFY_TYPE triggerType, BuffDelegate buff_delegate)
        {
            BuffDelegate handler;
            if (m_BuffTriggerEventDic.TryGetValue(triggerType, out handler))
            {
                BuffDelegate finalHandler = handler - buff_delegate;
                if(finalHandler != null)
                {
                    handler = finalHandler;
                }
                else
                {
                    m_BuffTriggerEventDic.Remove(triggerType);
                }
            }
        }
    }
}
