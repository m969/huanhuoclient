/* *********************************************************
 * Company   
	: MagicFire Studio
 * Autor         
	: Changmin
 * Description 
	: 
 * Date          
	: 6/9/2017
 * *********************************************************/

using MagicFire.Common;

namespace MagicFire.Mmorpg.Huanhuo
{
    using UnityEngine;
    using System.Collections;
    using MagicFire.Mmorpg.UI;

    //[RequireComponent(typeof())]
    //[AddComponentMenu("")]
    public class SceneLoadPanel : Panel
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
        //
        #endregion
    }//class_end
}//namespace_end