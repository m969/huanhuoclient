using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MagicFire.Common.Plugin;

namespace MagicFire
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PrefabMonoSingletonAttribute : Attribute
    {
        public string PrefabDatabasePath
        {
            get;set;
        }

        public string PrefabBundlePath
        {
            get; set;
        }

        public string PrefabBundleName
        {
            get; set;
        }

        public string PrefabAssetName
        {
            get; set;
        }

        public PrefabMonoSingletonAttribute(string prefabDatabasePath, string prefabBundlePath = "", string prefabBundleName = "", string prefabAssetName = "")
        {
            PrefabDatabasePath = prefabDatabasePath;
            PrefabBundlePath = prefabBundlePath;
            PrefabBundleName = prefabBundleName;
            PrefabAssetName = prefabAssetName;
        }
    }

    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        protected static T _instance = null; //ycm: protected internal  ->  private

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (FindObjectsOfType<T>().Length > 1)
                    {
                        Debug.Log("More than 1!");
                        return _instance;
                    }
                    if (_instance == null)
                    {
                        var instanceName = typeof(T).Name;
                        //Debug.Log("instance Name: " + instanceName);
                        var instanceGO = GameObject.FindObjectOfType<T>() as GameObject;

                        if (instanceGO == null)
                        {
                            var attrs = typeof(T).GetCustomAttributes(typeof(PrefabMonoSingletonAttribute), true);
                            if (attrs.Length == 0)
                            {
                                instanceGO = new GameObject(instanceName);
                                _instance = instanceGO.AddComponent<T>();
                            }
                            else
                            {
                                foreach (object customAttribute in attrs)
                                {
                                    if (customAttribute is PrefabMonoSingletonAttribute)
                                    {
                                        var prefab = 
                                            AssetTool.LoadAsset_Database_Or_Bundle(
                                                ((PrefabMonoSingletonAttribute)customAttribute).PrefabDatabasePath,
                                                ((PrefabMonoSingletonAttribute)customAttribute).PrefabBundlePath,
                                                ((PrefabMonoSingletonAttribute)customAttribute).PrefabBundleName,
                                                ((PrefabMonoSingletonAttribute)customAttribute).PrefabAssetName);
                                        if (prefab == null)
                                        {
                                            Debug.LogError(((PrefabMonoSingletonAttribute)customAttribute).PrefabDatabasePath + " is null");
                                            instanceGO = new GameObject(instanceName);
                                            _instance = instanceGO.AddComponent<T>();
                                        }
                                        else
                                        {
                                            instanceGO = UnityEngine.Object.Instantiate(prefab) as GameObject;
                                            _instance = instanceGO.GetComponent<T>();
                                            if (_instance == null)
                                            {
                                                _instance = instanceGO.AddComponent<T>();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        
                        DontDestroyOnLoad(instanceGO);  //保证实例不会被释放
                        instanceGO.tag = "DontDestroy";
                        //Debug.Log("Add New Singleton " + _instance.name + " in Game!");
                    }
                }
                return _instance;
            }
        }

        protected virtual void OnDestroy()
        {
            _instance = null;
        }
    }

}