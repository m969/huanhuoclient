/* --------------------------
 * Company: MagicFire Studio
 *   Autor: Changmin Yang
 *   类描述: 战斗对象的战斗信息面板
 * -------------------------- */
 using KBEngine;
using MagicFire.Common;
using UnityEngine.UI;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using System.Collections;
    using DG.Tweening;

    public class CombatEntityPanelView : EntityPanelView
    {
        private Slider _hpSlider;
        private Text _damageHint;
        private int _hp;
        private int _hpMax;
        public IValueSlidable Hp = new ValueBar();        //生命值条
        public IValueSlidable Mp = new ValueBar();        //法力值条

        public override void InitializeView(IModel model)
        {
            base.InitializeView(model);

            _hpSlider = GetComponentInChildren<Slider>();
            if (_hpSlider)
            {
                _damageHint = _hpSlider.transform.Find("DamageHint").GetComponent<Text>();
                _damageHint.gameObject.SetActive(false);
            }

            _damageHint.GetComponent<DOTweenAnimation>().onStepComplete.AddListener(HideDamageHint);    //掉血动画结束后隐藏动画

            HandleHpMaxUpdate(0);
            HandleHpUpdate(0);

            model.SubscribePropertyUpdate(CombatPropertys.HpMax, HandleHpMaxUpdate);
            model.SubscribePropertyUpdate(CombatPropertys.Hp, HandleHpUpdate);
        }

        public void HandleHpMaxUpdate(object old)
        {
            int hp = (int)((KBEngine.Model)Model).getDefinedProperty(CombatPropertys.Hp);
            int hpMax = (int)((KBEngine.Model)Model).getDefinedProperty(CombatPropertys.HpMax);

            if (Equals(_hp, hp) && Equals(_hpMax, hpMax))
                return;
            _hp = hp;
            _hpSlider.value = (float)hp / hpMax;
        }

        public void HandleHpUpdate(object old)
        {
            int hp = (int)((KBEngine.Model)Model).getDefinedProperty(CombatPropertys.Hp);
            int hpMax = (int)((KBEngine.Model)Model).getDefinedProperty(CombatPropertys.HpMax);

            if (_hp == hp && _hpMax == hpMax)
                return;
            _hp = hp;
            _hpMax = hpMax;
            _hpSlider.value = (float)hp / hpMax;
            _damageHint.text = "" + (hp - (int)old);
            _damageHint.gameObject.SetActive(true);
        }

        public void HideDamageHint()
        {
            _damageHint.gameObject.SetActive(false);
        }
    }

}