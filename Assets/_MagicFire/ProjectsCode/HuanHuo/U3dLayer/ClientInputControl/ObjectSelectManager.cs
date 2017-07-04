namespace MagicFire.Mmorpg
{
    using UnityEngine;
    using System.Collections;
    using MagicFire.Common.Plugin;

    public class ObjectSelectManager : MonoSingleton<ObjectSelectManager>
    {
        private GameObject _selectTipsCircle;
        private GameObject _currentSelectObject;

        private ObjectSelectManager()
        {

        }

        // Use this for initialization
        private void Start()
        {
            tag = "DontDestroy";
        }

        // Update is called once per frame
        void Update()
        {
            if (_currentSelectObject != null)
            {
                if (_selectTipsCircle == null)
                {
                    _selectTipsCircle = GameObject.Find("SelectObject (Clone)");
                    if (_selectTipsCircle == null)
                    {
                        _selectTipsCircle =
                            Instantiate(
                                AssetTool.LoadAsset_Database_Or_Bundle(
                                    AssetTool.Assets__Resources_Ours__Prefabs_ + "Npc/SelectObject.prefab",
                                    "Prefabs",
                                    "npc_bundle",
                                    "SelectObject"),
                                _currentSelectObject.transform.position,
                                Quaternion.identity) as GameObject;
                    }
                }
                else
                {
                    if (_selectTipsCircle.activeInHierarchy == false)
                    {
                        _selectTipsCircle.SetActive(true);
                    }
                    else
                    {
                        _selectTipsCircle.transform.position = new Vector3(_currentSelectObject.transform.position.x, 0.2f, _currentSelectObject.transform.position.z);
                    }
                }
            }
            else
            {
                if (_selectTipsCircle != null)
                {
                    if (_selectTipsCircle.activeInHierarchy == true)
                    {
                        _selectTipsCircle.SetActive(false);
                    }
                }
            }
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask layerMask = (1 << LayerMask.NameToLayer("Npc"));
            if (Physics.Raycast(ray, out hit, 100, layerMask) && Input.GetMouseButtonDown(0))
            {
                _currentSelectObject = hit.collider.gameObject;
            }
        }
    }

}