using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;
using LitJson;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using UnityEditor.SceneManagement;

public class ExportSceneObjectInfoEditor : Editor 
{	
	//将指定游戏场景导出为XML格式
	[MenuItem ("Assets/ExportXML")]
	static void ExportXML () 
	{
        Object[] selectedAssetList = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        string path = EditorUtility.SaveFilePanel("Save Resource", "Assets/StreamingAssets/SceneDescription", selectedAssetList[0].name, "xml");
        if (path.Length != 0)
        {
            //遍历所有的游戏对象
            foreach (Object selectObject in selectedAssetList)
            {
                // 场景名称
                string sceneName = selectObject.name;
                // 场景路径
                string scenePath = AssetDatabase.GetAssetPath(selectObject);
                // 场景文件
                //string xmlPath = path; //Application.dataPath + "/AssetBundles/Prefab/Scenes/" + sceneName + ".xml";
                // 如果存在场景文件，删除
                if (File.Exists(path)) File.Delete(path);
                // 打开这个关卡
                //EditorApplication.OpenScene(scenePath);
                EditorSceneManager.OpenScene(scenePath);
                XmlDocument xmlDocument = new XmlDocument();
                // 创建XML属性
                XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDocument.AppendChild(xmlDeclaration);
                // 创建XML根标志
                XmlElement rootXmlElement = xmlDocument.CreateElement("root");
                // 创建场景标志
                XmlElement sceneXmlElement = xmlDocument.CreateElement("scene");
                sceneXmlElement.SetAttribute("sceneName", sceneName);
                foreach (GameObject sceneObject in Object.FindObjectsOfType(typeof(GameObject)))
                {
                    // 如果对象是激活状态
                    if (sceneObject.transform.parent == null && sceneObject.activeSelf)
                    {
                        // 判断是否是预设
                        if (PrefabUtility.GetPrefabType(sceneObject) == PrefabType.PrefabInstance)
                        {
                            // 获取引用预设对象
                            //Object prefabObject = EditorUtility.GetPrefabParent(sceneObject);
                            Object prefabObject = PrefabUtility.GetPrefabParent(sceneObject);
                            if (prefabObject != null)
                            {
                                XmlElement gameObjectXmlElement = xmlDocument.CreateElement("gameObject");
                                gameObjectXmlElement.SetAttribute("objectName", sceneObject.name);
                                gameObjectXmlElement.SetAttribute("objectAsset", prefabObject.name);
                                XmlElement transformXmlElement = xmlDocument.CreateElement("transform");
                                // 位置信息
                                XmlElement positionXmlElement = xmlDocument.CreateElement("position");
                                positionXmlElement.SetAttribute("x", sceneObject.transform.position.x.ToString());
                                positionXmlElement.SetAttribute("y", sceneObject.transform.position.y.ToString());
                                positionXmlElement.SetAttribute("z", sceneObject.transform.position.z.ToString());
                                // 旋转信息
                                XmlElement rotationXmlElement = xmlDocument.CreateElement("rotation");
                                rotationXmlElement.SetAttribute("x", sceneObject.transform.rotation.eulerAngles.x.ToString());
                                rotationXmlElement.SetAttribute("y", sceneObject.transform.rotation.eulerAngles.y.ToString());
                                rotationXmlElement.SetAttribute("z", sceneObject.transform.rotation.eulerAngles.z.ToString());
                                // 缩放信息
                                XmlElement scaleXmlElement = xmlDocument.CreateElement("scale");
                                scaleXmlElement.SetAttribute("x", sceneObject.transform.localScale.x.ToString());
                                scaleXmlElement.SetAttribute("y", sceneObject.transform.localScale.y.ToString());
                                scaleXmlElement.SetAttribute("z", sceneObject.transform.localScale.z.ToString());
                                transformXmlElement.AppendChild(positionXmlElement);
                                transformXmlElement.AppendChild(rotationXmlElement);
                                transformXmlElement.AppendChild(scaleXmlElement);
                                gameObjectXmlElement.AppendChild(transformXmlElement);
                                sceneXmlElement.AppendChild(gameObjectXmlElement);
                            }
                        }
                    }
                }
                rootXmlElement.AppendChild(sceneXmlElement);
                xmlDocument.AppendChild(rootXmlElement);
                // 保存场景数据
                xmlDocument.Save(path);
                // 刷新Project视图
                AssetDatabase.Refresh();
            }
        }
	}

    [MenuItem("Assets/ExportXML", true)]
    private static bool CheckObjectType()
    {
        Object selectedObject = Selection.activeObject;
        if (selectedObject != null &&
            selectedObject.GetType() == typeof(SceneAsset))
        {
            return true;
        }
        return false;
    }
	
