using System;
using UnityEngine;
using UnityEditor;

using System.Collections.Generic;
using System.IO;

using XmlSpreadSheet = System.Collections.Generic.Dictionary< string, object[]>;





/**
 * 解析excel 
 * 
 * */
public class Excel : AssetPostprocessor {

	//资源路径 
	public static readonly string RES_UI_CONFIG_FOLDER = "Assets/Data/Excel/Res/MonsterConfig";
	//数据路径
	public static readonly string Data_UI_CONFIG_FOLDER = "Assets/Data/Excel/Data/MonsterConfig";




	/**
	 * 解析数据 
	 * */
	private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,string[] movedAssets, string[] movedFromPath)
	{
		if( CheckResModified(importedAssets) || CheckResModified(deletedAssets) || CheckResModified(movedAssets) )
		{
			createXML(RES_UI_CONFIG_FOLDER,"MonsterConfig");

		}
	}



	/**
	 * 创建xml 
	 * */
	private static void createXML(string url,string xmlName){
		string path = System.IO.Path.Combine(url, xmlName+".xml");
		TextReader tr = new StreamReader(path);
		string text = tr.ReadToEnd();



		if(text == null){
			Debug.LogError("Player level config file not exist");
			return;	
		}else{
			XmlSpreadSheetReader.ReadSheet(text);
			XmlSpreadSheet sheet = XmlSpreadSheetReader.Output;//所有的输出
			string[] keys =  XmlSpreadSheetReader.Keys;//所有的kes


			// 数据
			Dictionary<string,Dictionary<string,string>> dic = new Dictionary<string,Dictionary<string,string>>();

			//id数组
			object[] idArray = sheet[keys[0]];
			for(int i = 3; i< idArray.Length; i++ )//从第二个开始  0：id 1：说明 2
			{
				Dictionary<string,string> one = new Dictionary<string,string>();
				for(int j = 0;j<keys.Length ; j++){
					string key = Convert.ToString( keys[j] );
					string type = Convert.ToString( sheet[key][0] );
					string value = Convert.ToString( sheet[key][i] );
					one[key] = value;

					Debug.Log(key + "==" + value + "==" + type);
				}

				string current = Convert.ToString( idArray[i] );
				dic[current] = one;
			}

			//CreateSceneConfigDataBase(tempList);
		}
	}





	/**
	 * 获取类型并转化 
	 * */
	private void getResultByType(string type){


	}
















	
	private static bool CheckResModified(string[] files)
	{
		bool fileModified = false;
		foreach(string file in files)
		{
			if (file.Contains(RES_UI_CONFIG_FOLDER))
			{
				fileModified = true;	
				break;
			}
		}
		return fileModified;
	}
	
	
	private static void CreateSceneConfigDataBase()
	{
		/**
		string fileName = typeof(EctypePassThroughDataBase).Name;
		string path = System.IO.Path.Combine(ASSET_UI_CONFIG_FOLDER, fileName + ".asset");
		
		if(File.Exists(path))
		{
			EctypePassThroughDataBase database = (EctypePassThroughDataBase)AssetDatabase.LoadAssetAtPath(path, typeof(EctypePassThroughDataBase));
			
			if(null == database)
			{
				return;
			}
			
			database._dataTable = new EctypePassThroughData[list.Count];
			
			for(int i = 0; i < list.Count; i++)
			{
				database._dataTable[i] = list[i];
			}
			EditorUtility.SetDirty(database);
		}
		else
		{
			EctypePassThroughDataBase database = ScriptableObject.CreateInstance<EctypePassThroughDataBase>();
			
			database._dataTable = new EctypePassThroughData[list.Count];
			
			for(int i = 0; i < list.Count; i++)
			{
				database._dataTable[i] = list[i];
			}
			AssetDatabase.CreateAsset(database, path);
		}
		**/
	}
}

