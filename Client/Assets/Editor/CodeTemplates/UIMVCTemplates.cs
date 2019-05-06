using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Editor;

namespace Editor
{
    public class UIControllerTemplate : TemplateBase
    {
        //param 0: Controller Class name
        //Param 1: View Class name
        public override string GetTemplate()
        {
            return @"using COW.GamePlay;
using COW.HUD;
using GCommon;
using message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COW
{
    public class {0} : UIBaseController
    {
        {1} m_View;

        public static ResourceID GetResourceID()
        {
            return ResourceID.INVALID;
        }

        protected override void OnUIInit()
        {
            base.OnUIInit();

            m_View = CreateView<{1}> ();
        }
    }
}";
        }

        public override void ShowGetParam()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Controller Class Name");
            if(template_params == null)
            {
                template_params = new string[2];
                template_params[0] = "NewExportController";
                template_params[1] = "UIViewBase";
            }
            template_params[0] = EditorGUILayout.TextField(template_params[0]);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Related View Class Name");
            template_params[1] = EditorGUILayout.TextField(template_params[1]);
            EditorGUILayout.EndHorizontal();
        }
    }
}
