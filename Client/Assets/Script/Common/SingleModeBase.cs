using System;
using UnityEngine;

namespace Common
{
    public class SingleModeMono<T> : MonoBehaviour where T : SingleMode<T>, new()
    {

        private static T m_instance;

        public static SingleMode<T> Instance
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

