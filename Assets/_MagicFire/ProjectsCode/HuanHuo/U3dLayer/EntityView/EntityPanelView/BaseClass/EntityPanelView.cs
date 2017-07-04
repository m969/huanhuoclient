/* --------------------------
 * Company: MagicFire Studio
 *   Autor: Changmin Yang
 *    Date: 2017/02/20
 *    描述： 实体属性的UI面板，将实体的一些比较主要的属性可视化，比如名字、血条、状态等；
 * -------------------------- */
using System;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;
    using MagicFire.Common;
    using KBEngine;
    using DG.Tweening;

    public class EntityPanelView : View
    {
        private Text _nameText;//用于显示实体姓名的Text组件
        protected string _entityName;//实体的姓名

        protected virtual void FixedUpdate()
        {
            if (Model == null)
                return;
            var v = Camera.main.WorldToScreenPoint(((KBEngine.Model)Model).position);
            transform.DOMove(new Vector3(v.x, v.y, 0), 0.1f);
        }

        public override void InitializeView(IModel model)
        {
            base.InitializeView(model);
            GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _nameText = transform.Find("Name").GetComponentInChildren<Text>();
            HandleEntityNameUpdate(0);
            model.SubscribePropertyUpdate(EntityPropertys.EntityName, HandleEntityNameUpdate);//订阅姓名属性的更新
            model.SubscribeMethodCall("OnEntityDestroy", OnModelDestrooy);//订阅OnEntityDestroy方法的调用
        }

        public override void OnModelDestrooy(object[] objects)
        {
            if (Model != null)
            {
                Model.DesubscribePropertyUpdate(EntityPropertys.EntityName, HandleEntityNameUpdate);//取消订阅
                Model.DesubscribeMethodCall("OnEntityDestroy", OnModelDestrooy);//取消订阅
                ((KBEngine.Model)Model).renderObj = null;
            }
            base.OnModelDestrooy(objects);
        }

        /// <summary>
        /// 处理实体姓名的更新：如果实体的姓名变了，那么View的显示实体姓名的Text组件（_nameText）也要相应作出改变
        /// </summary>
        /// <param name="old"></param>
        public void HandleEntityNameUpdate(object old)
        {
            if (_entityName == ((KBEngine.Model)Model).getDefinedProperty(EntityPropertys.EntityName) as string) return;
            if (_nameText)
            {
                _nameText.text = _entityName = ((KBEngine.Model)Model).getDefinedProperty(EntityPropertys.EntityName) as string;
                gameObject.name = ((KBEngine.Model)Model).className + ":" + _entityName;
            }
        }

    }

}