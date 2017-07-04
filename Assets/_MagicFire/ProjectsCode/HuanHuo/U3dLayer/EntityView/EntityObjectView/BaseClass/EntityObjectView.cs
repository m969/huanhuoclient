/* --------------------------
 * Company: MagicFire Studio
 *   Autor: Changmin Yang
 *    Date: 2017/02/20
 *    描述： 
 * -------------------------- */
namespace MagicFire.Mmorpg
{
    using System;
    using UnityEngine;
    using System.Collections;
    using Mmorpg.UI;
    using MagicFire.Common;
    using KBEngine;
    using Model = KBEngine.Model;

    public abstract class EntityObjectView : View
    {
        public string EntityName { get; set; }

        public override void InitializeView(IModel model)
        {
            base.InitializeView(model);
            EntityNameUpdateHandle(0);
            model.SubscribePropertyUpdate(EntityPropertys.EntityName, EntityNameUpdateHandle);
            model.SubscribeMethodCall("OnEntityDestroy", OnModelDestrooy);
        }

        public override void OnModelDestrooy(object[] objects)
        {
            if (Model != null)
            {
                Model.DesubscribePropertyUpdate(EntityPropertys.EntityName, EntityNameUpdateHandle);
                Model.DesubscribeMethodCall("OnEntityDestroy", OnModelDestrooy);
                ((KBEngine.Model)Model).renderObj = null;
            }
            base.OnModelDestrooy(objects);
        }

        public void EntityNameUpdateHandle(object val)
        {
            if (EntityName == (string)((KBEngine.Model)Model).getDefinedProperty(EntityPropertys.EntityName))
                return;
            EntityName = (string)((KBEngine.Model)Model).getDefinedProperty(EntityPropertys.EntityName);
            var obj = ((KBEngine.Model)Model).renderObj as GameObject;
            if (obj != null)
            {
                obj.name = ((KBEngine.Model)Model).className + ":" + EntityName;
            }
        }
    }
}