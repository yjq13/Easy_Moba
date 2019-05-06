using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Editor
{
    public abstract class TemplateBase
    {
        protected string[] template_params = null;
        public string GetCode()
        {
            string templateCode = GetTemplate();
            Debug.Log(template_params.Length);
            int index = 0;
            foreach(string param in template_params)
            {
                templateCode = templateCode.Replace("{"+index+"}", param);
                index++;
            }
            return templateCode;
        }

        public abstract string GetTemplate();

        public abstract void ShowGetParam();
    }

}
