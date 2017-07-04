namespace MagicFire
{
using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
public class OpenSceneMenuItemScript{
[MenuItem("Open Scene/Jc")]
public static void Jc()
{
OpenScene("Jc");
}
[MenuItem("Open Scene/Lobby")]
public static void Lobby()
{
OpenScene("Lobby");
}
[MenuItem("Open Scene/LoginScene")]
public static void LoginScene()
{
OpenScene("LoginScene");
}
[MenuItem("Open Scene/MuLingCunSpace")]
public static void MuLingCunSpace()
{
OpenScene("MuLingCunSpace");
}
[MenuItem("Open Scene/Xsc")]
public static void Xsc()
{
OpenScene("Xsc");
}
[MenuItem("Open Scene/Ycm")]
public static void Ycm()
{
OpenScene("Ycm");
}
[MenuItem("Open Scene/YunLingZongSpace")]
public static void YunLingZongSpace()
{
OpenScene("YunLingZongSpace");
}
[MenuItem("Open Scene/Zxf")]
public static void Zxf()
{
OpenScene("Zxf");
}
static void OpenScene(string name)
{
if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo() == true)
{
   EditorSceneManager.OpenScene("Assets/_Scenes/" + name + ".unity");
}
}}
}
