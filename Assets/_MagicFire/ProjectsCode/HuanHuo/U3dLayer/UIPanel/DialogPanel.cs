using MagicFire.Common;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;

    public class DialogPanel : Panel
    {
        [SerializeField]
        private Text _textBox;

        protected override void Start()
        {
            base.Start();
            transform.SetParent(UiManager.instance.CanvasLayers[1].transform);
            transform.localPosition = new Vector3(0, 0, 0);
        }

        protected override void Update()
        {
            base.Update();
        }

        public void OnClickConfirm()
        {
            gameObject.SetActive(false);
            //KBEngine.Event.fireIn("DialogContinue");
        }

        public void OnClickRefuse()
        {
            gameObject.SetActive(false);
        }

        public void ShowDialog(string dialog)
        {
            if (_textBox == null)
            {
                return;
            }
            _textBox.text = dialog;
        }
    }

}