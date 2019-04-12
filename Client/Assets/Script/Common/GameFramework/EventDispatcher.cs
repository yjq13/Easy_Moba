using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class EventDispatcher
    {
        public delegate void EventHandler(params object[] data);

        private Dictionary<uint, EventHandler> m_eventDic;

        public void RegisterEvent(uint event_id, EventHandler handler)
        {
            EventHandler _handler = null;
            if (m_eventDic.TryGetValue(event_id,out _handler))
            {
                m_eventDic[event_id] = _handler + handler;
            }
            else
            {
                m_eventDic[event_id] = handler;
            }
        }

        public void DispatchEvent(uint event_id, params object[] data)
        {
            EventHandler handler;
            if (m_eventDic.TryGetValue(event_id, out handler))
            {
                handler.Invoke(data);
            }
        }

        public void UnRegisterEvent(uint event_id, EventHandler handler)
        {
            EventHandler _handler;
            if (m_eventDic.TryGetValue(event_id, out _handler))
            {
                EventHandler finalHandler = _handler - handler;
                if (finalHandler == null)
                {
                    m_eventDic.Remove(event_id);
                }
                else
                {
                    m_eventDic[event_id] = finalHandler;
                }
            }
        }
    }
}
