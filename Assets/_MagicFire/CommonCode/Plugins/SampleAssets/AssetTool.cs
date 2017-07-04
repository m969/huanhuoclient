/* --------------------------
 * Company: MagicFire Studio
 *   Autor: Changmin Yang
 *    描述： 此类用来封装unity的两种资源加载方式，Editor模式下的AssetDatabase和
 *          产品模式（就是编译发布后的）下的AssetBundle，（Resources的资源加载模式不适用于Mmorpg游戏）
 *          
 * 
    IOS:
    Application.dataPath :                      Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/xxx.app/Data
    Application.streamingAssetsPath :   Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/xxx.app/Data/Raw
    Application.persistentDataPath :      Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/Documents
    Application.temporaryCachePath :   Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/Library/Caches

    Android:
    Application.dataPath :                         /data/app/xxx.xxx.xxx.apk
    Application.streamingAssetsPath :      jar:file:///data/app/xxx.xxx.xxx.apk/!/assets
    Application.persistentDataPath :         /data/data/xxx.xxx.xxx/files
    Application.temporaryCachePath :      /data/data/xxx.xxx.xxx/cache

    Windows:
    Application.dataPath :                         /Assets
    Application.streamingAssetsPath :      /Assets/StreamingAssets
    Application.persistentDataPath :         C:/Users/xxxx/AppData/LocalLow/CompanyName/ProductName
    Application.temporaryCachePath :      C:/Users/xxxx/AppData/Local/Temp/CompanyName/ProductName

    Mac:
    Application.dataPath :                         /Assets
    Application.streamingAssetsPath :      /Assets/StreamingAssets
    Application.persistentDataPath :         /Users/xxxx/Library/Caches/CompanyName/Product Name
    Application.temporaryCachePath :     /var/folders/57/6b4_9w8113x2fsmzx_yhrhvh0000gn/T/CompanyName/Product Name


    Windows Web Player:
    Application.dataPath :             file:///D:/MyGame/WebPlayer (即导包后保存的文件夹，html文件所在文件夹)
    Application.streamingAssetsPath :
    Application.persistentDataPath :
    Application.temporaryCachePath :
 * -------------------------- */

using System;
using System.Collections.Generic;
using MagicFire.Mmorpg.Huanhuo;
using MagicFire.SceneManagement;

namespace MagicFire.Common.Plugin
{
    using UnityEngine;
    using System.Collections;
    using System.IO;

    public class AssetTool : MonoSingleton<AssetTool>
    {
        public const string Assets__Resources_ = "Assets/_Resources/";
        public const string Assets__Resources_Ours__Prefabs_ = "Assets/_Resources/Ours/_Prefabs/";
        public const string Assets__Resources_Ours_ = "Assets/_Resources/Ours/";
        public const string Assets__Resources_Ours__UIPanel_ = "Assets/_Resources/Ours/_UIPanel/";

        public static Object LoadAsset_Database_Or_Bundle(
            string assetDatabasePath, 
            string bundlePath = "",
            string bundleName = "",
            string assetName = "")
        {
            Object asset = null;
            AssetBundle bundle = null;

            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                if (XmlSceneManager.Instance.LoadMode == XmlSceneManager.LoadModeEnum.Database)
                {
                    asset = UnityEditor.AssetDatabase.LoadAssetAtPath<Object>(assetDatabasePath);
                    if (asset == null)
                        Debug.LogError(assetDatabasePath + " is null!");
                    return asset;
                }
            }
            bundle = BundleTool.Instance.TryGetBundle(bundlePath, bundleName);
            if (bundle)
                asset =  bundle.LoadAsset(assetName);
            return asset;
        }

        public static Object LoadAssetByTag(string assetTag)
        {
            return null;
        }
    }

}