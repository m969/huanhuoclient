namespace MagicFire.Common.Plugin
{
    using UnityEngine;
    using UnityEditor;
    using System.Collections;

    public class CreateSampleAssetList
    {
        [MenuItem("Assets/CreateSampleAssetList")]
        public static void CreateSampleAssetListMethod()
        {
            //Debug.Log("CreateSampleAssetListMethod" + SampleAssetList.instance);
            //Debug.Log(AssetDatabase.GetAssetPath(SampleAssetList.instance));
            Debug.Log(ScriptableSingleton<SceneList>.instance);
            AssetDatabase.SaveAssets();
            //Debug.Log(AssetDatabase.DeleteAsset());
            
        }
    }

}