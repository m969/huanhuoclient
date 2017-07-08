using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MagicFire.Common;
using MagicFire.Common.Plugin;
using MagicFire.Mmorpg;
using MagicFire.Mmorpg.Skill;
using MagicFire.Mmorpg.UI;
using MagicFire.SceneManagement;
using Model = KBEngine.Model;
using Object = UnityEngine.Object;

public class PlayerInputController : MonoBehaviour
{
    public AvatarView AvatarView { get; private set; }

    private Object _clickPointAuxiliaryPrefab;
    private static GameObject _clickPointObject;
    private static CharacterController _characterController;
    private static Vector3 _moveVector;
    private static PlayerInputController _instance;
    private PlayerState _currentState;
    private PlayerState _playerState;
    private DeadState _deadState;

    public static PlayerInputController instance
    {
        get
        {
            return _instance;
        }
    }

    private void Start()
    {
        _instance = this;
        AvatarView = GetComponent<AvatarView>();
        if (Application.platform == RuntimePlatform.Android)
            _playerState = new AndroidPlayerState(this);
        else
        {
            if (XmlSceneManager.Instance.ControlMode == XmlSceneManager.ControlModeEnum.PcControl)
                _playerState = new PcPlayerState(this);
            else
                _playerState = new AndroidPlayerState(this);
        }
        _deadState = new DeadState(this);
        _currentState = _playerState;
        _characterController = GetComponent<CharacterController>();

        _clickPointAuxiliaryPrefab = 
            AssetTool.LoadAsset_Database_Or_Bundle(
                AssetTool.Assets__Resources_Ours__Prefabs_ + "AuxiliaryPrefabs/ClickPointAuxiliary.prefab",
                "Prefabs",
                "auxiliaryprefabs_bundle",
                "ClickPointAuxiliary");

        _clickPointObject = Instantiate(_clickPointAuxiliaryPrefab) as GameObject;
    }

    private void Update()
    {
        _currentState.Run();
    }

