using KBEngine;
using MagicFire.Common;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using System.Collections;
    using UnityEngine.UI;

    public class NpcPanelView : EntityPanelView
    {
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void InitializeView(IModel model)
        {
            base.InitializeView(model);
            GetComponentInChildren<Slider>().gameObject.SetActive(false);
        }
    }

}