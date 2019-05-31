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
        STACK,
        QUEUE//队列形式展示，关闭一个展示一个
    }

    public class UISceneBase
    {
        private Dictionary<UIBaseController,uint> m_allControllers;
        private Queue<UIBaseController> m_queueControllers;
        private Stack<UIBaseController> m_stackControllers;

        public void Init()
        {
            m_allControllers = new Dictionary<UIBaseController, uint>();
            m_queueControllers = new Queue<UIBaseController>();
            m_stackControllers = new Stack<UIBaseController>();
        }

        public UIBaseController OpenUIController<T>() where T : UIBaseController
        {
            UnityEngine.Object res = ResourceManager.Instance.GetResource("");
            GameObject gameobj = GameObject.Instantiate(res) as GameObject;
            T m_controller = gameobj.AddComponent(typeof(T)) as T;
            return m_controller;
        }
    }
}
  