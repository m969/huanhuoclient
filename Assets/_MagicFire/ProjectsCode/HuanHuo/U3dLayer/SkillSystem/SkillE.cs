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

namespace MagicFire.Mmorpg.Skill
{
    using UnityEngine;
    using System.Collections;

    //[RequireComponent(typeof())]
    //[AddComponentMenu("")]
    public class SkillE : Skill
    {
        public SkillE(AvatarView spellcaster) : base(spellcaster) { }

        public override void Ready(AvatarView spellcaster)
        {
            base.Ready(spellcaster);
            if (SkillTrajectory)
            {
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //RaycastHit hit;
                //Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("Terrian"));
                //SkillTrajectory.transform.position = new Vector3(spellcaster.transform.position.x, spellcaster.transform.position.y + 0.5f, spellcaster.transform.position.z);
                //SkillTrajectory.transform.LookAt(new Vector3(hit.point.x, SkillTrajectory.transform.position.y, hit.point.z));
                //if (Input.GetMouseButtonDown(0))
                //{
                //    //KBEngine.Event.fireIn("RequestDoSkillE", new object[] { hit.point });
                //    spellcaster.SkillManager.CancelReady();
                //}
            }
        }

        public override void CancelReady()
        {
            base.CancelReady();
        }

        public override void Conjure()
        {
            KBEngine.Event.fireIn("RequestDoSkillW", new object[] { SkillTrajectory.transform.position });
        }
    }
}//namespace_end