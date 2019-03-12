using System;
using UnityEngine;

namespace Common
{
    public class SingletonModuleMono<T> : MonoBehaviour where T : SingletonModule<T>, new()
    {

        private static T m_instance;

        public static SingletonModule<T> Instance
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


    }
}

