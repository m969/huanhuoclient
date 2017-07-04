using MagicFire.Mmorpg;

namespace MagicFire.Common
{
    using UnityEngine;
    using System.Collections;

    public partial class SingletonGather
    {
        public static WorldMediator WorldMediator { get { return MagicFire.Mmorpg.WorldMediator.instance; } }
        public static UiManager UiManager { get { return Common.UiManager.instance; } }
        public static GameManager GameManager { get { return Common.GameManager.instance; } }
    }

}