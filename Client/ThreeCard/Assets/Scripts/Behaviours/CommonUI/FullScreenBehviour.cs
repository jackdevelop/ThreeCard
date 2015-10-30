using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;



/**
 * FullScreenBehviour
 * 公共面板的 基础 Behaviour 
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-10-5 上午12:20
* @version 1.0
*/
public class FullScreenBehviour : MonoBehaviour {


	private int lastWidth;
	private int lastHeight;

	void Awake() {
		setFullScreen ();
	}




	void Update(){
		#if UNITY_EDITOR  
			setFullScreen();
		#endif  
	}


	/**
	 * 更新 适应分辨率 
	 * 在UGUI中想设置一张全屏的背景图，直接设置 screen.width和screen.height后发现在有些分辨率下还是不能全屏。
	 * 调用下面方法 
	 * */
	private void setFullScreen(){
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

		if (width != lastWidth || height != lastHeight) {
			lastWidth = width;
			lastHeight = height;

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
			RectTransform rectTransform = this.transform as RectTransform;
			if (rectTransform != null) {
					rectTransform.sizeDelta = new Vector2 (designWidth, designHeight);
			}
		}

	}
}
