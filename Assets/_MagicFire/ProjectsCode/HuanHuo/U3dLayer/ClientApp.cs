using UnityEngine;
using System;
using System.Collections;
using MagicFire.Common;

public class ClientApp : KBEMain
{
    private static ClientApp _instance;
    public static ClientApp Instance { get { return _instance; } }
    public string Message1 { get; set; }
    public string Message2 { get; set; }
    public string Message3 { get; set; }

    public override void initKBEngine()
    {
        base.initKBEngine();
        _instance = this;
        Message1 = "Message:";
        this.DelayExecuteRepeating(
            () =>
            {
                var objects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
                Message1 = "";
                foreach (var obj in objects)
                {
                    if (!obj)
                        continue;
                    //Message1 += "    " + obj.name + "\n";
                }
            },
            1,
            1);
    }

    // ReSharper disable once UnusedMember.Local
    // ReSharper disable once InconsistentNaming
    void OnGUI()
    {
        //GUI.Label(
        //    new Rect(
        //        new Vector2(0, 0),
        //        new Vector2(200, 2000)),
        //    "SceneObjects: \n" + Message1);

        //GUI.Label(
        //    new Rect(
        //        new Vector2(200, 0),
        //        new Vector2(200, 2000)),
        //    "DebugLog: \n" + Message2);

        //GUI.Label(
        //    new Rect(
        //        new Vector2(400, 0),
        //        new Vector2(200, 2000)),
        //    "ErrorLog: \n" + Message3);
    }
}
