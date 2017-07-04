

namespace MagicFire.Mmorpg
{
    using UnityEngine;
    using System;
    using System.Collections;
    using Mmorpg.UI;
    using KBEngine;
    using MagicFire.Common;
    using MagicFire.Mmorpg.Skill;

    public class AvatarView : CombatEntityObjectView
    {
        private Animator _animator;
        private Animation _animation;
        private int _goldCount;
        private bool _goldCountHasAssign;
        private AvatarState _avatarState;
        private StandState _standState;
        private RunState _runState;
        private DeadState _deadState;

        public SkillManager SkillManager { get; set; }

        public CharacterController CharacterController { get; set; }

        public int Sp { get; set; }

        public int SpMax { get; set; }

        public Animator Animator
        {
            get
            {
                return _animator;
            }
        }

        public Animation Animation
        {
            get
            {
                return _animation;
            }
        }

        public int GoldCount
        {
            get
            {
                return _goldCount;
            }
            set
            {
                if (_goldCountHasAssign == false)
                {
                    _goldCountHasAssign = true;
                }
                else
                {

                }
                if (((KBEngine.Model)Model).isPlayer())
                {
                    var hintObject = SingletonGather.UiManager.TryGetOrCreatePanel("GoldCountHint");
                    if (hintObject != null)
                    {
                        hintObject.GetComponent<GoldCountHint>().ShowHint(value - _goldCount);
                    }
                }
                _goldCount = value;
                var bagPanelObject = SingletonGather.UiManager.TryGetOrCreatePanel("BagPanel");
                if (bagPanelObject != null)
                {
                    var bagPanel = bagPanelObject.GetComponent<BagPanel>();
                    bagPanel.GoldCountText = _goldCount.ToString();
                    bagPanel.gameObject.SetActive(false);
                }
            }
        }

        // Use this for initialization
        private void Start()
        {
            CharacterController = GetComponent<CharacterController>();
            SkillManager = new SkillManager { Owner = this };
            SkillManager.Init();

            //_animator = transform.Find("Player").GetComponent<Animator>();
            _animation = transform.GetChild(0).GetComponent<Animation>();

            _standState = new StandState(this);
            _runState = new RunState(this);
            _deadState = new DeadState(this);
            _avatarState = _standState;
        }

        // Update is called once per frame
        private void Update()
        {
            SkillManager.Update();
            _avatarState.Run();
        }

        public override void InitializeView(IModel model)
        {
            base.InitializeView(model);
            Model.SubscribeMethodCall("DoMove", DoMove);
            Model.SubscribeMethodCall("OnStopMove", OnStopMove);
            //郑晓飞---发送聊天信息
            Model.SubscribeMethodCall("onReciveChatMessage", onReciveChatMessage);
        }

        public override void OnModelDestrooy(object[] objects)
        {
            Model.DesubscribeMethodCall("DoMove", DoMove);
            Model.DesubscribeMethodCall("OnStopMove", OnStopMove);
            base.OnModelDestrooy(objects);
        }

        public override void OnDie(object[] args)
        {
            _avatarState = _deadState;
            PlayerInputController.instance.enabled = false;
            SingletonGather.UiManager.TryGetOrCreatePanel("DeadPanel").SetActive(true);
            transform.GetChild(0).localEulerAngles = new Vector3(0, 1, 90);
            base.OnDie(args);
        }

        public override void OnRespawn(object[] args)
        {
            gameObject.transform.position = (Vector3)args[0];
            PlayerInputController.instance.enabled = true;
            SingletonGather.UiManager.TryGetOrCreatePanel("DeadPanel").SetActive(false);
            transform.GetChild(0).localEulerAngles = new Vector3(0, 1, 0);
        }

        public void DoMove(object[] args)
        {
            _avatarState = _runState;
        }

        public void OnStopMove(object[] args)
        {
            _avatarState = _standState;
        }

        private class AvatarState
        {
            protected readonly AvatarView AvatarView;

            protected AvatarState(AvatarView avatarView)
            {
                AvatarView = avatarView;
            }

            public virtual void Run()
            {

            }

            public virtual void FixedRun()
            {

            }
        }
        private class StandState : AvatarState
        {
            public StandState(AvatarView avatarView)
                : base(avatarView)
            {

            }

            public override void Run()
            {
                base.Run();
                if (!AvatarView._animation)
                {
                    return;
                }
                if (!AvatarView._animation.IsPlaying("idle"))
                {
                    AvatarView._animation.Play("idle");
                }
            }
        }
        private class RunState : AvatarState
        {
            public RunState(AvatarView avatarView)
                : base(avatarView)
            {

            }

            public override void Run()
            {
                base.Run();
                if (!AvatarView._animation.IsPlaying("Run"))
                {
                    AvatarView._animation.Play("Run");
                }
            }
        }
        private class DeadState : AvatarState
        {
            public DeadState(AvatarView avatarView)
                : base(avatarView)
            {

            }
        }

        //郑晓飞-----聊天框
        public void onReciveChatMessage(object[] obj)
        {
            SingletonGather.UiManager.TryGetOrCreatePanel("PlayerDialogPanel").GetComponent<PlayerDialogPanel>().DialogContent(obj);
        }
    }
}