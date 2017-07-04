using MagicFire.Common;
using MagicFire.Common.Plugin;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class StorePanel : Panel
    {
        private List<object> _storeGoodsIdList = new List<object>();
        private List<object> _oldStoreGoodsIdList = new List<object>();
        private bool _hasAssign;//assign分配
        private bool _hasCreateOnce;
        static public int _tempId = 0;//By Jc
        public List<object> StoreGoodsIdList
        {
            get
            {
                return _storeGoodsIdList;
            }
            set
            {
                if (_hasAssign == false)
                {
                    _storeGoodsIdList = value;
                    _oldStoreGoodsIdList = new List<object>(_storeGoodsIdList.ToArray());
                    _hasAssign = true;
                }
                else
                {
                    _storeGoodsIdList = value;
                }
            }
        }
        public NpcView CurrentNpc { get; set; }
        [SerializeField]
        private Transform _content;

        protected override void Start()
        {
            base.Start();
            transform.SetParent(UiManager.instance.CanvasLayers[1].transform);
            transform.localPosition = new Vector3(0, 0, 0);
        }

        protected override void Update()
        {
            base.Update();
            if (CurrentNpc)
            {
                StoreGoodsIdList = ((KBEngine.Npc)CurrentNpc.Model).storeGoodsIDList;
            }
            if (!_hasCreateOnce)
            {
                CreateItem();
                _hasCreateOnce = true;
            }
            var i = _oldStoreGoodsIdList.Except(_storeGoodsIdList).ToList().Count;
            var j = _storeGoodsIdList.Except(_oldStoreGoodsIdList).ToList().Count;
            if (i != 0 || j != 0)
            {
                _oldStoreGoodsIdList = new List<object>(_storeGoodsIdList.ToArray());
            }
            if (i == 0 && j == 0)
            {
                return;
            }
            CreateItem();
        }
       
        //By Jc
        public void ChangeColor()
        {
            if (_tempId == 0)
            {
                _content.FindChild(_selectItemID.ToString() + "(Clone)").transform.Find("Image2").gameObject.SetActive(true);
            }
            if (_tempId != 0)
            {
                if (_tempId != _selectItemID)
                {
                    //Debug.Log(_selectItemID.ToString() + "(Clone)");
                    _content.FindChild(_selectItemID.ToString() + "(Clone)").transform.Find("Image2").gameObject.SetActive(true);
                    _content.FindChild(_tempId.ToString() + "(Clone)").transform.Find("Image2").gameObject.SetActive(false);
                }
            }
           
            _tempId = _selectItemID;
        }
        void CreateItem()
        {
            for (int i = 0; i < _content.childCount; i++)
            {
                Destroy(_content.GetChild(i).gameObject);
            }
            foreach (var goodsId in _storeGoodsIdList)
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
                }
            }
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void Buy()
        {
            if (_selectItemID > 0)
            {
                KBEngine.Event.fireIn("RequestBuyGoods", SingletonGather.WorldMediator.CurrentSpaceId, CurrentNpc.EntityName, _selectItemID);
                Debug.Log("购买商品 " + _selectItemID);
            }
            else
            {
                Debug.Log("请选择商品");
            }
        }
    }

}