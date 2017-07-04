/* --------------------------
 * Company: MagicFire Studio
 *   Autor: Changmin Yang
 *   类描述: 这是一个单例对象，处理kbe插件层的抛出事件，创建表现层的场景（Scene）、游戏对象（GameObject）等
 * -------------------------- */

using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using DG.Tweening;
using MagicFire.Common;
using MagicFire.Mmorpg.UI;

namespace MagicFire.Mmorpg
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using KBEngine;
    using System;
    using System.Reflection;
    using System.Collections;
    using System.Collections.Generic;
    using MagicFire;
    using Mmorpg.UI;
    using System.Text;
    using System.Linq;

    public class WorldMediator : MagicFire.BaseSingleton<WorldMediator>
    {
        private AvatarView _mainAvatarView;     //主玩家的View对象
        private Entity _currentSpaceModel;  //当前Space的实体

        //主玩家的View对象
        public AvatarView MainAvatarView
        {
            get
            {
                return _mainAvatarView;
            }
            set
            {
                _mainAvatarView = value;
            }
        }

        //当前Space的id
        public uint CurrentSpaceId
        {
            get;
            private set;
        }    

        //当前Space的实体
        public Model CurrentSpaceModel
        {
            get { return _currentSpaceModel as Model; }
            set
            {
                if (value == null)
                {
                    Debug.LogError("_currentSpaceModel == null!");
                }
                else
                {
                    value.getDefinedProperty("spaceName");
                }
                _currentSpaceModel = value;
            }
        }

        //场景是否加载完成，只有场景加载完才能创建角色、怪物、npc等游戏对象
        public bool IsSceneLoadComplete
        {
            get;
            private set;
        }

        private WorldMediator()
        {
        }

        public void InitializeGameWorld()
        {
            RegisterMethods();
        }

        //通过反射获取方法并且注册
        private void RegisterMethods()
        {
            KBEngine.Event.registerOut("onLoginSuccessfully", this, "onLoginSuccessfully");
            KBEngine.Event.registerOut("onMainAvatarEnterSpace", this, "onMainAvatarEnterSpace");
            KBEngine.Event.registerOut("onMainAvatarLeaveSpace", this, "onMainAvatarLeaveSpace");
            MethodInfo[] props = null;
            try
            {
                var type = typeof(WorldMediator);
                //object obj = Activator.CreateInstance(type);
                props = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
            if (props == null) return;
            foreach (var item in props)
            {
                var paramList = item.GetParameters();
                if (paramList.Length <= 0) continue;
                if (paramList[0].ParameterType.Name == "Entity")
                {
                    KBEngine.Event.registerOut(item.Name, this, item.Name);
                }
            }
        }
        // ReSharper disable once InconsistentNaming
        public void onLoginSuccessfully(
            // ReSharper disable once InconsistentNaming
            ulong rndUUID, 
            int eid, 
            Account accountEntity)
        {
            SingletonGather.UiManager.CanvasLayerFront.transform.FindChild("LoginPanel").gameObject.SetActive(false);
            SingletonGather.UiManager.TryGetOrCreatePanel("SceneLoadPanel");
        }

        // ReSharper disable once InconsistentNaming
        public string UnicodeToGB(string text)
        {
            var mc = Regex.Matches(text, "\\\\u([\\w]{4})");

            if (mc.Count <= 0) return text;

            foreach (Match m2 in mc)
            {
                var v = m2.Value;              //  \u7502
                var word = v.Substring(2);     //  7502
                var codes = new byte[2];
                var code = Convert.ToInt32(word.Substring(0, 2), 16);      //  75
                var code2 = Convert.ToInt32(word.Substring(2), 16);        //  02
                codes[0] = (byte)code2;
                codes[1] = (byte)code;
                text = text.Replace(v, Encoding.Unicode.GetString(codes));
            }
            return text;
        }

        //--------------------------------------------------------------------------------------

        // ReSharper disable once InconsistentNaming
        public void onEnterWorld(Entity entity)
        {
            if (entity.isPlayer()) return;
            if (entity.renderObj != null) return;
            CreateEntityView(entity);
        }

        // ReSharper disable once InconsistentNaming
        public void onLeaveWorld(Entity entity)
        {

        }

        // ReSharper disable once InconsistentNaming
        public void onMainAvatarLeaveSpace()
        {
            Debug.Log("onMainAvatarLeaveSpace");
            PlayerInputController.instance.gameObject.SetActive(false);
            IsSceneLoadComplete = false;
            var panel = SingletonGather.UiManager.TryGetOrCreatePanel("SceneLoadPanel");
            if (panel == null)
                Debug.LogError("panel == null");
            else
                panel.SetActive(true);
            KBEngine.Event.fireIn("OnLeaveSpaceClientInputInValid");
        }

        // ReSharper disable once InconsistentNaming
        public void onMainAvatarEnterSpace(int spaceId, string spaceName)
        {
            CurrentSpaceId = (uint)spaceId;
            IsSceneLoadComplete = false;
            SceneManagement.XmlSceneManager.LoadScene(spaceName, CreateSceneCallBackAction);
        }

        private void CreateSceneCallBackAction()
        {
            IsSceneLoadComplete = true;
            SingletonGather.UiManager.TryGetOrCreatePanel("SceneLoadPanel").SetActive(false);
            KBEngine.Avatar avatar = (KBEngine.Avatar)KBEngineApp.app.player();
            if (avatar == null)
            {
                Debug.LogError("onMainAvatarEnterSpace:avatar == null!");
                return;
            }

            var gamePanel = SingletonGather.UiManager.TryGetOrCreatePanel("GamePanel").GetComponent<GamePanel>();
            if (gamePanel != null)
                gamePanel.OnMainAvatarActive(avatar);

            var playerDialogPanel = SingletonGather.UiManager.TryGetOrCreatePanel("PlayerDialogPanel").GetComponent<PlayerDialogPanel>();//聊天窗口
            if (playerDialogPanel != null)
                playerDialogPanel.OnMainAvatarActive(avatar);

            if (PlayerInputController.instance)
                PlayerInputController.instance.gameObject.SetActive(true);
            SingletonGather.UiManager.Canvas.ToString();
            PlayerTarget.Instance.ToString();
            ObjectSelectManager.Instance.ToString();
            ClientApp.Instance.DelayExecuteRepeating(DetectRenderObj, 0, 4);
        }

        private void DetectRenderObj()
        {
            foreach (var entity in KBEngineApp.app.entities.Values)
            {
                if (entity.renderObj == null)
                {
                    if (entity.className != "Space")
                        CreateEntityView(entity);
                }
            }
        }

        private void CreateEntityView(Entity entity)
        {
            if (IsSceneLoadComplete == false)
                return;
            SingletonGather.FactorysFactory.CreateFactory<EntityViewFactory>().CreateProduct<EntityObjectView>(entity);
            SingletonGather.FactorysFactory.CreateFactory<EntityViewFactory>().CreateProduct<EntityPanelView>(entity);
        }
        // ReSharper disable once InconsistentNaming
        public void updatePosition(Entity entity)
        {
            if (entity.renderObj == null)
                return;
            try
            {
                ((GameObject) entity.renderObj).transform.DOMove(entity.position, 0.1f);
                ((GameObject)entity.renderObj).transform.eulerAngles = new Vector3(entity.direction.x, entity.direction.z, entity.direction.y);
            }
            catch (Exception)
            {
                //Debug.Log("## " + entity.getDefinedProperty("entityName"));
            }
        }
        // ReSharper disable once InconsistentNaming
        public void set_position(Entity entity)
        {
            if (entity.renderObj == null)
                return;
            var entityModel = entity as Model;
            if (entityModel != null)
            {
                Action<object> action;
                entityModel.PropertyUpdateHandlers.TryGetValue(EntityPropertys.Position, out action);
                if (action != null) action.Invoke(entity.position);
            }
            ((GameObject)entity.renderObj).transform.DOMove(entity.position, 0.1f);
        }
        // ReSharper disable once InconsistentNaming
        public void set_direction(Entity entity)
        {
            if (entity.renderObj == null)
                return;
            var entityModel = entity as Model;
            if (entityModel != null)
            {
                Action<object> action;
                entityModel.PropertyUpdateHandlers.TryGetValue(EntityPropertys.Direction, out action);
                if (action != null) action.Invoke(new Vector3(0, entity.direction.y, 0));
            }
            ((GameObject)entity.renderObj).transform.eulerAngles = new Vector3(0, entity.direction.y, 0);
        }

        public void OnUpdatePropertys(Entity entity, string propertyName, object val)
        {
            if (entity.renderObj == null)
            {
                return;
            }
            var entityModel = entity as Model;
            if (entityModel != null)
            {
                Action<object> action;
                entityModel.PropertyUpdateHandlers.TryGetValue(propertyName, out action);
                if (action != null) action.Invoke(val);
            }
        }
        // ReSharper disable once InconsistentNaming
        public void onRemoteMethodCall_(Entity entity, string methodName, object[] args)
        {
            if (entity.renderObj == null)
            {
                return;
            }
            var entityModel = entity as Model;
            if (entityModel != null)
            {
                Action<object[]> action;
                entityModel.MethodCallHandlers.TryGetValue(methodName, out action);
                if (action != null)
                {
                    action(args);
                }
            }
        }

        public void DoDialog(Entity entity, string npcName, string dialog)
        {
            if (entity.renderObj == null)
            {
                return;
            }
            ((GameObject)entity.renderObj).GetComponent<PlayerInputController>().DoDialog(npcName, dialog);
        }

        public void BuyResult(Entity entity, bool result)
        {
            if (entity.renderObj == null)
            {
                return;
            }
            ((GameObject)entity.renderObj).GetComponent<PlayerInputController>().BuyResult(result);
        }

        public void DoStore(Entity entity)
        {
            if (entity.renderObj == null)
            {
            }
            //((UnityEngine.GameObject)entity.renderObj).GetComponent<Player>().DoStore();
        }

    } 
}