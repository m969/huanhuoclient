using UnityEngine;
using System;
using System.Collections;

namespace KBEngine
{
    public class Monster : CombatModel
    {
        public Monster()
        {

        }

        public override void __init__()
        {
            base.__init__();
        }

        public override void onEnterWorld()
        {
            base.onEnterWorld();
        }

        public override void onDestroy()
        {
            base.onDestroy();
        }

  //      #region 接收服务端的调用并向u3d表现层抛出事件
  //      /*
		//	以下函数是实体的属性被设置时插件底层调用
		//	set_属性名称(), 想监听哪个属性就实现该函数，事件触发后由于GameWorld.cs中监听了该事件，GameWorld.cs会取出数据做行为表现。
		//	另外，这些属性如果需要同步到客户端，前提是在def中定义过该属性，并且属性的广播标志为ALL_CLIENTS、OTHER_CLIENTS、等等，
		//	参考：http://www.kbengine.org/cn/docs/programming/entitydef.html
		//*/
  //      public void set_HP(object old)
  //      {
  //          object v = getDefinedProperty("HP");
  //          Event.fireOut("set_HP", new object[] { this, v });
  //      }

  //      public void set_HP_Max(object old)
  //      {
  //          object v = getDefinedProperty("HP_Max");
  //          Event.fireOut("set_HP_Max", new object[] { this, v });
  //      }
  //      #endregion
    }
}