using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Common;

namespace GamePlay
{
    public class UILoginController : UIBaseController
    {
        public UILoginView m_View;

        public override void UIOnInit()
        {
            m_View = CreateView<UILoginView>();
        }
    }
}
