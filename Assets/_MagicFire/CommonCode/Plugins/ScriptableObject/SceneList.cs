using MagicFire.Common.Plugin;

namespace MagicFire
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    [FilePath("Assets/SceneList.asset")]
    public class SceneList : ScriptableObject
    {
        public List<Object> sceneList = new List<Object>();
    }
}