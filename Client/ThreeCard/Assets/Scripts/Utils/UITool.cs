using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITool :MonoBehaviour
{

	/**
		 * 快速创建UGUI 
		 * @param name 地址
		 * */
	public static GameObject createUGUI(Object obj,Transform Transform_parent)
	{
		//MonoBehaviour monoBehaviour = Transform_parent.GetComponent<MonoBehaviour> ();
		GameObject instance = Instantiate(obj) as GameObject;
		instance.transform.parent  = Transform_parent;
		instance.transform.localScale = Vector3.one; 
		instance.transform.localPosition = Vector3.zero;
		//设置对称
		RectTransform rectTransformPanel_UI_UI = instance.GetComponent<RectTransform>();
		if(rectTransformPanel_UI_UI != null){
			rectTransformPanel_UI_UI.localPosition = new Vector3(0, 0, 0);
			rectTransformPanel_UI_UI.sizeDelta = new Vector2(0f ,0f );
		}
		return instance;
	}



		/**
		 * 创建图片 
		 * @param name 地址
		 * */
		public static GameObject createUGUIImage (string url,Transform Transform_parent)
		{
			//第三种方法  创建图片 
			Sprite sprite = Resources.Load<GameObject>(url).GetComponent<SpriteRenderer>().sprite;
			GameObject obj = new GameObject(sprite.name);	
			obj.transform.parent = Transform_parent;
			obj.layer = LayerMask.NameToLayer("UI");		
			obj.transform.localScale= Vector3.one;		
			Image image = obj.AddComponent<Image>();		
			image.sprite = sprite;		
			image.SetNativeSize();
			obj.transform.localScale= Vector3.one;		
			//obj.transform.localPosition = Vector3.zero;
			return obj;
		}


	public static void ClearChild(Transform parent){
		var childCount = parent.childCount;
		if(childCount > 0){
			for(int i=0;i<childCount;i++){
				UnityEngine.Object.Destroy(parent.GetChild(i).gameObject);
			}
		}
	}

	/**
	 * 更新 适应分辨率 
	 * 在UGUI中想设置一张全屏的背景图，直接设置 screen.width和screen.height后发现在有些分辨率下还是不能全屏。
	 * 调用下面方法 
	 * */
	public static void setFullScreen(GameObject gameObject){
		/* *
		int currentWidth = Screen.width;
		int currentHeight = Screen.height;
		if(currentWidth != lastWidth || currentHeight != lastHeight ){
			lastWidth = currentWidth;
			lastHeight =currentHeight;


			RectTransform rectTransform = GetComponent<RectTransform>();
			rectTransform.localPosition = new Vector3(0, 0, 0);
			rectTransform.sizeDelta = new Vector2((float)lastWidth ,(float)lastHeight );
		}
		 * */
		
		
		
		int width = Screen.width;
		int height = Screen.height;
	
			
		int designWidth = 960;//开发时分辨率宽
		int designHeight = 640;//开发时分辨率高
		float s1 = (float)designWidth / (float)designHeight;
		float s2 = (float)width / (float)height;
		if (s1 < s2) {
			designWidth = (int)Mathf.FloorToInt (designHeight * s2);
		} else if (s1 > s2) {
			designHeight = (int)Mathf.FloorToInt (designWidth / s2);
		}
		float contentScale = (float)designWidth / (float)width;
		RectTransform rectTransform = gameObject.transform as RectTransform;
		if (rectTransform != null) {
			rectTransform.sizeDelta = new Vector2 (designWidth, designHeight);
		}
		
		
	}
}

