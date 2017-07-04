/* --------------------------
 * Company: MagicFire Studio
 *   Autor: Changmin Yang
 *   类描述: 实例化Entity的View部分，包括ObjectView以及PanelView等
 * -------------------------- */

using KBEngine;
using MagicFire.Common;
using MagicFire.Common.Plugin;
using MagicFire.Mmorpg;
using MagicFire.Mmorpg.UI;

namespace MagicFire {
    using System;
    using System.Reflection;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using Object = UnityEngine.Object;

    /// <summary>
    /// 这个类用来实例化Entity的View部分，包括ObjectView以及PanelView等
    /// </summary>
    public class EntityViewFactory : IBaseFactory
    {
        private Object _entityPanelViewPrefab;
        private readonly Dictionary<Type, Dictionary<string, Object>> _products = new Dictionary<Type, Dictionary<string, Object>>();

        public TProductType CreateProduct<TProductType>(params object[] productParameters)
        {
            return (TProductType)CreateProduct(typeof(TProductType), productParameters);
        }

        public object CreateProduct(Type productType, params object[] productParameters)
        {
            var entity = productParameters[0] as Entity;
            if (entity == null)
                return null;
            if (productType == typeof(EntityObjectView))
                return CreateRenderObjectView(entity, entity.className);
            if (productType != typeof(EntityPanelView))
                return CreateEntityPanelView(entity); ;
            return CreateEntityPanelView(entity);
        }

        private EntityObjectView CreateRenderObjectView(Entity entity, string viewType)
        {
            Dictionary<string, Object> typeProducts;
            _products.TryGetValue(typeof(EntityObjectView), out typeProducts);
            if (typeProducts == null)
            {
                typeProducts = new Dictionary<string, Object>();
                _products.Add(typeof(EntityObjectView), typeProducts);
            }
            Object productPrefab;

            switch (viewType)
            {
                case "Avatar":
                    typeProducts.TryGetValue(viewType, out productPrefab);
                    break;
                case "Monster":
                    typeProducts.TryGetValue(viewType + entity.getDefinedProperty("modelName"), out productPrefab);
                    break;
                case "Npc":
                    typeProducts.TryGetValue(viewType + entity.getDefinedProperty("modelName"), out productPrefab);
                    break;
                case "Trigger":
                    typeProducts.TryGetValue(viewType, out productPrefab);
                    break;
                default:
                    productPrefab = null;
                    break;
            }

            if (productPrefab == null)
            {
                var productPrefabPath = "";
                var databasePath = "";
                var bundlePath = "Prefabs";
                var bundleName = "";
                var assetName = "";

                switch (viewType)
                {
                    case "Avatar":
                        productPrefabPath = "Player/Player";
                        bundleName = "player_bundle";
                        assetName = "Player";
                        break;
                    case "Monster":
                        productPrefabPath = "Monster/" + entity.getDefinedProperty("modelName");
                        bundleName = "monster_bundle";
                        assetName = "" + entity.getDefinedProperty("modelName");
                        break;
                    case "Npc":
                        productPrefabPath = "Npc/" + entity.getDefinedProperty("modelName");
                        bundleName = "npc_bundle";
                        assetName = "" + entity.getDefinedProperty("modelName");
                        break;
                    case "Trigger":
                        productPrefabPath = "Trigger/Trigger";
                        bundleName = "trigger_bundle";
                        assetName = "Trigger";
                        break;
                }
                databasePath = "Assets/_Resources/Ours/_Prefabs/" + productPrefabPath + ".prefab";
                productPrefab = AssetTool.LoadAsset_Database_Or_Bundle(databasePath, bundlePath, bundleName, assetName);
                if (productPrefab != null)
                {
                    switch (viewType)
                    {
                        case "Avatar":
                            typeProducts.Add(viewType, productPrefab);
                            break;
                        case "Monster":
                            typeProducts.Add(viewType + entity.getDefinedProperty("modelName"), productPrefab);
                            break;
                        case "Npc":
                            typeProducts.Add(viewType + entity.getDefinedProperty("modelName"), productPrefab);
                            break;
                        case "Trigger":
                            typeProducts.Add(viewType, productPrefab);
                            break;
                        default:
                            productPrefab = null;
                            break;
                    }
                }
                else
                {
                    Debug.LogError(entity.getDefinedProperty("entityName") + " no " + entity.getDefinedProperty("modelName") + " prefab!");
                    return null;
                }
            }
            EntityObjectView entityView = null;
            if (productPrefab != null)
            {
                var gameObject = Object.Instantiate(productPrefab, entity.position, Quaternion.identity) as GameObject;
                entity.renderObj = gameObject;
                if (gameObject != null)
                {
                    gameObject.name = entity.className + ":" + entity.getDefinedProperty("entityName");
                    entityView = gameObject.GetComponent<EntityObjectView>();
                    entityView.InitializeView(entity as KBEngine.Model);
                    if (entity.isPlayer())
                    {
                        SingletonGather.WorldMediator.MainAvatarView = entityView as AvatarView;
                        gameObject.AddComponent<PlayerInputController>();
                    }
                }
            }
            return entityView;
        }

        private EntityPanelView CreateEntityPanelView(Entity entity)
        {
            if (!_entityPanelViewPrefab)
            {
                _entityPanelViewPrefab = 
                    AssetTool.LoadAsset_Database_Or_Bundle(
                        AssetTool.Assets__Resources_Ours__UIPanel_ + "EntityPanel.prefab",
                        "Prefabs",
                        "uipanel_bundle",
                        "EntityPanel");
                if (_entityPanelViewPrefab == null)
                {
                    Debug.LogError("_entityPanelViewPrefab == null!");
                    return null;
                }
            }

            var tempVector = Camera.main.WorldToScreenPoint(entity.position);
            tempVector = new Vector3(tempVector.x, tempVector.y, 0);
            GameObject gObj;
            switch (entity.className)
            {
                case "Avatar":
                    gObj = Object.Instantiate(_entityPanelViewPrefab, tempVector, Quaternion.identity) as GameObject;
                    if (gObj != null) gObj.AddComponent<AvatarPanelView>();
                    break;
                case "Monster":
                    gObj = Object.Instantiate(_entityPanelViewPrefab, tempVector, Quaternion.identity) as GameObject;
                    if (gObj != null) gObj.AddComponent<MonsterPanelView>();
                    break;
                case "Npc":
                    gObj = Object.Instantiate(_entityPanelViewPrefab, tempVector, Quaternion.identity) as GameObject;
                    if (gObj != null) gObj.AddComponent<NpcPanelView>();
                    break;
                default:
                    return null;
            }
            if (gObj == null) return null;
            gObj.transform.SetParent(SingletonGather.UiManager.CanvasLayerBack.transform);
            var view = gObj.GetComponent<EntityPanelView>();
            view.InitializeView(entity as KBEngine.Model);
            return view;
        }
    }
}