    private void FixedUpdate()
    {
        _currentState.FixedRun();
        if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        }
        if (transform.eulerAngles.x != 0 || transform.eulerAngles.z != 0)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        if (KBEngine.KBEngineApp.app == null)
        {
            return;
        }
        KBEngine.Event.fireIn("updatePlayer", transform.position.x, transform.position.y, transform.position.z, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public void StartMove()
    {
        KBEngine.Event.fireIn("RequestMove", new object[] { transform.forward * 2 });
    }

    public void MoveMainAvatar(Vector2 vec)
    {
        var androidPlayerState = _playerState as AndroidPlayerState;
        if (androidPlayerState != null) androidPlayerState.MoveMainAvatar(vec);
    }

    public void EndMove()
    {
        _moveVector = Vector3.zero;
        KBEngine.Event.fireIn("StopMove");
    }

    public void SkillQReady()
    {
        int skill_id = 1;
        AvatarView.SkillManager.SkillReady(skill_id);
    }

    public void OnSkillQReadying(Vector2 vec)
    {
        int skill_id = 1;
        var skill = AvatarView.SkillManager.GetSkillRef(skill_id) as SkillQ;
        if (skill != null) skill.SkillDirection = vec;
    }

    public void DoSkillQ()
    {
        int skill_id = 1;
        var skill = AvatarView.SkillManager.GetSkillRef(skill_id) as SkillQ;
        AvatarView.SkillManager.DoSkill(skill_id);
    }

    public void SkillWReady()
    {
        int skill_id = 2;
        AvatarView.SkillManager.SkillReady(skill_id);
    }

    public void OnSkillWReadying(Vector2 vec)
    {
        int skill_id = 2;
        var skill = AvatarView.SkillManager.GetSkillRef(skill_id) as SkillW;
        if (skill != null) skill.SkillTrajectoryPosition = vec;
    }

    public void DoSkillW()
    {
        int skill_id = 2;
        var skill = AvatarView.SkillManager.GetSkillRef(skill_id) as SkillW;
        AvatarView.SkillManager.DoSkill(skill_id);
    }

    public void DoSkillE()
    {
        int skill_id = 3;
        var skill = AvatarView.SkillManager.GetSkillRef(skill_id) as SkillE;
        AvatarView.SkillManager.DoSkill(skill_id);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TargetPoint")
        {
            _moveVector = new Vector3(0, 0, 0);
            KBEngine.Event.fireIn("StopMove");
        }
    }

    private void OnDestroy()
    {
        Debug.Log("PlayerInputController:OnDestroy");
        _instance = null;
    }

    public void OnDie()
    {
        _currentState = _deadState;
    }

    public List<object> GetBagGoodsList()
    {
        return ((KBEngine.Avatar)AvatarView.Model).AvatarBag;
    }

    public void DoDialog(string npcName, string dialog)
    {
        var dialogPanel = UiManager.instance.TryGetOrCreatePanel("DialogPanel");
        if (dialogPanel == null)
        {
            return;
        }
        if (!dialogPanel.activeInHierarchy)
        {
            dialogPanel.SetActive(true);
        }
        dialogPanel.GetComponent<DialogPanel>().ShowDialog(dialog);
    }

    public void BuyResult(bool result)
    {
        var messageBox = SingletonGather.UiManager.TryGetOrCreatePanel("MessageBox");

        if (messageBox != null)
        {
            messageBox.transform.SetParent(UiManager.instance.Canvas.transform);
            messageBox.transform.localPosition = new Vector3(0, 0, 0);
            messageBox.transform.Find("MessageText").GetComponent<Text>().text = result == true ? "购买成功" : "购买失败";
        }
    }

    public void DoStore(NpcView npc)
    {
        var storePanel = UiManager.instance.TryGetOrCreatePanel("TheStorePanel_");
        if (storePanel == null)
        {
            return;
        }
        storePanel.GetComponent<StorePanel>().CurrentNpc = npc;
        if (!storePanel.activeInHierarchy)
        {
            storePanel.SetActive(true);
        }
    }

    private class PlayerState
    {
        protected float _speed = 0.2f;

        public virtual void Run()
        {
            
        }

        public virtual void FixedRun()
        {
            
        }
    }

    private class PcPlayerState : PlayerState
    {
        private readonly PlayerInputController _playerInputController;

        public PcPlayerState(PlayerInputController playerInputController)
        {
            _playerInputController = playerInputController;
        }

        public override void Run()
        {
            if (!_characterController.isGrounded)
            {
                if (Math.Abs(_moveVector.y - (-2)) > 0)
                {
                    _moveVector = new Vector3(_moveVector.x, -2, _moveVector.z);
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                //取消技能预备
                _playerInputController.AvatarView.SkillManager.CancelReady();
                //右键点击地面移动
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                LayerMask layerMask = 1 << LayerMask.NameToLayer("Terrian");
                if (Physics.Raycast(ray, out hit, 100, layerMask))
                {
                    if (_clickPointObject == null)
                    {
                        _clickPointObject = Instantiate(_playerInputController._clickPointAuxiliaryPrefab, hit.point, Quaternion.identity) as GameObject;
                    }
                    else
                    {
                        if (!_clickPointObject.activeInHierarchy)
                        {
                            _clickPointObject.SetActive(true);
                        }
                        _clickPointObject.transform.position = hit.point;
                    }
                    instance.transform.LookAt(new Vector3(hit.point.x, instance.transform.position.y, hit.point.z));

                    KBEngine.Event.fireIn("RequestMove", new object[] { hit.point });
                    _playerInputController.AvatarView.Animation.Play("Run");
                    _playerInputController.AvatarView.DoMove(null);
                    _moveVector = new Vector3(0, 0, 0);
                    _playerInputController.transform.DOLookAt(new Vector3(hit.point.x, _playerInputController.transform.position.y, hit.point.z), 0.0f);
                    _moveVector = _playerInputController.transform.forward * _speed;                    
                }
            }
            //左键点击npc请求交互
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Npc")
                    {
                        if (Mathf.Abs(Vector3.Distance(_playerInputController.transform.position, hit.collider.transform.position)) < 5)
                        {
                            NpcView npcView = hit.collider.GetComponent<NpcView>();
                            if (npcView.EntityName == "新手引导" || npcView.EntityName == "守村人"
                                || npcView.EntityName == "上水村长" || npcView.EntityName == "工匠"
                                || npcView.EntityName == "店小二" || npcView.EntityName == "刘公子"
                                || npcView.EntityName == "神秘人" || npcView.EntityName == "陈丘村长"
                                || npcView.EntityName == "姑娘" || npcView.EntityName == "钱大娘"
                                || npcView.EntityName == "兽族族长" || npcView.EntityName == "看箱人"
                                || npcView.EntityName == "宝马盗贼")
                            {
                                KBEngine.Event.fireIn("RequestDialog", new object[] { SingletonGather.WorldMediator.CurrentSpaceId, npcView.EntityName });
                            }
                            if (npcView.EntityName == "商人" || npcView.EntityName == "上水商人"
                                 || npcView.EntityName == "钻石商人"                            )
                            {
                                _playerInputController.DoStore(npcView);
                            }                           
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                int skill_id = 1;
                _playerInputController.AvatarView.SkillManager.SkillReady(skill_id);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                int skill_id = 2;
                _playerInputController.AvatarView.SkillManager.SkillReady(skill_id);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                int skill_id = 3;
                _playerInputController.AvatarView.SkillManager.SkillReady(skill_id);
            }
        }

        public override void FixedRun()
        {
            _characterController.Move(_moveVector);
        }
    }

    private class AndroidPlayerState : PlayerState
    {
        private readonly PlayerInputController _playerInputController;
        private Transform _avatarRotate;

        public AndroidPlayerState(PlayerInputController playerInputController)
        {
            _playerInputController = playerInputController;
            Instantiate(AssetTool.LoadAsset_Database_Or_Bundle(
                AssetTool.Assets__Resources_Ours__Prefabs_ + "AuxiliaryPrefabs/EasyTouchControlsCanvas.prefab",
                "Prefabs",
                "auxiliaryprefabs_bundle",
                "EasyTouchControlsCanvas"));
            Instantiate(AssetTool.LoadAsset_Database_Or_Bundle(
                AssetTool.Assets__Resources_Ours__Prefabs_ + "AuxiliaryPrefabs/InputManager.prefab",
                "Prefabs",
                "auxiliaryprefabs_bundle",
                "InputManager"));
        }

        public override void Run()
        {
            //左键点击npc请求交互
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Npc")
                    {
                        if (Mathf.Abs(Vector3.Distance(_playerInputController.transform.position, hit.collider.transform.position)) < 5)
                        {
                            NpcView npcView = hit.collider.GetComponent<NpcView>();
                            if (npcView.EntityName == "新手引导" || npcView.EntityName == "守村人"
                                || npcView.EntityName == "上水村长" || npcView.EntityName == "工匠"
                                || npcView.EntityName == "店小二" || npcView.EntityName == "刘公子"
                                || npcView.EntityName == "神秘人" || npcView.EntityName == "陈丘村长"
                                || npcView.EntityName == "姑娘" || npcView.EntityName == "钱大娘"
                                || npcView.EntityName == "兽族族长" || npcView.EntityName == "看箱人"
                                || npcView.EntityName == "宝马盗贼")
                            {
                                KBEngine.Event.fireIn("RequestDialog", new object[] { SingletonGather.WorldMediator.CurrentSpaceId, npcView.EntityName });
                            }
                            if (npcView.EntityName == "商人" || npcView.EntityName == "上水商人"
                                 || npcView.EntityName == "钻石商人")
                            {
                                _playerInputController.DoStore(npcView);
                            }
                        }
                    }
                }
            }
        }

        public override void FixedRun()
        {
            _characterController.Move(_moveVector);
        }

        public void MoveMainAvatar(Vector2 vec)
        {
            if (_avatarRotate == null)
                _avatarRotate = new GameObject("AvatarRotate").transform;
            _avatarRotate.LookAt(new Vector3(_avatarRotate.position.x + vec.x, _avatarRotate.position.y, _avatarRotate.position.z + vec.y));
            _playerInputController.transform.eulerAngles = new Vector3(0, _avatarRotate.eulerAngles.y, 0);

            _playerInputController.AvatarView.DoMove(null);
            _moveVector = _playerInputController.transform.forward * _speed;
        }
    }

    private class StandState:PcPlayerState
    {
        public StandState(PlayerInputController playerInputController):base(playerInputController)
        {
            
        }
    }

    private class RunState : PcPlayerState
    {
        public RunState(PlayerInputController playerInputController):base(playerInputController)
        {

        }
    }

    private class DeadState : PcPlayerState
    {
        public DeadState(PlayerInputController playerInputController)
            : base(playerInputController)
        {

        }

        public override void Run()
        {
            //base.Run();
        }

        public override void FixedRun()
        {
            //base.FixedRun();
        }
    }
}
//
//Vector3 a = _player.transform.eulerAngles;
//Vector3 b = hit.point;
//Vector3 c = Vector3.Cross(a, b);
//float angle = Vector3.Angle(a, b);

//// b 到 a 的夹角
//float sign = Mathf.Sign(Vector3.Dot(c.normalized, Vector3.Cross(a.normalized, b.normalized)));
//float signed_angle = angle * sign;

//Debug.Log("b -> a :" + signed_angle);

//// a 到 b 的夹角
//sign = Mathf.Sign(Vector3.Dot(c.normalized, Vector3.Cross(b.normalized, a.normalized)));
//signed_angle = angle * sign;

//Debug.Log("a -> b :" + signed_angle);
//