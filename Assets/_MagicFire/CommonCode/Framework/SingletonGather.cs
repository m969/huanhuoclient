namespace MagicFire.Common
{
    using UnityEngine;
    using System.Collections;
    using MagicFire;

    public partial class SingletonGather
    {
        public static IFactorysFactory FactorysFactory { get { return MagicFire.FactorysFactory.instance; } }
    }

}