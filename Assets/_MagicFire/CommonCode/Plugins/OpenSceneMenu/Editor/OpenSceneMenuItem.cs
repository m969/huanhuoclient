namespace MagicFire
{
    using UnityEngine;
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using System.Collections;
    using UnityEditor.SceneManagement;
    using System.Collections.Generic;
    using System.Xml;
    using System.IO;
    using System.Text;
    using LitJson;

    public class OpenSceneMenuItem
    {
        //[MenuItem("Open Scene/CreateSceneList")]
        //public static void CreateSceneList()
        //{
        //    AssetDatabase.CreateAsset(new SceneList(), "Assets/Resources/SceneList.asset");
        //}

        //static void OpenScene(string name)
        //{
        //    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo() == true)
        //    {
        //        EditorSceneManager.OpenScene("Assets/_Scenes/" + name + ".unity");
        //    }
        //}

        [MenuItem("Open Scene/RefreshSceneItem")]
        public static void OutputCSharp()
        {
            string filepath = Application.dataPath + @"/_MagicFire/CommonCode/Plugins/OpenSceneMenu/Editor/OpenSceneMenuItemScript.cs";
            FileInfo t = new FileInfo(filepath);
            if (!File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            StreamWriter sw = t.CreateText();
            StringBuilder sb = new StringBuilder();
            sb.Append("namespace MagicFire\n" +
                      "{\n" +
                      "using System;\n" +
                      "using UnityEngine;\n" +
                      "using UnityEditor;\n" +
                      "using UnityEditor.SceneManagement;\n" +
                      "public class OpenSceneMenuItemScript{\n" +
                      MenuItemFunc() +
                      "}");
            sw.WriteLine(sb.ToString());
            sw.Close();
            sw.Dispose();
            AssetDatabase.Refresh();
        }

        private static string MenuItemFunc()
        {
            //string scenesPath = "Assets/_MagicFire/ProjectsCode/HuanHuo/HuanHuouFrameProject/Scenes";
            string scenesPath = "Assets/_Scenes";
            string[] guids = AssetDatabase.FindAssets("", new[] { scenesPath });
            string funcs = "";
            foreach (var guid in guids)
            {
                var scene = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(guid));
                string func = "[MenuItem(\"Open Scene/" + scene.name + "\")]\r\n" +
                              "public static void " + scene.name + "()\r\n" +
                              "{\r\n" +
                              "OpenScene(\"" + scene.name + "\");\r\n" +
                              "}\r\n";
                funcs += func;
            }
            string openSceneFunc = "" +
                                    "static void OpenScene(string name)\r\n" +
                                    "{\r\n" +
                                    "if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo() == true)\r\n" +
                                    "{\r\n" +
                                    "   EditorSceneManager.OpenScene(\"" + scenesPath + "/\" + name + \".unity\");\r\n" +
                                    "}\r\n}" +
                                    "}\n" +
                                   "";
            funcs += openSceneFunc;
            return funcs;
        }
    }
}