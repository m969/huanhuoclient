using System;
using System.Collections.Generic;

namespace MagicFire.Common
{

    public interface IModel
    {

        Dictionary<string, Action<object>> PropertyUpdateHandlers { get; }

        Dictionary<string, Action<object[]>> MethodCallHandlers { get; }

        void SubscribePropertyUpdate(string name, Action<object> handler);

        bool DesubscribePropertyUpdate(string name, Action<object> handler);

        void SubscribeMethodCall(string methodName, Action<object[]> handler);

        bool DesubscribeMethodCall(string methodName, Action<object[]> handler);
    }

}