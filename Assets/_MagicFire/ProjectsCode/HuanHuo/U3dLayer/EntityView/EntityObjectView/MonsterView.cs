using MagicFire.Common.Plugin;

namespace MagicFire.Mmorpg
{
    using UnityEngine;
    using System.Collections;
    using System;
    using System.Linq;
    using Object = UnityEngine.Object;

    public class MonsterView : CombatEntityObjectView
    {
        public Animator animator;
        public Animation animation;

        public void Start()
        {
            animator = GetComponent<Animator>();
            animation = GetComponent<Animation>();
        }

        public override void InitializeView(Common.IModel model)
        {
            base.InitializeView(model);
            Model.SubscribePropertyUpdate(CombatPropertys.Hp, Hp);//订阅
            Model.SubscribeMethodCall("StartMove", StartMove);
            Model.SubscribeMethodCall("StopMove",StopMove);
            Model.SubscribeMethodCall("Attack_01", Attack_01);
            Model.SubscribeMethodCall("Attack_02", Attack_02);
        }

        public override void OnDie(object[] args)
        {
            base.OnDie(args);
        }

        public void Hp(object old)
        {

        }

        public void StartMove(object[] var)
        {
            if (animation != null)
            {
                if (!animation.IsPlaying("Walk"))
                {
                    animation.Play("Walk");
                }
            }
            if (animator != null)
            {
                var collection = animator.GetCurrentAnimatorClipInfo(0);

                foreach (var item in collection)
                {
                    if (item.clip.name == "Walk")
                    {
                        animator.Play("Walk");
                    }
                }
            }

        }

        public void StopMove(object[] var)
        {
            if (animation != null)
            {
                if (!animation.IsPlaying("Idle_01"))
                {
                    animation.Play("Idle_01");
                }
            }
            if (animator != null)
            {
                var collection = animator.GetCurrentAnimatorClipInfo(0);

                foreach (var item in collection)
                {
                    if (item.clip.name == "Idle_01")
                    {
                        animator.Play("Idle_01");
                    }
                }
            }
        }
        public void Attack_01(object[] var)
        {
            if (animation != null)
            {
                if (!animation.IsPlaying("Attack_01"))
                {
                    animation.Play("Attack_01");
                }
            }
            if (animator != null)
            {
                var collection = animator.GetCurrentAnimatorClipInfo(0);

                foreach (var item in collection)
                {
                    if (item.clip.name == "Attack_01")
                    {
                        animator.Play("Attack_01");
                    }
                }
            }
        }
        public void Attack_02(object[] var)
        {
            if (animation != null)
            {
                if (!animation.IsPlaying("Attack_02"))
                {
                    animation.Play("Attack_02");
                }
            }
            if (animator != null)
            {
                var collection = animator.GetCurrentAnimatorClipInfo(0);

                foreach (var item in collection)
                {
                    if (item.clip.name == "Attack_02")
                    {
                        animator.Play("Attack_02");
                    }
                }
            }
        }
        public void Die(object var)
        {
            if (animation != null)
            {
                if (!animation.IsPlaying("Die"))
                {
                    animation.Play("Die");
                }
            }
            if (animator != null)
            {
                var collection = animator.GetCurrentAnimatorClipInfo(0);

                foreach (var item in collection)
                {
                    if (item.clip.name == "Die")
                    {
                        animator.Play("Die");
                    }
                }
            }
        }
    }
}