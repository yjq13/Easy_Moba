using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Common
{
    public class UIBaseView : MonoBehaviour
    {
        public void Init(Transform root)
        {
            transform.parent = root;
            transform.localScale = Vector3.one;
        }

        protected virtual void OnUIinit()
        {
            
        }
    }
}
