/* *********************************************************
 * Company   
	: MagicFire Studio
 * Autor         
	: Chengmin
 * Description 
	: 
 * Date          
	: 6/25/2017
 * *********************************************************/

namespace KBEngine
{
    using UnityEngine;

    //[RequireComponent(typeof())]
    //[AddComponentMenu("")]
    public class SpacesManager : Model
    {
        #region Property and Field
        //  [Tooltip("")]
        //  [SerializeField]
        #endregion

        #region Private Method
        //  void Start () 
        //  {
        //
        //  }

        //  void Update()
        //  {
        //
        //  }
        #endregion

        #region Public Method
        //

        public override void __init__()
        {
            base.__init__();
            Debug.Log("SpacesManager:__init__ "+ id);
        }

        //
        #endregion
    }//class_end
}//namespace_end