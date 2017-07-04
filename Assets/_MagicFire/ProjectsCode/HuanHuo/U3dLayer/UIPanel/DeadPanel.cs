/* *********************************************************
 * Company   
	: MagicFire Studio
 * Autor         
	: Chengmin
 * Description 
	: 
 * Date          
	: 6/27/2017
 * *********************************************************/

using MagicFire.Mmorpg.UI;

namespace MagicFire.Mmorpg.Huanhuo
{
    using UnityEngine;
    using System.Collections;

    //[RequireComponent(typeof())]
    //[AddComponentMenu("")]
    public class DeadPanel : Panel
    {
        #region Property and Field
        //  [Tooltip("")]
        //  [SerializeField]
        #endregion

        #region Private Method
        protected override void Start()
        {
            base.Start();
            StretchLayout();
        }

        //  void Update()
        //  {
        //
        //  }
        #endregion

        #region Public Method
        //
        public override void Initialize()
        {
            base.Initialize();
        }

        #endregion
    }//class_end
}//namespace_end