/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson tech. Co., Ltd.
 *  FileName: STEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 11/7/2015
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.           STEditor               Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     11/7/2015       1.0        Build this file.
 *************************************************************************/

namespace Developer.STTool
{
    using System.IO;
    using System.Text;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Edit ScriptTemplate.
    /// </summary>
    public class STEditor : EditorWindow
    {
        #region Enum
        //Private enum.
        private enum EditTarget
        {
            CsharpBehaviour,
            JavascriptBehaviour,
            StateMachineBehaviour,
            SubStateMachineBehaviour,
            SurfaceShader,
            UnlitShader,
            ImageEffectShader,
            ComputeShader
        }//enum_end
        #endregion

        #region Field
        private static STEditor instance;
        private static EditTarget editTarget;
        private static string sTText;
        private static Vector2 scrollPos;
        #endregion

        #region Method
        //Show the window in unity editor menu.
        [MenuItem("Tool/STEditor &S")]
        private static void ShowSTEditor()
        {
            instance = GetWindow<STEditor>("STEditor");
            instance.Show();
        }//ShowS...()_end

        //Draw the window.
        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            editTarget = (EditTarget)EditorGUILayout.EnumPopup("EditTarget", editTarget);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("ScriptTemplate");
            EditorGUILayout.Space();
            if (GUILayout.Button("Current", GUILayout.Width(60)))
                GetScriptTemplateText();
            if (GUILayout.Button("Save", GUILayout.Width(60)))
                SaveScriptTemplate();
            EditorGUILayout.EndHorizontal();

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            sTText = EditorGUILayout.TextArea(sTText, GUILayout.ExpandHeight(true));
            EditorGUILayout.EndScrollView();

            EditorGUILayout.EndVertical();
        }//OnGUI()_end

        /// <summary>
        /// Get editer's script template path.
        /// </summary>
        private string GetScriptTemplatePath()
        {
            string scriptPath = string.Empty;
            switch (editTarget)
            {
                case EditTarget.CsharpBehaviour:
                    scriptPath = "81-C# Script-NewBehaviourScript.cs";
                    break;
                case EditTarget.JavascriptBehaviour:
                    scriptPath = "82-Javascript-NewBehaviourScript.js";
                    break;
                case EditTarget.StateMachineBehaviour:
                    scriptPath = "86-C# Script-NewStateMachineBehaviourScript.cs";
                    break;
                case EditTarget.SubStateMachineBehaviour:
                    scriptPath = "86-C# Script-NewSubStateMachineBehaviourScript.cs";
                    break;
                case EditTarget.SurfaceShader:
                    scriptPath = "83-Shader__Standard Surface Shader-NewSurfaceShader.shader";
                    break;
                case EditTarget.UnlitShader:
                    scriptPath = "84-Shader__Unlit Shader-NewUnlitShader.shader";
                    break;
                case EditTarget.ImageEffectShader:
                    scriptPath = "85-Shader__Image Effect Shader-NewImageEffectShader.shader";
                    break;
                case EditTarget.ComputeShader:
                    scriptPath = "90-Shader__Compute Shader-NewComputeShader.compute";
                    break;
            }//switch_end
            return EditorApplication.applicationContentsPath + "/Resources/ScriptTemplates/" + scriptPath + ".txt";
        }//GetS...()_end

        /// <summary>
        /// Get script template text.
        /// </summary>
        private void GetScriptTemplateText()
        {
            var scriptPath = GetScriptTemplatePath();
            if (File.Exists(scriptPath))
                sTText = File.ReadAllText(scriptPath, Encoding.Default);
            else
                sTText = string.Empty;
        }//GetS...()_end

        /// <summary>
        /// Save ScriptTemplate.
        /// </summary>
        private void SaveScriptTemplate()
        {
            File.WriteAllText(GetScriptTemplatePath(), sTText, Encoding.Default);
            bool closeEditor = EditorUtility.DisplayDialog(
                "Save Template",
                "Your edit content is already save to unity3d editor's script template!",
                "Close",
                "Continue"
                );
            if (closeEditor)
                instance.Close();
        }//SaveS...()_end
        #endregion
    }//class_end
}//namespace_end