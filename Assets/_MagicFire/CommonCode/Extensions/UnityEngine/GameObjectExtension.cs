namespace UnityEngine
{
    using System.Collections;

    public static class GameObjectExtension
    {
        public static void DestroyChildren(this GameObject gObj)
        {
            gObj.transform.DestroyChildren();
        }

        public static GameObject CreateChildFromPrefab(this GameObject gObj, Object prefab, string name = null)
        {
            return gObj.transform.CreateChildFromPrefab((Transform)prefab, name).gameObject;
        }

        public static GameObject CreateChildByName(this GameObject gObj, string name = null)
        {
            return gObj.transform.CreateChildByName(name).gameObject;
        }
    }

}