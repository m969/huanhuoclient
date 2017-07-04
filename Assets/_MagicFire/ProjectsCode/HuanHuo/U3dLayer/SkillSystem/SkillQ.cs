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
    public class SkillQ : Skill
    {
        public Vector2 SkillDirection { get; set; }

        public SkillQ(AvatarView spellcaster) : base(spellcaster) { }

        public override void Ready(AvatarView spellcaster)
        {
            base.Ready(spellcaster);

            if (SkillTrajectory == null)
            {
                Debug.LogError("SkillTrajectory == null");
            }
            if (spellcaster == null)
            {
                Debug.LogError("spellcaster == null");
            }
            if (SkillTrajectory == null || spellcaster == null)
            {
                return;
            }
            SkillTrajectory.transform.position = spellcaster.transform.position;
            if (Application.platform == RuntimePlatform.Android)
            {
                SkillTrajectory.transform.LookAt(
                    new Vector3(
                        spellcaster.transform.position.x + SkillDirection.x, 
                        spellcaster.transform.position.y, 
                        spellcaster.transform.position.z + SkillDirection.y));
            }
            else
            {
                if (XmlSceneManager.Instance.ControlMode == XmlSceneManager.ControlModeEnum.PcControl)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("Terrian"));
                    SkillTrajectory.transform.LookAt(new Vector3(hit.point.x, spellcaster.transform.position.y,
                        hit.point.z));
                    if (Input.GetMouseButtonDown(0))
                    {
                        KBEngine.Event.fireIn(
                            "RequestDoSkillQ",
                            new object[]
                            {
                                SkillTrajectory.transform.Find("SkillPoint").position,
                                Mathf.Deg2Rad*SkillTrajectory.transform.rotation.eulerAngles.y
                            });
                        spellcaster.SkillManager.CancelReady();
                    }
                }
                else
                {
                    SkillTrajectory.transform.LookAt(
                        new Vector3(
                            spellcaster.transform.position.x + SkillDirection.x,
                            spellcaster.transform.position.y,
                            spellcaster.transform.position.z + SkillDirection.y));
                }
            }
        }

        public override void CancelReady()
        {
            base.CancelReady();
        }

        public override void Conjure()
        {
            if (!_spellcaster.Animation)
            {
                return;
            }
            if (!_spellcaster.Animation.IsPlaying("Attack"))
            {
                _spellcaster.Animation.Play("Attack");
            }
            KBEngine.Event.fireIn("RequestDoSkillQ", new object[] { SkillTrajectory.transform.Find("SkillPoint").position, Mathf.Deg2Rad * SkillTrajectory.transform.rotation.eulerAngles.y });
            _spellcaster.SkillManager.CancelReady();
        }
    }//class_end
}//namespace_end