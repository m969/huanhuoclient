namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using MagicFire.Common;
    using MagicFire.Common.Plugin;
    using MagicFire.Mmorpg.UI;

    public class BagPanel : Panel
    {
        private List<object> _bagGoodsList = new List<object>();
        private List<object> _oldBagGoodsList = new List<object>();
        private bool _hasAssign;
        private bool _hasCreateOnce;
        [SerializeField]
        private Transform _content;
        //private Object _bagItemPrefab;
        private Text _goldCountText;

        public List<object> BagGoodsList
        {
            get
            {
                return _bagGoodsList;
            }
            set
            {
                if (_hasAssign == false)
                {
                    _bagGoodsList = value;
                    _oldBagGoodsList = new List<object>(_bagGoodsList.ToArray());
                    _hasAssign = true;
                }
                else
                {
                    _bagGoodsList = value;
                }
            }
        }

        public string GoldCountText
        {
            set
            {
                if (_goldCountText == null)
                {
                    _goldCountText = transform.Find("GoldCount").GetComponent<Text>();
                }
                _goldCountText.text = value + "  金币";
            }
        }

        protected override void Start()
        {
            base.Start();
            transform.SetParent(UiManager.instance.CanvasLayers[1].transform);
            transform.localPosition = new Vector3(0, 0, 0);
            _goldCountText = transform.Find("GoldCount").GetComponent<Text>();

            var model = SingletonGather.WorldMediator.MainAvatarView.Model as KBEngine.Model;
            if (model != null)
            {
                object avatarBagObject = model.getDefinedProperty("avatarBag");
                BagGoodsList = ((Dictionary<string, object>)avatarBagObject)["values"] as List<object>;
            }
            (SingletonGather.WorldMediator.MainAvatarView.Model).SubscribePropertyUpdate("avatarBag", HandleAvatarBagUpdate);
        }

        protected override void Update()
        {
            base.Update();
            if (PlayerInputController.instance)
            {
                BagGoodsList = ((KBEngine.Avatar)PlayerInputController.instance.AvatarView.Model).AvatarBag;
            }
            if (!_hasCreateOnce)
            {
                CreateItem();
                _hasCreateOnce = true;
            }
            int i = _oldBagGoodsList.Except(_bagGoodsList).ToList().Count;
            int j = _bagGoodsList.Except(_oldBagGoodsList).ToList().Count;
            if (i != 0 || j != 0)
            {
                _oldBagGoodsList = new List<object>(_bagGoodsList.ToArray());
            }
            if (i == 0 && j == 0)
            {
                return;
            }
            CreateItem();
        }

        public void HandleAvatarBagUpdate(object val)
        {
            Debug.Log("avatarBag has update");
            object avatarBagObject = (SingletonGather.WorldMediator.MainAvatarView.Model as KBEngine.Model).getDefinedProperty("avatarBag");
            _bagGoodsList = ((Dictionary<string, object>)avatarBagObject)["values"] as List<object>;
        }

        void CreateItem()
        {
            for (int i = 0; i < _content.childCount; i++)
            {
                Destroy(_content.GetChild(i).gameObject);
            }
            foreach (var goodsId in _bagGoodsList)
            {
                var tempItem = 
                    Instantiate(
                        AssetTool.LoadAsset_Database_Or_Bundle(
                            AssetTool.Assets__Resources_Ours__UIPanel_ + "StoreItems/" + (int)goodsId + ".prefab",
                            "Prefabs",
                            "uipanel_bundle",
                            "" + (int)goodsId)) as GameObject;
                if (tempItem != null)
                {
                    tempItem.transform.SetParent(_content);
                    tempItem.GetComponent<StoreItem>().goodsID = (int)goodsId;
                    tempItem.GetComponent<StoreItem>().parentPanel = this;
                    RectTransform itemObjectRectTrans = tempItem.transform.Find("Object").GetComponent<RectTransform>();
                    itemObjectRectTrans.anchorMin = new Vector2(0.0f, 0.0f);
                    itemObjectRectTrans.anchorMax = new Vector2(1, 1);
                    itemObjectRectTrans.offsetMax = new Vector2(-10, -10);
                    itemObjectRectTrans.offsetMin = new Vector2(10, 10);
                    RectTransform describeRectTrans = tempItem.transform.Find("Describe").GetComponent<RectTransform>();
                    describeRectTrans.anchorMin = new Vector2(0.0f, 0.0f);
                    describeRectTrans.anchorMax = new Vector2(1, 0.0f);
                    describeRectTrans.localPosition = new Vector3(0, -1, 0);
                    describeRectTrans.sizeDelta = new Vector2(20, 20);
                }
            }
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }
    }

}