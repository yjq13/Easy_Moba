using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using GameEditor;
using Common;

public class AutoGenCodeUtil : EditorWindow
{
    private string codeExportBasePath = string.Empty;
    private string exportFileName = string.Empty;
    private Rect codeExportBaseRect;
    private TemplateType template = TemplateType.UIController;
    TemplateBase templateCode = null;

    public enum TemplateType
    {
        UIView = 0,
        UIController = 1,
        UIModel = 2
    }


    [MenuItem("Auto Gen/Auto Gen Code")]
    static void Init()
    {
        AutoGenCodeUtil window = (AutoGenCodeUtil)EditorWindow.GetWindow(typeof(AutoGenCodeUtil));
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Drag the path into it:");
        codeExportBaseRect = EditorGUILayout.GetControlRect();
        codeExportBasePath = EditorGUI.TextField(codeExportBaseRect, codeExportBasePath);
        if ((Event.current.type == EventType.DragUpdated
            || Event.current.type == EventType.DragExited)
            && codeExportBaseRect.Contains(Event.current.mousePosition))
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
            if (DragAndDrop.paths != null && DragAndDrop.paths.Length > 0 && Directory.Exists(DragAndDrop.paths[0]))
            {
                codeExportBasePath = DragAndDrop.paths[0];
            }
            Debug.Log(codeExportBasePath);
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Template choose:");
        template = (TemplateType)EditorGUILayout.EnumPopup(template);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("File name:");
        exportFileName = EditorGUILayout.TextField(exportFileName);



        GetTemplate();

        if (templateCode != null)
        {
            templateCode.ShowGetParam();

            if (GUILayout.Button("Generate"))
            {
                Generate();
            }
        }
        else
        {
            Debug.LogError("the template choosed cant find!");
        }


    }

    private void Generate()
    {
        string codeStr = templateCode.GetCode();
        try
        {
            string realDiskCSFilePath = codeExportBasePath + "/" + exportFileName;
            string filePath = realDiskCSFilePath.Replace("Assets", "");
            filePath = Application.dataPath + filePath;
            Debug.Log(filePath + File.Exists(filePath));
            if (!File.Exists(filePath))
            {
                StreamWriter sw;
                sw = new StreamWriter(new FileStream(realDiskCSFilePath, FileMode.Create, FileAccess.ReadWrite));

                sw.Write(codeStr);
                sw.Close();
                sw.Dispose();
            }
            else
            {
                Debug.Log("Controller has been create");
            }

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void GetTemplate()
    {
        switch (template)
        {
            case TemplateType.UIView:
                {
                    break;
                }
            case TemplateType.UIController:
                {
                    if(templateCode == null || templateCode.GetType() != typeof(UIControllerTemplate))
                    {
                        templateCode = new UIControllerTemplate();
                    }
                    break;
                }
            case TemplateType.UIModel:
                {
                    break;
                }
        }
    }
}