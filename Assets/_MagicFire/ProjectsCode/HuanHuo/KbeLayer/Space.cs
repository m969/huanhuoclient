using MagicFire.Common;

namespace KBEngine
{
    using UnityEngine;
    using System.Collections;

    public class Space : Model
    {
        public override void __init__()
        {
            base.__init__();

        }

        public override void onEnterSpace()
        {
            base.onEnterSpace();            
        }

        public override void onEnterWorld()
        {
            base.onEnterWorld();
            SingletonGather.WorldMediator.CurrentSpaceModel = this;
        }
    }

}