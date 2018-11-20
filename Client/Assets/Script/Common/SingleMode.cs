using System;

namespace Common{
    public abstract class SingleMode<T> where T : SingleMode<T>, new()
    {

        private static T m_instance;

        public static T Instance
        {
            get
            {
                if (m_instance != null)
                {
                    return m_instance;
                }
                else
                {
                    m_instance = new T();
                    m_instance.Init();
                    return m_instance;
                }
            }
        }

        private void Init()
        {
            OnInit();
        }

        protected abstract void OnInit();
    }
}