	//将所有游戏场景导出为JSON格式
	[MenuItem ("GameObject/ExportJSON")]
	static void ExportJSON () 
	{
        string filepath = Application.dataPath + @"/StreamingAssets/SceneDescription/SceneDescriptionJSON.txt";
      	FileInfo t = new FileInfo(filepath);
		if(!File.Exists (filepath))
		{
			File.Delete(filepath);
		}
        StreamWriter sw = t.CreateText();
       
		
		StringBuilder sb = new StringBuilder ();
        JsonWriter writer = new JsonWriter (sb);
		writer.WriteObjectStart ();
		writer.WritePropertyName ("GameObjects");
		writer.WriteArrayStart ();

		foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
        {
            if (S.enabled)
            {
                string name = S.path;
                //EditorApplication.OpenScene(name);
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(name);
				writer.WriteObjectStart();
				writer.WritePropertyName("scenes");
 				writer.WriteArrayStart ();
				writer.WriteObjectStart();
				writer.WritePropertyName("name");
				writer.Write(name);
				writer.WritePropertyName("gameObject");
				writer.WriteArrayStart ();
				
				foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
				{
    				if (obj.transform.parent == null)
    				{
						writer.WriteObjectStart();
						writer.WritePropertyName("name");
						writer.Write(obj.name);
						
						writer.WritePropertyName("position");
				        writer.WriteArrayStart ();
						writer.WriteObjectStart();
						writer.WritePropertyName("x");
						writer.Write(obj.transform.position.x.ToString("F5"));
						writer.WritePropertyName("y");
						writer.Write(obj.transform.position.y.ToString("F5"));
						writer.WritePropertyName("z");
						writer.Write(obj.transform.position.z.ToString("F5"));
						writer.WriteObjectEnd();
						writer.WriteArrayEnd();
						
						writer.WritePropertyName("rotation");
				        writer.WriteArrayStart ();
						writer.WriteObjectStart();
						writer.WritePropertyName("x");
						writer.Write(obj.transform.rotation.eulerAngles.x.ToString("F5"));
						writer.WritePropertyName("y");
						writer.Write(obj.transform.rotation.eulerAngles.y.ToString("F5"));
						writer.WritePropertyName("z");
						writer.Write(obj.transform.rotation.eulerAngles.z.ToString("F5"));
						writer.WriteObjectEnd();
						writer.WriteArrayEnd();
						
						writer.WritePropertyName("scale");
				        writer.WriteArrayStart ();
						writer.WriteObjectStart();
						writer.WritePropertyName("x");
						writer.Write(obj.transform.localScale.x.ToString("F5"));
						writer.WritePropertyName("y");
						writer.Write(obj.transform.localScale.y.ToString("F5"));
						writer.WritePropertyName("z");
						writer.Write(obj.transform.localScale.z.ToString("F5"));
						writer.WriteObjectEnd();
						writer.WriteArrayEnd();
						
						writer.WriteObjectEnd();
					}
				}
				
				writer.WriteArrayEnd();
				writer.WriteObjectEnd();
				writer.WriteArrayEnd();
				writer.WriteObjectEnd();
			}
		}
		writer.WriteArrayEnd();
		writer.WriteObjectEnd ();

	
		sw.WriteLine(sb.ToString());
        sw.Close();
        sw.Dispose();
		AssetDatabase.Refresh();
	}
	
	
	[MenuItem ("GameObject/BINARY")]
	static void XMLJSONTOBinary ()
	{
        string filepath = Application.dataPath + @"/StreamingAssets/SceneDescription/SceneDescriptionBINARY.txt";
		if(File.Exists (filepath))
		{
			File.Delete(filepath);
		}
		FileStream  fs = new FileStream(filepath, FileMode.Create);
		BinaryWriter bw = new BinaryWriter(fs);
		foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
        {
            if (S.enabled)
            {
				string name = S.path;
                //EditorApplication.OpenScene(name);
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(name);
				foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
				{
    				if (obj.transform.parent == null)
    				{
						bw.Write(name);
						bw.Write(obj.name);
						
						short posx = (short)(obj.transform.position.x * 100);
						bw.Write(posx);
						bw.Write((short)(obj.transform.position.y * 100.0f));
						bw.Write((short)(obj.transform.position.z * 100.0f));
						bw.Write((short)(obj.transform.rotation.eulerAngles.x * 100.0f));
						bw.Write((short)(obj.transform.rotation.eulerAngles.y * 100.0f));
						bw.Write((short)(obj.transform.rotation.eulerAngles.z * 100.0f));
						bw.Write((short)(obj.transform.localScale.x * 100.0f));
						bw.Write((short)(obj.transform.localScale.y * 100.0f));
						bw.Write((short)(obj.transform.localScale.z * 100.0f));
	
					}
				}
	
			}
		}
	
		bw.Flush();
		bw.Close();
		fs.Close();
	}

}
