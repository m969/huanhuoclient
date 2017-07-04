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

using MagicFire.Common.Plugin;

namespace MagicFire.Common.Plugin
{
    using UnityEngine;
    using System.Collections;

    //[RequireComponent(typeof())]
    //[AddComponentMenu("")]
    public class AssetToolObject : MonoBehaviour
    {
        #region Property and Field
        //  [Tooltip("")]
        //  [SerializeField]
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
        public void LoadAssetBundleByWww(string bundlePath)
        {
            StartCoroutine(LoadBundle(bundlePath));
        }

        private IEnumerator LoadBundle(string bundlePath)
        {
            var www = new WWW(bundlePath);
            while (true)
            {
                if (www.isDone)
                {
                    Debug.Log(bundlePath + " is Done!");
                    break;
                }
                yield return null;
            }
            
        }
        //
        #endregion
    }//class_end
}//namespace_end