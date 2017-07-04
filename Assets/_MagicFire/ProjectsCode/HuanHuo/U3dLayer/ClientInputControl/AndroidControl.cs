/* *********************************************************
 * Company   
	: MagicFire Studio
 * Autor         
	: Chengmin
 * Description 
	: 
 * Date          
	: 7/2/2017
 * *********************************************************/

namespace MagicFire.Mmorpg.Huanhuo
{
    using UnityEngine;
    using System.Collections;

    //[RequireComponent(typeof())]
    //[AddComponentMenu("")]
    public class AndroidControl : MonoBehaviour
    {
        #region Property and Field
        //[Tooltip("")]
        [SerializeField]
        private ETCJoystick _characterMoveJoystick;
        [SerializeField]
        private ETCJoystick _skillQJoystick;
        [SerializeField]
        private ETCJoystick _skillWJoystick;
        [SerializeField]
        private ETCButton _skillEtcButton;
        #endregion

        #region Private Method
        void Start()
        {
            _characterMoveJoystick.onMoveStart.AddListener(OnMoveStart);
            _characterMoveJoystick.onMove.AddListener(OnMove);
            _characterMoveJoystick.onMoveEnd.AddListener(OnMoveEnd);

            _skillQJoystick.onMoveStart.AddListener(OnSkillQJoystickMoveStart);
            _skillQJoystick.onMove.AddListener(OnSkillQJoystickMove);
            _skillQJoystick.onMoveEnd.AddListener(OnSkillQJoystickMoveEnd);

            _skillWJoystick.onMoveStart.AddListener(OnSkillWJoystickMoveStart);
            _skillWJoystick.onMove.AddListener(OnSkillWJoystickMove);
            _skillWJoystick.onMoveEnd.AddListener(OnSkillWJoystickMoveEnd);

            _skillEtcButton.onDown.AddListener(OnSkillEDown);
        }

        //  void Update()
        //  {
        //
        //  }
        #endregion

        #region Public Method
        //
        public void OnMoveStart()
        {
            if (PlayerInputController.instance)
                PlayerInputController.instance.StartMove();
        }

        public void OnMove(Vector2 vec)
        {
            if (PlayerInputController.instance)
                PlayerInputController.instance.MoveMainAvatar(vec);
        }

        public void OnMoveEnd()
        {
            if (PlayerInputController.instance)
                PlayerInputController.instance.EndMove();
        }

        public void OnSkillQJoystickMoveStart()
        {
            if (PlayerInputController.instance)
                PlayerInputController.instance.SkillQReady();
        }

        public void OnSkillQJoystickMove(Vector2 vec)
        {
            if (PlayerInputController.instance)
                PlayerInputController.instance.OnSkillQReadying(vec);
        }

        public void OnSkillQJoystickMoveEnd()
        {
            if (PlayerInputController.instance)
                PlayerInputController.instance.DoSkillQ();
        }

        public void OnSkillWJoystickMoveStart()
        {
            if (PlayerInputController.instance)
                PlayerInputController.instance.SkillWReady();
        }

        public void OnSkillWJoystickMove(Vector2 vec)
        {
            if (PlayerInputController.instance)
                PlayerInputController.instance.OnSkillWReadying(vec);
        }

        public void OnSkillWJoystickMoveEnd()
        {
            if (PlayerInputController.instance)
                PlayerInputController.instance.DoSkillW();
        }

        public void OnSkillEDown()
        {
            if (PlayerInputController.instance)
                PlayerInputController.instance.DoSkillE();
        }
        //
        #endregion
    }//class_end
}//namespace_end