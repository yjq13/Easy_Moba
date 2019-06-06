﻿using System;
using UnityEngine;

namespace Common
{
    public class UIBaseController
    {
        protected T CreateView<T>() where T : UIBaseView, new()
        {

            System.Reflection.MemberInfo info = typeof(T);
            object[] attributes = info.GetCustomAttributes(true);
            T view = null;
            foreach (var attribute in attributes)
            {
                UIResourceAttribute resourceInfo = (UIResourceAttribute)attribute;
                if(resourceInfo != null)
                {
                    ResourceID id = ResourceManager.Instance.GetResourceIDbyPath(resourceInfo.ResID);
                    UnityEngine.Object res = ResourceManager.Instance.GetResource(id);
                    GameObject viewObject = UnityEngine.Object.Instantiate(res) as GameObject;
                    view = viewObject.GetComponent<T>();
                }
            }
            view.Init(UISceneBase.UIRoot);
            return view;
        }

        public void OnInit()
        {
            UIOnInit();
        }

        public virtual void UIOnInit()
        {

        }
    }
}
