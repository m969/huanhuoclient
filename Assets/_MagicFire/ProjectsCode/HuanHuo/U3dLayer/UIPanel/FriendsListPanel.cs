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
        //���������ı���
        public Text _findFriendName;        

        private string _friendName;
        private Transform tempItem;
        //
        private static int _friId;
        private KBEngine.Model _avatar;

        //{֣���� 
        //��¼��ѯ����������
        string goldxFriendsName;
        //������������ʱֻ����һ����ʾ���к���
        bool fristEnableFriendsPanel = true;
        //}

        protected override void Start()
        {
            base.Start();
            //�������б�������Ϊcanvas����
            transform.SetParent(SingletonGather.UiManager.CanvasLayerFront.transform);
            //���¼��䲻Ҫɾ
            _friListItem = AssetTool.LoadAsset_Database_Or_Bundle(
                    AssetTool.Assets__Resources_Ours__UIPanel_ + "Views/FriendsListItem.prefab",
                    "",
                    "",
                    "");

            //_friListContent.CreateChildFromPrefab(_friListItem);
            //_friListContent.GetChild(_friListContent.childCount - 1).transform.GetComponent<FriendsListItem>().friendName = "First";
            //_friListContent.CreateChildFromPrefab(_friListItem as Transform, "xiaofei");
            transform.localScale = new Vector3(1, 1, 1);

            ///����ϵͳ��λ��
            var rect = GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(1.0f, 0.5f);
            rect.anchorMax = new Vector2(1.0f, 0.5f);
            //rect.offsetMax = new Vector2(-0, 0);
            //rect.offsetMin = new Vector2(0, 0);
            //rect.sizeDelta = new Vector2(195, 193);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = new Vector2(-97, 0);
            rect.sizeDelta = new Vector2(195, 245);
            
        }

        //����������������ʱ���Զ�����
        private void OnEnable()
        {
            Debug.Log("OnEnable");
            if (!_hasSub)
            {
                //ע���������¼�
                SingletonGather.WorldMediator.MainAvatarView.Model.SubscribeMethodCall("OnFindFriends", OnFindFriends);
                //ע����ʾ�����¼�
                SingletonGather.WorldMediator.MainAvatarView.Model.SubscribeMethodCall("OnShowAllFriends", OnShowAllFriends);
                _hasSub = true;
            }
            //ֻ�е�һ����ʾ���к���
            if (fristEnableFriendsPanel)
            {
                //��������ʱ��ʾȫ������
                Debug.Log("first show friends!");
                var avatar = SingletonGather.WorldMediator.MainAvatarView.Model as KBEngine.Avatar;
                if (avatar != null)
                    avatar.ShowAllFriends();
                fristEnableFriendsPanel = false;
            }
        }

        private void OnDisable()
        {
            Debug.Log("OnDisable");
            //SingletonGather.WorldMediator.MainAvatarView.Model.DesubscribeMethodCall("OnFindFriends", OnFindFriends);
            //SingletonGather.WorldMediator.MainAvatarView.Model.DesubscribeMethodCall("OnShowAllFriends", OnShowAllFriends);
        }

        //���ݿ��е���������������������ת��
        public void OnShowAllFriends(object[] args)
        {
            object avatarFriendsObject = ((KBEngine.Avatar)SingletonGather.WorldMediator.MainAvatarView.Model).getDefinedProperty("avatarFriends");
            Debug.Log(avatarFriendsObject);
            if (avatarFriendsObject == null)
                return;
            ShowAllFriends(((Dictionary<string, object>)avatarFriendsObject)["values"] as List<object>);
        }
        //��ʾ���к���
        public void ShowAllFriends(List<object> friendNameList)
        {            
            Debug.Log("ShowAllFriends: " + friendNameList.Count);
            //��ʾǰ�����պ����б�
            ClearAllFriends();
            //����args
            foreach (object a in friendNameList)
            {
                //ʵ����һ��������Ŀ
                var item = Instantiate(AssetTool.LoadAsset_Database_Or_Bundle(
                    AssetTool.Assets__Resources_Ours__UIPanel_ + "Views/FriendsListItem.prefab",
                    "",
                    "",
                    ""));

                Debug.Log("item == " + item);
                //��Ŀ������
                if (item != null)
                {
                    item.name = a as string;
                    //������Ŀ�ĸ��ڵ�
                    ((GameObject)item).transform.SetParent(_friListContent);
                    //����Ŀ�µ�friend��ֵ
                    ((GameObject)item).GetComponent<FriendsListItem>().friendName = a.ToString();
                }
            }
        }

        //�ͻ��˲��ҷ����˺����Ƿ�����
        public void FindFriendButton()
        {
            var avatar = SingletonGather.WorldMediator.MainAvatarView.Model as KBEngine.Avatar;
            if (avatar != null)
                avatar.FindFriends();
        }

        //ƥ���Ƿ����ڴ�����
        public void OnFindFriends(object[] args)
        {
            Debug.Log(args[0].ToString());
            foreach (object item in args)
            {
                if (item.ToString() == _findFriendName.text.ToString())
                {
                    //����ѯ���ĺ������ּ�¼�����������Ժ�����������
                    goldxFriendsName = _findFriendName.text.ToString();
                    //��ʾǰ�����պ����б�
                    ClearAllFriends();
                    Debug.Log("clearallfriends ok");
                    if (_friListItem == null)
                    {
                        Debug.LogError("_friListItem is null");
                    }
                    //ʵ����һ��������Ŀ
                    var friendItem = Instantiate(_friListItem) as GameObject;
                    //������Ŀ�ĸ��ڵ�
                    if (friendItem != null)
                    {
                        friendItem.transform.SetParent(_friListContent);
                        //��Ŀ������
                        friendItem.name = item as string;
                        //
                        friendItem.GetComponent<FriendsListItem>().friendName = item as string;
                    }
                    break;
                }                                       
                else
                {
                    Debug.Log("没有此玩家!");
                }
            }
        }
        //���Ӻ���
        public void AddFriendsButton()
        {
            Debug.Log("FriendsListPanel: Enter AddFriendsButton()");
            //KBEngine.Event.fireIn("AddFriends",_friendName);
            var avatar = SingletonGather.WorldMediator.MainAvatarView.Model as KBEngine.Avatar;
            if (avatar != null)
                avatar.AddFriends(goldxFriendsName);
            //(SingletonGather.WorldMediator.MainAvatarView.Model as KBEngine.Avatar).ShowAllFriends();
        }

        //ɾ������
        public void DeleteFriendsButton()
        {
            Debug.Log("FriendsListPanel: Enter DeleteFriendsButton()");
            //KBEngine.Event.fireIn("DeleteFriends", _friendName);
            var avatar = SingletonGather.WorldMediator.MainAvatarView.Model as KBEngine.Avatar;
            if (avatar != null)
                avatar.DeleteFriends();
        }

        //�رպ����б���ť
        public void CloseFriendsListButton()
        {
            Debug.Log("Enter CloseFriendsListButton()!");
            gameObject.SetActive(false);
        }
        //���պ���
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

