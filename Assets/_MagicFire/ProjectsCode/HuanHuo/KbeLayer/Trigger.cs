using UnityEngine;
using System;
using System.Collections;

namespace KBEngine
{
    public class Trigger : Model
    {
        public Trigger()
        {

        }

        public override void __init__()
        {
            base.__init__();
        }

        public override void onDestroy()
        {
            base.onDestroy();
        }

        public override void onEnterWorld()
        {
            base.onEnterWorld();
        }

        //public virtual void set_triggerSize(object old)
        //{
        //    object v = getDefinedProperty("triggerSize");
        //    Event.fireOut("set_triggerSize", new object[] { this, v });
        //}

        //public virtual void set_parentSkill(object old)
        //{
        //    object v = getDefinedProperty("parentSkill");
        //    Event.fireOut("set_parentSkill", new object[] { this, v });
        //}
    }
}