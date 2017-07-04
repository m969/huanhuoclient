namespace MagicFire.Mmorpg.Skill
{
    using UnityEngine;
    using System.Collections;
    using MagicFire.Mmorpg;
    using MagicFire.Common.Plugin;

    public class Skill
    {
        public GameObject SkillTrajectory
        {
            get;
            set;
        }
        protected AvatarView _spellcaster;

        public Skill(AvatarView spellcaster)
        {
            _spellcaster = spellcaster;
            if (SkillTrajectory == null)
            {
                var className = this.GetType().Name;
                var prefab = 
                    AssetTool.LoadAsset_Database_Or_Bundle(
                        AssetTool.Assets__Resources_Ours__Prefabs_ + "Trigger/Skill/" + className + "/" + className + "_SkillTrajectory.prefab",
                        "Prefabs",
                        "trigger_bundle",
                        className + "_SkillTrajectory");

                if (prefab != null)
                {
                    SkillTrajectory = Object.Instantiate(prefab) as GameObject;
                    if (SkillTrajectory != null) SkillTrajectory.SetActive(false);
                }
                else
                {
                    Debug.LogError(className + " SkillTrajectory prefab == null!");
                }
            }
        }

        //技能预备
        public virtual void Ready(AvatarView spellcaster)
        {
            if (SkillTrajectory == null)
            {
                var prefab = 
                    AssetTool.LoadAsset_Database_Or_Bundle(
                        AssetTool.Assets__Resources_Ours__Prefabs_ + "Trigger/Skill/" + this.GetType().Name + "/SkillTrajectory.prefab",
                        "Prefabs",
                        "trigger_bundle",
                        this.GetType().Name + "_SkillTrajectory");

                if (prefab != null)
                {
                    SkillTrajectory = Object.Instantiate(prefab) as GameObject;
                }
            }
            else
            {
                if (SkillTrajectory.activeInHierarchy == false)
                {
                    SkillTrajectory.SetActive(true);
                }
            }
        }
        //取消技能预备
        public virtual void CancelReady()
        {
            if (SkillTrajectory != null)
            {
                SkillTrajectory.SetActive(false);
            }
        }
        //技能施放
        public virtual void Conjure()
        {

        }
    }
}