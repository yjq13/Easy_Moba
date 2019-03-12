using System;

namespace Common
{

    public interface InterfaceSingletonModule
    {
        void Init();
        void Cleanup();
    }

    public abstract class SingletonModule<T>: InterfaceSingletonModule where T : SingletonModule<T>, new()
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

        public SingletonModule()
        {
            SingletonModuleContext.RegisterModule(this);
        }

        public void Init()
        {
            OnInit();
        }

        public void Cleanup()
        {
            OnCleanup();
        }
        protected abstract void OnInit();

        protected abstract void OnCleanup();
    }
}


