namespace MagicFire.Mmorpg
{
    using UnityEngine;
    using System.Collections;
    using UnityEngine.UI;
    using KBEngine;
    using System;
    using Common;

    public class NpcView : EntityObjectView
    {
        private GameObject _player;
        public int id;

        // Use this for initialization
        private void Start()
        {
            gameObject.tag = "Npc";
        }

        // Update is called once per frame
        private void Update()
        {
            if (_player == null)
            {
                if (KBEngine.KBEngineApp.app != null)
                {
                    _player = KBEngine.KBEngineApp.app.player().renderObj as GameObject;
                }
            }
        }

        public override void InitializeView(IModel model)
        {
            base.InitializeView(model);
            id = (int)((KBEngine.Model)model).getDefinedProperty("npcID");
        }
    } 
}