/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson tech. Co., Ltd.
 *  FileName: STUpdater.cs
 *  Author: Mogoson   Version: 1.0   Date: 11/7/2015
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.          STUpdater               Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     11/7/2015        1.0        Build this file.
 *************************************************************************/

namespace Developer.STTool
{
    using System;
    using System.IO;
    using UnityEditor;

    /// <summary>
    /// Update ScriptTemplate.
    /// </summary>
    public class STUpdater : AssetModificationProcessor
    {
        /// <summary>
        /// This is called by Unity when it is about to create an asset
        /// not imported by the user, eg. ".meta" files.
        /// </summary>
        private static void OnWillCreateAsset(string assetPath)
        {
            //Get extension.
            assetPath = assetPath.Replace(".meta", string.Empty);
            var fileSuffix = Path.GetExtension(assetPath);
            if (fileSuffix != ".cs" && fileSuffix != ".js" && fileSuffix != ".shader" && fileSuffix != ".compute")
                return;

            //Get time.
            var nowTime = DateTime.Now;
            var createTime = nowTime.ToShortDateString();
            var copyrightTime = nowTime.Year.ToString() + "-" + (nowTime.Year + 1).ToString();

            //Update scripttemplate.
            var content = File.ReadAllText(assetPath);
            content = content.Replace("#CreateTime#", createTime);
            content = content.Replace("#CopyrightTime#", copyrightTime);
            File.WriteAllText(assetPath, content);

            //Refresh asset database.
            AssetDatabase.Refresh();
        }//OnW...()_end
    }//class_end
}//namespace_end