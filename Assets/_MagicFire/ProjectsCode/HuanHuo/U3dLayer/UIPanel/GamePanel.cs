using System;
using MagicFire.Common;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;

    public class GamePanel : Panel
    {
        [SerializeField]
        private Image _hpSlider;
        [SerializeField]
        private Image _mpSlider;

        [SerializeField]
        private Text _hpAmounText;
        [SerializeField]
        private Text _mpAmounText;

        private KBEngine.Model _avatar;

        protected override void Start()
        {
            base.Start();
            StretchLayout();
            ShowPanelByName("BagPanel");
            ShowPanelByName("TheStorePanel");
            ShowPanelByName("TaskInfoListPanel");
            ShowPanelByName("FriendsListPanel");
            ShowPanelByName("CharacterInfoPanel");
        }

        //当主玩家激活
        public void OnMainAvatarActive(KBEngine.Model avatar)
        {
            if (avatar != null)
            {
                _avatar = avatar;
                Subscribe();
            }
            else
                Debug.LogError("GamePanel.OnAvatarActive avatar == null!");
        }

        //当主玩家无效
        public void OnMainAvatarInvalid(KBEngine.Model avatar)
        {
            Debug.Log("GamePanel.OnAvatarInvalid");
            if (_avatar != null)
                Desubscribe();
        }

        private void OnEnable()
        {
            if (_avatar != null)
                Subscribe();
        }

        private void OnDisable()
        {
            if (_avatar != null)
                Desubscribe();
        }

        //订阅属性更新、方法调用
        private void Subscribe()
        {
            Hp_Up(null);
            Mp_Up(null);
            HpMax_Up(null);
            MpMax_Up(null);
            _avatar.SubscribePropertyUpdate(CombatPropertys.Hp, Hp_Up);
            _avatar.SubscribePropertyUpdate(AvatarPropertys.Msp, Mp_Up);
            _avatar.SubscribePropertyUpdate(CombatPropertys.HpMax, HpMax_Up);
            _avatar.SubscribePropertyUpdate(AvatarPropertys.MspMax, MpMax_Up);
            _avatar.SubscribeMethodCall("OnEntityDestoy", OnModelDestroy);
        }

        private void Desubscribe()
        {
            _avatar.DesubscribePropertyUpdate(CombatPropertys.Hp, Hp_Up);
            _avatar.DesubscribePropertyUpdate(AvatarPropertys.Msp, Mp_Up);
            _avatar.DesubscribePropertyUpdate(CombatPropertys.HpMax, HpMax_Up);
            _avatar.DesubscribePropertyUpdate(AvatarPropertys.MspMax, MpMax_Up);
            _avatar.DesubscribeMethodCall("OnEntityDestoy", OnModelDestroy);
        }

        private void OnModelDestroy(object[] objects)
        {
            Desubscribe();
        }

        //生命值UI更新
        private void Hp_Up(object old)
        {
            if (_avatar != null)
            {
                var hp = (int)_avatar.getDefinedProperty(CombatPropertys.Hp);
                var maxHp = (int)_avatar.getDefinedProperty(CombatPropertys.HpMax);
                if (_hpSlider != null)
                    _hpSlider.fillAmount = (float)hp / maxHp;
                if (_hpAmounText != null)
                    _hpAmounText.text = "" + hp + "/" + maxHp;
            }
        }

        //灵力值UI更新
        private void Mp_Up(object old)
        {
            if (_avatar != null)
            {
                var mp = (int)_avatar.getDefinedProperty(AvatarPropertys.Msp);
                var maxMp = (int)_avatar.getDefinedProperty(AvatarPropertys.MspMax);
                if (_mpSlider != null)
                    _mpSlider.fillAmount = (float)mp / maxMp;
                if (_mpAmounText != null)
                    _mpAmounText.text = "" + mp + "/" + maxMp;
            }
        }

        //生命值上限UI更新
        private void HpMax_Up(object old)
        {
            if (_avatar != null)
            {
                var hp = (int)_avatar.getDefinedProperty(CombatPropertys.Hp);
                var maxHp = (int)_avatar.getDefinedProperty(CombatPropertys.HpMax);
                if (_hpSlider != null)
                    _hpSlider.fillAmount = (float)hp / maxHp;
                if (_hpAmounText != null)
                    _hpAmounText.text = "" + hp + "/" + maxHp;
            }
        }

        //灵力值上限UI更新
        private void MpMax_Up(object old)
        {
            if (_avatar != null)
            {
                var mp = (int)_avatar.getDefinedProperty(AvatarPropertys.Msp);
                var maxMp = (int)_avatar.getDefinedProperty(AvatarPropertys.MspMax);
                if (_mpSlider != null)
                    _mpSlider.fillAmount = (float)mp / maxMp;
                if (_mpAmounText != null)
                    _mpAmounText.text = "" + mp + "/" + maxMp;
            }
        }

        public void ExitGame()
        {
#if UNITY_EDITOR

#endif
            //if (UnityEngine.Application.isEditor)
            //{
            //    UnityEditor.EditorApplication.isPlaying = false;
            //    return;
            //}
            UnityEngine.Application.Quit();
        }

        public void ShowPanelByName(string panelName)
        {
            var panel = UiManager.instance.TryGetOrCreatePanel(panelName);
            ActiveOrHide(panel);
        }

        private void ActiveOrHide(GameObject panel)
        {
            if (panel == null)
                return;
            if (panel.activeInHierarchy)
                panel.SetActive(false);
            else
                panel.SetActive(true);
        }
    }

}