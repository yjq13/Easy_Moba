using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    public enum BUFF_TRIGGER_TYPE
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
        public delegate void BuffDelegate(params object[] param);
        private Dictionary<BUFF_TRIGGER_TYPE, BuffDelegate> m_BuffTriggerEventDic;

        public void DispatchBuffTrigger(BUFF_TRIGGER_TYPE triggerType, params object[] data)
        {
            BuffDelegate handler;
            if (m_BuffTriggerEventDic.TryGetValue(triggerType, out handler))
            {
                handler.Invoke(data);
            }
        }

        public void AddBuff(BuffBase buff)
        {
            RegisterTrigger(BUFF_TRIGGER_TYPE triggerType, BuffDelegate buff_delegate)
        }

        public void RegisterTrigger(BUFF_TRIGGER_TYPE triggerType, BuffDelegate buff_delegate)
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

        public void UnRegisterTrigger(BUFF_TRIGGER_TYPE triggerType, BuffDelegate buff_delegate)
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
