using KBEngine;
using MagicFire.Common;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using System.Collections;

    public class EntityPanelManager : MagicFire.BaseSingleton<EntityPanelManager>
    {
        private Object _entityPanelPrefab;

        public EntityPanelView CreateEntityPanelView(KBEngine.Entity entity)
        {
            var obj = entity.renderObj as GameObject;
            if (obj != null)
            {
                var tempVector = Camera.main.WorldToScreenPoint(obj.transform.position);
                tempVector = new Vector3(tempVector.x, tempVector.y, 0);
                if (_entityPanelPrefab == null)
                {
                    _entityPanelPrefab = Resources.Load("EntityPanel");
                }
                var gObj = Object.Instantiate(_entityPanelPrefab, tempVector, Quaternion.identity) as GameObject;
                if (gObj != null)
                {
                    switch (entity.className)
                    {
                        case "Avatar":
                            gObj.AddComponent<AvatarPanelView>();
                            break;
                        case "Monster":
                            gObj.AddComponent<MonsterPanelView>();
                            break;
                        case "Npc":
                            gObj.AddComponent<NpcPanelView>();
                            break;
                    }
                    gObj.transform.SetParent(SingletonGather.UiManager.Canvas.transform.Find("LayerBack"));
                    var view = gObj.GetComponent<EntityPanelView>();
                    return view;
                }
            }
            return null;
        }
    }

}