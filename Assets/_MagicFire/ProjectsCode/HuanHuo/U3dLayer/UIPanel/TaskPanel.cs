using MagicFire.Common;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using System.Collections;

    public class TaskPanel : Panel
    {
        protected override void Start()
        {
            transform.SetParent(SingletonGather.UiManager.CanvasLayerFront.transform);
            transform.localScale = new Vector3(1, 1, 1);
            var rect = GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(1.0f, 1.0f);
            rect.anchorMax = new Vector2(1.0f, 1.0f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.offsetMax = new Vector2(-0, 0);
            rect.offsetMin = new Vector2(0, 0);
            rect.anchoredPosition = new Vector2(-100, -100);
            rect.sizeDelta = new Vector2(200, 100);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }

}