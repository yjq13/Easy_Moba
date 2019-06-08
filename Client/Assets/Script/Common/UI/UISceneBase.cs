using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Common
{
    enum UI_OPEN_TYPE
    {
        NORMAL = 0,
        STACK = 1,
        QUEUE = 2//队列形式展示，关闭一个展示一个
    }

    public class UISceneBase
    {
        private Dictionary<UIBaseController, UI_OPEN_TYPE> m_allControllers;
        private Queue<UIBaseController> m_queueControllers;
        private Stack<UIBaseController> m_stackControllers;
        public static Transform UIRoot { get; private set; }

        public void Init()
        {
            m_allControllers = new Dictionary<UIBaseController, UI_OPEN_TYPE>();
            m_queueControllers = new Queue<UIBaseController>();
            m_stackControllers = new Stack<UIBaseController>();
        }

        public static void SetUIRoot(Transform root)
        {
            UIRoot = root;
        }

        public T OpenUIController<T>() where T : UIBaseController
        {
            T m_controller = (T)Activator.CreateInstance(typeof(T));
            m_controller.Init();
            m_allControllers.Add(m_controller, UI_OPEN_TYPE.NORMAL);
            return m_controller;
        }
    }
}
  