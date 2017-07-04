/* *********************************************************
 * Company   
	: MagicFire Studio
 * Autor         
	: Chengmin
 * Description 
	: 
 * Date          
	: 6/29/2017
 * *********************************************************/

using System.Collections.Generic;
using MagicFire.SceneManagement;

namespace MagicFire.Common.Plugin
{
    using UnityEngine;
    using System.Collections;

    //[RequireComponent(typeof())]
    //[AddComponentMenu("")]
    public class BundleTool : MonoSingleton<BundleTool>
    {
        #region Property and Field
        //  [Tooltip("")]
        //  [SerializeField]
        private static readonly Dictionary<string, AssetBundle> _bundles = new Dictionary<string, AssetBundle>();
        #endregion

        #region Private Method
        //  void Start () 
        //  {
        //
        //  }

        //  void Update()
        //  {
        //
        //  }
        #endregion

        #region Public Method
        //
        public void LoadAllBundle()
        {
            StartCoroutine(LoadBundle("Prefabs", "uipanel_bundle"));
            StartCoroutine(LoadBundle("Prefabs", "player_bundle"));
            StartCoroutine(LoadBundle("Prefabs", "monster_bundle"));
            StartCoroutine(LoadBundle("Prefabs", "npc_bundle"));
            StartCoroutine(LoadBundle("Prefabs", "trigger_bundle"));
            StartCoroutine(LoadBundle("Prefabs", "auxiliaryprefabs_bundle"));
        }

        public IEnumerator LoadBundle(string bundlePath, string bundleName)
        {
            string fullBundlePath = null;
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                    fullBundlePath = "file://" + Application.streamingAssetsPath + "/" + bundlePath + "/Windows/" + bundleName;
                    break;
                case RuntimePlatform.WindowsPlayer:
                    fullBundlePath = "file://" + Application.streamingAssetsPath + "/" + bundlePath + "/Windows/" + bundleName;
                    break;
                case RuntimePlatform.Android:
                    fullBundlePath = Application.streamingAssetsPath + "/" + bundlePath + "/Android/" + bundleName;
                    break;
            }
            var www = new WWW(fullBundlePath);
            while (true)
            {
                if (www.isDone)
                {
                    XmlSceneManager.Message1 += bundleName + "is Done!\n";
                    break;
                }
                yield return null;
            }
            var bundle = www.assetBundle;
            AddBundle(bundlePath + bundleName, bundle);
        }

        public AssetBundle TryGetBundle(string bundlePath, string bundleName)
        {
            AssetBundle bundle = null;
            _bundles.TryGetValue(bundlePath + bundleName, out bundle);
            return bundle;
        }

        public static void AddBundle(string bundlePath, AssetBundle bundle)
        {
            _bundles.Add(bundlePath, bundle);
        }

        //
        #endregion
    }//class_end
}//namespace_end