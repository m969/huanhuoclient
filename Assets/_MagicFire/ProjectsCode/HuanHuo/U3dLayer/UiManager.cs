namespace MagicFire.Common
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using System.Collections;
    using System.Collections.Generic;
    using Mmorpg.UI;
    using Common.Plugin;

    public class UiManager : MagicFire.BaseSingleton<UiManager>, IUiManager
    {
        private GameObject _canvas;
        private GameObject _eventSystem;
        private Dictionary<string, GameObject> _panels = new Dictionary<string, GameObject>();

        public Dictionary<string, GameObject> Panels
        {
            get
            {
                return _panels;
            }
            set
            {
                _panels = value;
            }
        }

        public GameObject Canvas
        {
            get
            {
                if (_canvas)
                    return _canvas;
                else
                {
                    _canvas = GameObject.Find("Canvas");
                    //_canvas = GameObject.FindObjectOfType<Canvas>().gameObject;
                    if (_canvas)
                    {
                        return _canvas;
                    }
                    else
                    {
                        _eventSystem =
                            Object.Instantiate(
                                AssetTool.LoadAsset_Database_Or_Bundle(
                                    AssetTool.Assets__Resources_Ours__UIPanel_ + "Views/" + "EventSystem.prefab",
                                    "Prefabs",
                                    "uipanel_bundle",
                                    "EventSystem"),
                                new Vector3(0, 0, 0),
                                Quaternion.identity) as GameObject;
                        return _canvas = 
                            Object.Instantiate(
                                AssetTool.LoadAsset_Database_Or_Bundle(
                                    AssetTool.Assets__Resources_Ours__UIPanel_ + "Views/" + "Canvas.prefab",
                                    "Prefabs",
                                    "uipanel_bundle",
                                    "Canvas"),
                                new Vector3(0, 0, 0),
                                Quaternion.identity) as GameObject;
                    }
                }
            }
            set { _canvas = value; }
        }

        public GameObject CanvasLayerFront
        {
            get
            {
                var layerFront = Canvas.transform.Find("LayerFront");
                if (layerFront)
                {
                    return layerFront.gameObject;
                }
                else
                {
                    var layerObj = instance.Canvas.CreateChildByName("LayerFront");
                    layerObj.AddComponent<RectTransform>();
                    return layerObj;
                }
            }
        }

        public GameObject CanvasLayerBack
        {
            get
            {
                var layerBack = Canvas.transform.Find("LayerBack");
                if (layerBack)
                {
                    return layerBack.gameObject;
                }
                else
                {
                    var layerObj = instance.Canvas.CreateChildByName("LayerBack");
                    layerObj.AddComponent<RectTransform>();
                    return layerObj;
                }
            }
        }

        public CanvasLayer CanvasLayers = new CanvasLayer();


        public class CanvasLayer
        {
            public GameObject this[int index]
            {
                get
                {
                    var layer = instance.Canvas.transform.Find("Layer" + index);
                    if (layer)
                    {
                        return layer.gameObject;
                    }
                    else
                    {
                        var layerObj = instance.Canvas.CreateChildByName("Layer" + index);
                        layerObj.AddComponent<RectTransform>();
                        return layerObj;
                    }
                }
            }
        }


        private UiManager()
        {
            mInstance = this;
        }

        public GameObject TryGetOrCreatePanel(string panelName)
        {
            GameObject tempPanel;

            if (!_panels.ContainsKey(panelName))
            {
                tempPanel = 
                    Object.Instantiate(
                        AssetTool.LoadAsset_Database_Or_Bundle(
                            AssetTool.Assets__Resources_Ours__UIPanel_ + "Views/" + panelName + ".prefab",
                            "Prefabs",
                            "uipanel_bundle",
                            panelName)) as GameObject;
                _panels.Add(panelName, tempPanel);
            }
            else
            {
                _panels.TryGetValue(panelName, out tempPanel);
                if (tempPanel == null)
                {
                    tempPanel = 
                        Object.Instantiate(
                            AssetTool.LoadAsset_Database_Or_Bundle(
                                AssetTool.Assets__Resources_Ours__UIPanel_ + "Views/" + panelName + ".prefab",
                                "Prefabs",
                                "uipanel_bundle",
                                panelName)) as GameObject;
                    _panels.Remove(panelName);
                    _panels.Add(panelName, tempPanel);
                }
            }
            if (tempPanel != null)
            {
                tempPanel.GetComponent<RectTransform>().SetAsLastSibling();
            }
            return tempPanel;
        }
    } 
}