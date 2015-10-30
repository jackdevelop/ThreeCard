using UnityEngine;
using System.Collections;

public class Platform {
	
	private static Platform instance;
	public static Platform getInstance(){
		if (instance == null){
			instance = new Platform();
		}
		return instance;
	}



	public void getPlatform(){
		string platform = string.Empty;

		#if UNITY_EDITOR  
			platform = "在unity编辑模式下";  
		#elif UNITY_XBOX360  
			platform = "hi，大家好,我在XBOX360平台";  
		#elif UNITY_IPHONE  
			platform="hi，大家好,我是IPHONE平台";  
		#elif UNITY_ANDROID  
			 platform="hi，大家好,我是ANDROID平台";  
		#elif UNITY_STANDALONE_OSX  
			 platform="hi，大家好,我是OSX平台";  
		#elif UNITY_STANDALONE_WIN  
			 platform="hi，大家好,我是Windows平台";  
		#endif  

		//Application.loadedLevelName  场景名称
 
		Debug.Log("Current Platform:" + platform);
	}

}
