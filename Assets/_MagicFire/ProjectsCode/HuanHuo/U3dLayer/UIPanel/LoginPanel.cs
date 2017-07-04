using MagicFire.Common;
using UnityEngine.UI;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using KBEngine;
    using System;
    using System.Collections;
    using MagicFire;

    public class LoginPanel : MagicFire.MonoSingleton<LoginPanel>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        [SerializeField]
        private Dropdown _dropdown;

        // Use this for initialization
        private void Start()
        {
            SingletonGather.WorldMediator.InitializeGameWorld();
        }

        public void Login()
        {
            switch (_dropdown.value)
            {
                case 0:
                    ClientApp.Instance.ip = "127.0.0.1";
                    break;
                case 1:
                    KBEngineApp.app.getInitArgs().ip = "47.94.18.88";
                    Debug.Log("ip 47.94.18.88");
                    break;
                case 2:
                    ClientApp.Instance.ip = "127.0.0.1";
                    break;
            }
            byte[] datas;
            datas = new byte[1];
            KBEngine.Event.fireIn("login", new object[] { Username, Password, datas });
        }

        public void QuiklyLogin01()
        {
            
            switch (_dropdown.value)
            {
                case 0:
                    ClientApp.Instance.ip = "127.0.0.1";
                    break;
                case 1:
                    KBEngineApp.app.getInitArgs().ip = "47.94.18.88";
                    Debug.Log("ip 47.94.18.88");
                    break;
                case 2:
                    ClientApp.Instance.ip = "127.0.0.1";
                    break;
            }
            byte[] datas;
            datas = new byte[1];
            KBEngine.Event.fireIn("login", new object[] { "test01", "test01", datas });
        }

        public void QuiklyLogin02()
        {
            switch (_dropdown.value)
            {
                case 0:
                    ClientApp.Instance.ip = "127.0.0.1";
                    break;
                case 1:
                    KBEngineApp.app.getInitArgs().ip = "47.94.18.88";
                    Debug.Log("ip 47.94.18.88");
                    break;
                case 2:
                    ClientApp.Instance.ip = "127.0.0.1";
                    break;
            }
            byte[] datas;
            datas = new byte[1];
            KBEngine.Event.fireIn("login", new object[] { "test02", "test02", datas });
        }

        public void QuiklyLogin03()
        {
            switch (_dropdown.value)
            {
                case 0:
                    ClientApp.Instance.ip = "127.0.0.1";
                    break;
                case 1:
                    KBEngineApp.app.getInitArgs().ip = "47.94.18.88";
                    Debug.Log("ip 47.94.18.88");
                    break;
                case 2:
                    ClientApp.Instance.ip = "127.0.0.1";
                    break;
            }
            byte[] datas;
            datas = new byte[1];
            KBEngine.Event.fireIn("login", new object[] { "test03", "test03", datas });
        }
    }
}