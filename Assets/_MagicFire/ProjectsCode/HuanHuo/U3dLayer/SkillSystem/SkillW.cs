/* *********************************************************
 * Company   
	: MagicFire Studio
 * Autor         
	: Changmin
 * Description 
	: 
 * Date          
	: 5/28/2017
 * *********************************************************/

using MagicFire.SceneManagement;

namespace MagicFire.Mmorpg.Skill
{
    using UnityEngine;
    using System.Collections;

    //[RequireComponent(typeof())]
    //[AddComponentMenu("")]
    public class SkillW : Skill
    {
        public Vector2 SkillTrajectoryPosition { get; set; }

        public SkillW(AvatarView spellcaster) : base(spellcaster) { }

        public override void Ready(AvatarView spellcaster)
        {
            base.Ready(spellcaster);

            if (Application.platform == RuntimePlatform.Android)
            {
                SkillTrajectory.transform.position = new Vector3(spellcaster.transform.position.x + SkillTrajectoryPosition.x * 20, spellcaster.transform.position.y, spellcaster.transform.position.z + SkillTrajectoryPosition.y * 20);
            }
            else
            {
                if (XmlSceneManager.Instance.ControlMode == XmlSceneManager.ControlModeEnum.PcControl)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("Terrian"));
                    SkillTrajectory.transform.position = hit.point;
                    SkillTrajectory.transform.position = new Vector3(SkillTrajectory.transform.position.x, SkillTrajectory.transform.position.y + 0.5f, SkillTrajectory.transform.position.z);
                    if (Input.GetMouseButtonDown(0))
                    {
                        KBEngine.Event.fireIn("RequestDoSkillW", new object[] { hit.point });
                        spellcaster.SkillManager.CancelReady();
                    }
                }
                else
                {
                    SkillTrajectory.transform.position = new Vector3(spellcaster.transform.position.x + SkillTrajectoryPosition.x * 20, spellcaster.transform.position.y, spellcaster.transform.position.z + SkillTrajectoryPosition.y * 20);
                }
            }
        }

        public override void CancelReady()
        {
            base.CancelReady();
        }

        public override void Conjure()
        {
            base.Conjure();
            if (!_spellcaster.Animation)
            {
                return;
            }
            if (!_spellcaster.Animation.IsPlaying("Attack"))
            {
                _spellcaster.Animation.Play("Attack");
            }
            KBEngine.Event.fireIn("RequestDoSkillW", new object[] { SkillTrajectory.transform.position });
            _spellcaster.SkillManager.CancelReady();
        }
    }
}//namespace_end