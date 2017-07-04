using MagicFire.Common;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using System.Collections;

    public class Panel : MonoBehaviour
    {

        protected int _selectItemID = -1;

        protected string _selectItemName = "";//JC
        // Use this for initialization
        protected virtual void Start()
        {
        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }

        public virtual void Initialize()
        {
            
        }

        //伸展布局
        public virtual void StretchLayout()
        {
            transform.SetParent(SingletonGather.UiManager.CanvasLayerFront.transform);
            transform.localScale = new Vector3(1, 1, 1);
            var rect = GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.0f, 0.0f);
            rect.anchorMax = new Vector2(1, 1);
            rect.offsetMax = new Vector2(-0, 0);
            rect.offsetMin = new Vector2(0, 0);
        }

        public virtual void SetChildSelect(int childID)
        {
            _selectItemID = childID;
        }
    }

}