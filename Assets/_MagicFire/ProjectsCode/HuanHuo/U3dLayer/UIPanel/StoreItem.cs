namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using System.Collections;
    using UnityEngine.UI;
    using MagicFire.Common;

    public class StoreItem : MonoBehaviour
    {
        public Panel parentPanel { get; set; }
        public string goodsName { get; set; }
        public int goodsID { get; set; }

        // Use this for initialization
        void Start()
        {
            if (goodsName == null)
            {
                goodsName = "NoGoods";
            }
        }
        public void OnClick()
        {
            //KBEngine.Event.fireIn("RequestBuyGoods", new object[] { goodsID });
            parentPanel.SetChildSelect(goodsID);
            SingletonGather.UiManager.TryGetOrCreatePanel("TheStorePanel").GetComponent<StorePanel>().ChangeColor();
        }
    }

}