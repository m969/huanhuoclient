using System;
using System.Collections.Generic;
using System.IO;

namespace MagicFire.Common.Plugin
{
    using UnityEngine;
    using System.Collections;
    using System.IO;
    using System.Resources;
    using UnityEngine.Scripting;

    [FilePath("Assets/AssetListObject.asset")]
    public class AssetListObject : ScriptableSingleton<AssetListObject>
    {
        public List<Object> SceneList = new List<Object>();
        [SerializeField]
        private Dictionary<string, UnityEngine.Object> _assets = new Dictionary<string, Object>();

        private AssetListObject() : base()
        {
            Debug.Log("AssetListObject");
        }

        protected override void Save(bool saveAsText)
        {
            base.Save(saveAsText);
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class FilePathAttribute : Attribute
    {
        public string filepath { get; private set; }

        public FilePathAttribute(string filepath)
        {
            this.filepath = filepath;
        }
    }

}