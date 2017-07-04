namespace MagicFire.Common
{
    using UnityEngine;
    using System.Collections;
    using KBEngine;
    using System;
    using Model = KBEngine.Model;

    public class View : MonoBehaviour, IView
    {
        public IModel Model { get; private set; }//实体Model（在kbe的框架中，Entity即Model，Model即Entity），包含了实体的所有主要属性
        /// <summary>
        /// 初始化View：View是Model的可视化，不管是ObjectView还是PanelView，这个方法一般会在实例化一个View后被调用，以使得View具有正确的数据可以可视化
        /// </summary>
        /// <param name="model"></param>
        public virtual void InitializeView(IModel model)
        {
            Model = model;
        }
        /// <summary>
        /// 在实体销毁时此方法会被触发，View通常在这里销毁自己
        /// </summary>
        /// <param name="objects"></param>
        public virtual void OnModelDestrooy(object[] objects)
        {
            Destroy(gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (Model != null)
            {
                ((KBEngine.Model)Model).renderObj = null;
            }
        }
    }
}