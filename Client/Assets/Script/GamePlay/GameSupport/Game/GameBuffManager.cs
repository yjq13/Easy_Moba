using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    public enum Buff_NOTIFY_TYPE
    {
        GET_DAMAGE,
        CAUSE_DAMAGE,
        AFTER_ROUND,
        DIE,
        USE_CARD,
        IMMEDIATELY,
        BEFORE_ROUND,
    }
    public class GameBuffManager
    {
        public delegate IEnumerator BuffDelegate(params object[] param);
        private Dictionary<Buff_NOTIFY_TYPE, BuffDelegate> m_BuffTriggerEventDic;
        private Dictionary<string, List<Buff>> m_BuffDic;

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
                        Buff oldBuff = buffList[0];
                        UnRegisterTrigger(oldBuff.GetTriggerTime(), oldBuff.OnTriggerBuff);
                        UnRegisterTrigger(oldBuff.GetCuntCountTime(), oldBuff.OnCutCountTime);
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
