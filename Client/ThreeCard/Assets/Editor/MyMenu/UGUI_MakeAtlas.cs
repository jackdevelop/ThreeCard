using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;


public static class UGUI_MakeAtlas
{
	/**
	 * UGUI 中使用每个图片 
	 * 过程：
	 * 吧  /Atlas  中的所有 png 后缀的图片
	 * 打包成预设prefab，放到目标路径中：/Resources/Sprite
	 * 
	 * 原理：
	 * 先把小图打成图集，然后把所有小图关联在prefab上，拷贝在Resources文件夹下，
	 * 这样运行时也能用Resources.load了。
	 * 
	 * */
	[MenuItem ("MyMenu/UGUI_MakeAtlas")]
	static void MakeAtlas()
	{

		string spriteDir = Application.dataPath +"/Resources/Sprite";
		
		if(!Directory.Exists(spriteDir)){
			Directory.CreateDirectory(spriteDir);
		}

		DirectoryInfo rootDirInfo = new DirectoryInfo (Application.dataPath +"/UGUI_MakeAtlas");
		foreach (DirectoryInfo dirInfo in rootDirInfo.GetDirectories()) {
			//foreach (FileInfo pngFile in dirInfo.GetFiles("",SearchOption.AllDirectories)) {
			foreach (FileInfo pngFile in dirInfo.GetFiles("*.png",SearchOption.AllDirectories)) {
				string allPath = pngFile.FullName;
				string assetPath = allPath.Substring(allPath.IndexOf("Assets"));
				Sprite sprite = Resources.LoadAssetAtPath<Sprite>(assetPath);

				GameObject go = new GameObject(sprite.name);
				go.layer = LayerMask.NameToLayer("UI");

				go.  AddComponent<SpriteRenderer>().sprite = sprite;
				allPath = spriteDir+"/sprite_"+sprite.name+".prefab";
				string prefabPath = allPath.Substring(allPath.IndexOf("Assets"));
				PrefabUtility.CreatePrefab(prefabPath,go);
				GameObject.DestroyImmediate(go);
			}
		}	
	}
}