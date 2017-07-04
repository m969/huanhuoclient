using MagicFire.Common;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;

    public class PlayerDialogPanel : Panel
    {
        public InputField _inputContent;
        public Transform _tranText;
        public Scrollbar _sbVertical;
        private Text textContent;
        private KBEngine.Model _avatar;

        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            transform.SetParent(SingletonGather.UiManager.CanvasLayerFront.transform);
            transform.localScale = new Vector3(1,1,1);
            var rect = GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.0f,0.5f);
            rect.anchorMax = new Vector2(0.0f,0.5f);
            rect.pivot = new Vector2(0.0f, 0.0f);
            //rect.offsetMax = new Vector2(-0, 0);
            //rect.offsetMin = new Vector2(0, 0);            
            rect.anchoredPosition = new Vector2(0, -100);
            rect.sizeDelta = new Vector2(240, 200);
            textContent = _tranText.GetComponent<Text>();

        }

        private void OnEnable()
        {
            if (SingletonGather.WorldMediator.MainAvatarView)
            {
                _avatar = SingletonGather.WorldMediator.MainAvatarView.Model as KBEngine.Avatar;
                if (_avatar != null)
                {
                    _avatar.SubscribeMethodCall("DialogContent", DialogContent);
                }
            }
        }

        private void OnDisable()
        {
            _avatar = SingletonGather.WorldMediator.MainAvatarView.Model as KBEngine.Avatar;
            if (_avatar != null)
            {
                _avatar.DesubscribeMethodCall("DialogContent", DialogContent);
            }
        }

        public void OnMainAvatarActive(KBEngine.Model avatar)
        {
            if (avatar != null)
            {
                _avatar = avatar;
                _avatar.SubscribeMethodCall("DialogContent", DialogContent);
            }
            else
            {
                Debug.LogError("PlayerDialogPanel.OnAvatarActive avatar == null!");
            }
        }

        //当主玩家无效
        public void OnMainAvatarInvalid(KBEngine.Model avatar)
        {
            if (avatar != null)
                avatar.DesubscribeMethodCall("DialogContent", DialogContent);
        }

        public void DialogContent(object[] args)
        {
            //Debug.Log("发送的信息为：" + _inputContent.text);
            if (args[1].ToString().Length > 0)
            {
                textContent.text += ( " " + args[0] + ":" + args[1] + "\n");
            }
            //if (textContent.preferredHeight + 30 > 67)
            //{
            //    _tranText.GetComponent<RectTransform>().sizeDelta = new Vector2(0, textContent.preferredHeight);
            //}

            //_sbVertical.value = 0;
            _inputContent.text = "";
        }

        public void OnSendMessage()
       {
            if(_inputContent.text.Length > 0)
            {
                Debug.Log("发送的信息为：" + _inputContent.text);
                KBEngine.Event.fireIn("SendChatMessage", _inputContent.text);
            }
       }
    }
}


