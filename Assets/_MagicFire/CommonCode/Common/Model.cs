using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace MagicFire.Common
{
    using UnityEngine;
    using System.Collections;

    public class Model : IModel
    {
        public Model(Dictionary<string, Action<object>> propertyUpdateHandlers, Dictionary<string, Action<object[]>> methodCallHandlers)
        {
            _propertyUpdateHandlers = propertyUpdateHandlers;
            _methodCallHandlers = methodCallHandlers;
        }

        private readonly Dictionary<string, Action<object>> _propertyUpdateHandlers;

        private readonly Dictionary<string, Action<object[]>> _methodCallHandlers;

        // ReSharper disable once ConvertPropertyToExpressionBody
        public Dictionary<string, Action<object>> PropertyUpdateHandlers { get { return _propertyUpdateHandlers; } }
        // ReSharper disable once ConvertPropertyToExpressionBody
        public Dictionary<string, Action<object[]>> MethodCallHandlers { get { return _methodCallHandlers; } }

        public void SubscribePropertyUpdate(string name, Action<object> handler)
        {
            throw new NotImplementedException();
        }

        public bool DesubscribePropertyUpdate(string name, Action<object> handler)
        {
            throw new NotImplementedException();
        }

        public void SubscribeMethodCall(string methodName, Action<object[]> handler)
        {
            throw new NotImplementedException();
        }

        public bool DesubscribeMethodCall(string methodName, Action<object[]> handler)
        {
            throw new NotImplementedException();
        }
    }

}