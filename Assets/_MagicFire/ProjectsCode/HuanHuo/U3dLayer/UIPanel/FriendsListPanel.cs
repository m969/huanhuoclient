using MagicFire.Common;
using MagicFire.Common.Plugin;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class FriendsListPanel : Panel
    {
        private bool _hasSub;

        [SerializeField]
        private Transform _friListContent;

        private Object _friListItem;
        public Text _findFriendName;        

        private string _friendName;
        private Transform tempItem;
        //
        private static int _friId;
        private KBEngine.Model _avatar;

        //{郑晓飞 
        //用于以后添加好友时使用
        string goldxFriendsName = "";
        //显示全部好友只调用一次
        bool fristEnableFriendsPanel = true;

        private KBEngine.Avatar _mainAvatar
        {
            get
            {
                return KBEngine.KBEngineApp.app.player() as KBEngine.Avatar;
            }
        }

        protected override void Start()
        {
            base.Start();
            transform.SetParent(SingletonGather.UiManager.CanvasLayerFront.transform);
            _friListItem = AssetTool.LoadAsset_Database_Or_Bundle(
                    AssetTool.Assets__Resources_Ours__UIPanel_ + "Views/FriendsListItem.prefab",
                    "Prefabs",
                    "uipanel_bundle",
                    "FriendsListItem");

            transform.localScale = new Vector3(1, 1, 1);

            var rect = GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(1.0f, 0.5f);
            rect.anchorMax = new Vector2(1.0f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = new Vector2(-97, 0);
            rect.sizeDelta = new Vector2(195, 245);
            
        }

        //开启面板时启用
        private void OnEnable()
        {
            Debug.Log("OnEnable");
            //if (!_hasSub)
            {
                _mainAvatar.SubscribeMethodCall("OnFindFriends", OnFindFriends);
                _mainAvatar.SubscribeMethodCall("OnShowAllFriends", OnShowAllFriends);
                ////订阅查找好友事件
                //SingletonGather.WorldMediator.MainAvatarView.Model.SubscribeMethodCall("OnFindFriends", OnFindFriends);
                ////订阅显示全部好友事件
                //SingletonGather.WorldMediator.MainAvatarView.Model.SubscribeMethodCall("OnShowAllFriends", OnShowAllFriends);
                //_hasSub = true;
            }
            //初始化面板时只调用一次显示全部好友
            if (fristEnableFriendsPanel)
            {
                Debug.Log("first show friends!");
                var avatar = _mainAvatar;
                if (avatar != null)
                    avatar.ShowAllFriends();
                fristEnableFriendsPanel = false;
            }
        }

        private void OnDisable()
        {
            Debug.Log("OnDisable");
            _mainAvatar.DesubscribeMethodCall("OnFindFriends", OnFindFriends);
            _mainAvatar.DesubscribeMethodCall("OnShowAllFriends", OnShowAllFriends);
        }

        //显示所有好友将数据库类型的数据转换为List<object>
        public void OnShowAllFriends(object[] args)
        {
            object avatarFriendsObject = _mainAvatar.getDefinedProperty("avatarFriends");
            Debug.Log(avatarFriendsObject);
            if (avatarFriendsObject == null)
                return;
            ShowAllFriends(((Dictionary<string, object>)avatarFriendsObject)["values"] as List<object>);
        }

        //显示全部好友
        public void ShowAllFriends(List<object> friendNameList)
        {            
            Debug.Log("ShowAllFriends: " + friendNameList.Count);
            //清空面板
            ClearAllFriends();
            //遍历friendNameList
            foreach (object a in friendNameList)
            {
                //实例化新条目
                var item = Instantiate(AssetTool.LoadAsset_Database_Or_Bundle(
                    AssetTool.Assets__Resources_Ours__UIPanel_ + "Views/FriendsListItem.prefab",
                    "Prefabs",
                    "uipanel_bundle",
                    "FriendsListItem"));

                Debug.Log("item == " + item);
                if (item != null)
                {
                    item.name = a as string;
                    //设置父节点
                    ((GameObject)item).transform.SetParent(_friListContent);
                    //显示好有名字
                    ((GameObject)item).GetComponent<FriendsListItem>().friendName = a.ToString();
                }
            }
        }

        //寻找玩家
        public void FindFriendButton()
        {
            var avatar = _mainAvatar;
            if (avatar != null)
                avatar.FindFriends();
        }

        //在客户端进行搜索玩家匹配
        public void OnFindFriends(object[] args)
        {
            Debug.Log(args[0].ToString());
            string[] argsName = args[0].ToString().Split(' ');
            var avatar = _mainAvatar;
            if (_findFriendName.text.Trim().ToString() == avatar.getDefinedProperty("entityName").ToString())
            {
                Debug.Log("自己不能添加自己为好友");
                return;
            }
            foreach (string item in argsName)
            {
                if (item == ("b'"+_findFriendName.text.Trim().ToString()+"'"))
                {
                    //用于下面添加好友
                    goldxFriendsName = _findFriendName.text.ToString();
                    //清除好友面板中所有的好友条目，经搜到的玩家进行显示
                    ClearAllFriends();
                    Debug.Log("clearallfriends ok");
                    if (_friListItem == null)
                    {
                        Debug.LogError("_friListItem is null");
                    }
                    //创建新的条目
                    var friendItem = Instantiate(_friListItem) as GameObject;
                    //条目不为空怎进行名字显示
                    if (friendItem != null)
                    {
                        //设置新条目的父节点
                        friendItem.transform.SetParent(_friListContent);
                        //给新增的条目名字
                        friendItem.name = item;
                        //将friendName给予新增的条目
                        friendItem.GetComponent<FriendsListItem>().friendName = _findFriendName.text.Trim().ToString();
                    }
                    break;
                }                                       
                else
                {
                    Debug.Log("没有此玩家!");
                }
            }
        }
        //添加朋友
        public void AddFriendsButton()
        {
            if(goldxFriendsName != "")
            {
                Debug.Log("FriendsListPanel: Enter AddFriendsButton()");
                var avatar = _mainAvatar;
                if (avatar != null)
                    avatar.AddFriends(goldxFriendsName);
                goldxFriendsName = "";
            }            
        }

        //删除朋友
        public void DeleteFriendsButton()
        {
            if(FriendsListItem._friName != "")
            {
                Debug.Log("FriendsListPanel: Enter DeleteFriendsButton()");
                var avatar = _mainAvatar;
                if (avatar != null)
                    avatar.DeleteFriends(FriendsListItem._friName);
                FriendsListItem._friName = "";
            }           
        }

        //关闭面板
        public void CloseFriendsListButton()
        {
            Debug.Log("Enter CloseFriendsListButton()!");
            gameObject.SetActive(false);
        }

        //清空面板
        public void ClearAllFriends()
        {
            List<GameObject> childObjects = new List<GameObject>();
            for (int i = 0; i < _friListContent.childCount; i++)
            {
                childObjects.Add(_friListContent.GetChild(i).gameObject);
            }

            foreach (GameObject obj in childObjects)
            {
                Destroy(obj);
            }
        }

        public void OnMainAvatarActive(KBEngine.Model avatar)
        {
            if (avatar != null)
            {
                _avatar = avatar;
            }
            else
            {
                Debug.LogError("FriendsListPanel.OnAvatarActive avatar == null!");
            }
        }
    }
}

