/* --------------------------
 * Company: MagicFire Studio
 *   Autor: Changmin Yang
 *   类描述: 
 * -------------------------- */
using KBEngine;
using MagicFire.Common;

namespace MagicFire.Mmorpg
{
    using UnityEngine;
    using System.Collections;
    using System;
    using MagicFire.Common.Plugin;

    public class TriggerView : EntityObjectView
    {
        [SerializeField, Range(0, 4)]
        private int _triggerSize = 1;

        private GameObject _myTriggerObject;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(_triggerSize, _triggerSize, _triggerSize));
        }

        public override void InitializeView(IModel model)
        {
            base.InitializeView(model);

            var model1 = Model as KBEngine.Model;
            if (model1 != null)
            {
                string entityName 
                    = transform.Find("Name").GetComponent<TextMesh>().text
                    = (string)model1.getDefinedProperty("name");
                transform.Find("Name").GetComponent<TextMesh>().text = "";

                if (entityName == "GateWayTrigger")
                {
                    //transform.Find("Name").GetComponent<TextMesh>().text
                    //    = (string)model1.getDefinedProperty("name");

                    var trigger =
                        Instantiate(
                            AssetTool.LoadAsset_Database_Or_Bundle(
                                AssetTool.Assets__Resources_Ours__Prefabs_ + "Trigger/Skill/GateWayTrigger/GateWayTrigger.prefab",
                                "Prefabs",
                                "trigger_bundle",
                                "GateWayTrigger")) as GameObject;
                    if (trigger != null)
                    {
                        trigger.transform.SetParent(transform);
                        trigger.transform.localPosition = new Vector3(0, 0, 0);
                    }
                }
                else
                {
                    var trigger = 
                        Instantiate(
                            AssetTool.LoadAsset_Database_Or_Bundle(
                                AssetTool.Assets__Resources_Ours__Prefabs_ + "Trigger/Skill/" + entityName + "/"+ entityName + "_Trigger.prefab",
                                "Prefabs",
                                "trigger_bundle",
                                entityName + "_Trigger")) as GameObject;
                    if (trigger != null)
                    {
                        trigger.SetActive(false);
                        trigger.transform.SetParent(transform);
                        trigger.transform.localPosition = new Vector3(0, 0, 0);
                        trigger.transform.eulerAngles = Vector3.zero;
                        _myTriggerObject = trigger;
                        Invoke("InvokeMethod", 0.1f);
                    }
                    else
                    {
                        Debug.LogError(entityName + "_Trigger.prefab is null");
                    }
                }
            }

            HandleTriggerSizeUpdate(0);
            HandleParentSkillUpdate(0);

            model.SubscribePropertyUpdate(TriggerPeopertys.ParentSkill, HandleParentSkillUpdate);
            model.SubscribePropertyUpdate(TriggerPeopertys.TriggerSize, HandleTriggerSizeUpdate);
        }

        private void InvokeMethod()
        {
            _myTriggerObject.SetActive(true);
        }

        public void HandleParentSkillUpdate(object val)
        {
        }

        public void HandleTriggerSizeUpdate(object val)
        {
            _triggerSize = (int)((KBEngine.Model)Model).getDefinedProperty(TriggerPeopertys.TriggerSize);
        }
    }

}