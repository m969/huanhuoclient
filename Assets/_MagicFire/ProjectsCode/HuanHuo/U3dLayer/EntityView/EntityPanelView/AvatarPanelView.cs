using System;
using DG.Tweening;
using MagicFire.Common;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using System.Collections;

    public class AvatarPanelView : CombatEntityPanelView
    {
        protected override void FixedUpdate()
        {
            if (Model == null)
                return;
            var v = Camera.main.WorldToScreenPoint(((KBEngine.Model)Model).position);
            transform.DOMove(new Vector3(v.x, v.y + 10, 0), 0.4f);
        }

        public override void InitializeView(IModel model)
        {
            base.InitializeView(model);
            Model.SubscribePropertyUpdate(AvatarPropertys.Sp, Sp_Up);
        }

        private void Sp_Up(object old)
        {
            var val = (int)((KBEngine.Model) Model).getDefinedProperty(AvatarPropertys.Sp);
            if (val != (int)old)
            {
                
            }
        }
    }

}