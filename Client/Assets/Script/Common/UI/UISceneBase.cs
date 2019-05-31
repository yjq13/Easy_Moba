using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
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
    }
}
  