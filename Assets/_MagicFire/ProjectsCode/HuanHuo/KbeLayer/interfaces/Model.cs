/* --------------------------
 * Company: MagicFire Studio
 *   Autor: Changmin Yang
 *    Date: 2017/02/20
 *    描述： 封装Entity为Model，使之适用于MVC模式
 * -------------------------- */
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MagicFire.Common;

namespace KBEngine
{
    public class Model : Entity, IModel
    {
        private readonly Dictionary<string, Action<object>> _propertyUpdateHandlers = new Dictionary<string, Action<object>>();
        // ReSharper disable once ConvertPropertyToExpressionBody
        public Dictionary<string, Action<object>> PropertyUpdateHandlers { get { return _propertyUpdateHandlers; } }

        private readonly Dictionary<string, Action<object[]>> _methodCallHandlers = new Dictionary<string, Action<object[]>>();
        // ReSharper disable once ConvertPropertyToExpressionBody
        public Dictionary<string, Action<object[]>> MethodCallHandlers { get { return _methodCallHandlers; } }

        public void SubscribePropertyUpdate(string name, Action<object> handler)
        {
            Action<object> handlers;
            _propertyUpdateHandlers.TryGetValue(name, out handlers);

            if (handlers == null)
            {
                _propertyUpdateHandlers.Add(name, handler);
            }
            else
            {
                if (handler != null) handlers += handler;
                _propertyUpdateHandlers.TryAddOrReplace(name, handlers);
            }
        }

        public void SubscribeMethodCall(string methodName, Action<object[]> handler)
        {
            Action<object[]> handlers;
            _methodCallHandlers.TryGetValue(methodName, out handlers);

            if (handlers == null)
            {
                if (_methodCallHandlers.ContainsKey(methodName))
                {
                    handlers += handler;
                    _methodCallHandlers[methodName] = handlers;
                }
                else
                {
                    _methodCallHandlers.Add(methodName, handler);
                }
            }
            else
            {
                if (handler != null) handlers += handler;
                _methodCallHandlers.TryAddOrReplace(methodName, handlers);
            }
        }

        public bool DesubscribePropertyUpdate(string name, Action<object> handler)
        {
            Action<object> handlers;
            _propertyUpdateHandlers.TryGetValue(name, out handlers);

            if (handlers == null)
            {
                return true;
            }
            else
            {
                if (handler != null) handlers -= handler;
                _propertyUpdateHandlers.TryAddOrReplace(name, handlers);
            }
            return false;
        }

        public bool DesubscribeMethodCall(string methodName, Action<object[]> handler)
        {
            Action<object[]> handlers;
            _methodCallHandlers.TryGetValue(methodName, out handlers);

            if (handlers == null)
            {
                return true;
            }
            else
            {
                if (handler != null) handlers -= handler;
                _methodCallHandlers.TryAddOrReplace(methodName, handlers);
            }
            return false;
        }

        public override void onEnterWorld()
        {
            base.onEnterWorld();

            if (isPlayer())
            {
                //Event.fireOut("onAvatarEnterWorld", new object[] { KBEngineApp.app.entity_uuid, id, this }); 
            }
            else
            {
                if (!SingletonGather.WorldMediator.IsSceneLoadComplete)
                    return;
                Event.fireOut("onEnterWorld", new object[] { this });
            }
        }

        public override void onDestroy()
        {
            Event.fireOut("onRemoteMethodCall_", this, "OnEntityDestroy", null);
            base.onDestroy();
        }
    }
}