using MagicFire.Common;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;

    public class GoldCountHint : Panel
    {
        [SerializeField]
        private Text _goldCountChangeText;

        protected override void Start()
        {
            transform.SetParent(UiManager.instance.CanvasLayerFront.transform);
            transform.localPosition = new Vector3(0, 0, 0);
            if (gameObject.activeInHierarchy == true)
            {
                gameObject.SetActive(false);
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void ShowHint(int goldCountChangeNum)
        {
            if (goldCountChangeNum > 0)
            {
                _goldCountChangeText.text = "获得" + goldCountChangeNum + "金币";
            }
            else
            {
                _goldCountChangeText.text = "失去" + -1 * goldCountChangeNum + "金币";
            }
            gameObject.SetActive(true);
            GetComponent<RectTransform>().SetAsLastSibling();
        }
    }

}