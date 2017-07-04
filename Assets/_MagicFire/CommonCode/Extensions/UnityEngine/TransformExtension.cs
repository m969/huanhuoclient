using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *	
 *  
 *
 *	by Xuanyi
 *
 */

namespace UnityEngine
{
	public static class TransformExtension 
    {
        public static void DestroyChildren(this Transform trans)
        {
            foreach (Transform child in trans)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public static Transform CreateChildFromPrefab(this Transform trans, Transform prefab, string name = null)
        {
            Transform childTrans = GameObject.Instantiate(prefab) as Transform;
            childTrans.SetParent(trans, false);
            if (name != null)
	        {
                childTrans.name = name;
	        }
            return childTrans;
        }

        public static Transform CreateChildByName(this Transform trans, string name = null)
        {
            var child = new GameObject();
            child.transform.SetParent(trans, false);
            if (name != null)
            {
                child.name = name;
            }
            return child.transform;
        }
	}
}
