using KBEngine;
using MagicFire.Common;
using DG.Tweening;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using System.Collections;

    public class MonsterPanelView : CombatEntityPanelView
    {
        protected override void FixedUpdate()
        {
            if (Model == null)
                return;
            var v = Camera.main.WorldToScreenPoint(((KBEngine.Model)Model).position);
            if (_entityName == "狼兽人怪")
                transform.DOMove(new Vector3(v.x, v.y + 4, 0), 0.2f);
            else
                transform.DOMove(new Vector3(v.x, v.y, 0), 0.2f);
        }

        public override void InitializeView(IModel model)
        {
            base.InitializeView(model);
        }
    }

}