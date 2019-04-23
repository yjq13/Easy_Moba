using Common;
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

        public void DispatchBuffTrigger(Buff_NOTIFY_TYPE triggerType, params object[] data)
        {
            BuffDelegate handler;
            if (m_BuffTriggerEventDic.TryGetValue(triggerType, out handler))
            {
                GameEngine.Instance.StartCoroutine(handler.Invoke(data));
            }
        }

        public void AddBuff(Buff buff)
        {
            RegisterTrigger(buff.GetTriggerTime(), buff.OnTriggerBuff);
        }

        public void RegisterTrigger(Buff_NOTIFY_TYPE triggerType, BuffDelegate buff_delegate)
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

        public void UnRegisterTrigger(Buff_NOTIFY_TYPE triggerType, BuffDelegate buff_delegate)
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
