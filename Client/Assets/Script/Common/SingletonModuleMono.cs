using System;
using UnityEngine;

namespace Common
{
    public abstract class SingletonModuleMono<T> : MonoBehaviour,InterfaceSingletonModule where T : SingletonModuleMono<T>, new()
    {

        private static T m_instance;

        public static SingletonModuleMono<T> Instance
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
                    return m_instance;
                }
            }
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

