using UnityEditor;
 
public class ModifyModelScale : AssetPostprocessor
{
    public void OnPreprocessModel()
    {
        ModelImporter modelImporter = (ModelImporter)assetImporter;
        //这里是模型的缩放比例，默认为0.01 这里我改成了1
        //modelImporter.globalScale = 50.0f;
        //这里修改模型类型
        //modelImporter.animationType = ModelImporterAnimationType.Legacy;
    }
}