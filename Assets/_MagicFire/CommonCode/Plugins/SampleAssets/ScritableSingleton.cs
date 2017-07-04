namespace MagicFire.Common.Plugin
{
    using System.IO;
    //using UnityEditorInternal;
    using UnityEngine;
    using UnityEngine.Scripting;

    public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject
    {
        private static T s_Instance;

        public static T instance
        {
            get
            {
                Debug.Log("instance");
                foreach (object customAttribute in typeof(T).GetCustomAttributes(true))
                {
                    Debug.Log(customAttribute);
                    if (customAttribute is FilePathAttribute)
                        Debug.Log((customAttribute as FilePathAttribute).filepath);
                }
                if ((Object) ScriptableSingleton<T>.s_Instance == (Object) null)
                {
                    Debug.Log("== null!");
                    Debug.Log("CreateAndLoad");
                    ScriptableSingleton<T>.CreateAndLoad();
                }
                else
                {
                    //ScriptableSingleton<T>.CreateAndLoad();
                    Debug.Log("ScriptableSingleton<T>.s_Instance not null");
                }
                return ScriptableSingleton<T>.s_Instance;
            }
        }

        protected ScriptableSingleton()
        {
            Debug.Log("ScriptableSingleton");
            if ((Object)ScriptableSingleton<T>.s_Instance != (Object)null)
                Debug.LogError((object)"ScriptableSingleton already exists. Did you query the singleton in a constructor?");
            else
                ScriptableSingleton<T>.s_Instance = (object)this as T;
        }

        private static void CreateAndLoad()
        {
            string filePath = ScriptableSingleton<T>.GetFilePath();
            Debug.Log(filePath);
            if (!string.IsNullOrEmpty(filePath))
            {
                //var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(filePath);
                //if (asset)
                //{
                //    ScriptableSingleton<T>.s_Instance = asset as T;
                //}
                //else
                //{
                //    ScriptableSingleton<T>.s_Instance = ScriptableObject.CreateInstance<T>();
                //}
                // // InternalEditorUtility.LoadSerializedFileAndForget(filePath);
            }
            if (!((Object) ScriptableSingleton<T>.s_Instance == (Object) null))
            {
                Debug.Log("not null!");
                return;
            }
            
        }

        protected virtual void Save(bool saveAsText)
        {
            if ((Object)ScriptableSingleton<T>.s_Instance == (Object)null)
            {
                Debug.Log((object)"Cannot save ScriptableSingleton: no instance!");
            }
            else
            {
                string filePath = ScriptableSingleton<T>.GetFilePath();
                if (string.IsNullOrEmpty(filePath))
                    return;
                string directoryName = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryName))
                    Directory.CreateDirectory(directoryName);
          //      InternalEditorUtility.SaveToSerializedFileAndForget((Object[])new T[1]
          //      {
          //ScriptableSingleton<T>.s_Instance
          //      }, filePath, (saveAsText ? 1 : 0) != 0);
            }
        }

        private static string GetFilePath()
        {
            foreach (object customAttribute in typeof(T).GetCustomAttributes(true))
            {
                Debug.Log(customAttribute);
                if (customAttribute is FilePathAttribute)
                    return (customAttribute as FilePathAttribute).filepath;
            }
            return (string)null;
        }
    } 
}