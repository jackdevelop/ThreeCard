using UnityEngine;
using System.Collections;
using UnityEngine;
using System.Collections;     
using System.Collections.Generic;  



public enum PopManagerModalType {
	None,
	LockScreen
};

/**
* PopManager
*   ui面板的弹出管理 
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-9-6 上午12:00
* @version 1.1
*/
public class PopUpManager  {

	private static Dictionary<string,GameObject> dicUI = new Dictionary<string,GameObject>();


	/**
	 * 显示 创建  
	 * */
	public static GameObject createPopUp(Object obj,float maskAlpha){
		Transform Panel_UI_UI =  RootCanvasBehviour.getInstance().Panel_UI_UI;
		GameObject  instance = UITool.createUGUI(obj,Panel_UI_UI);

		BaseUIBehaviour baseUIBehaviour = BaseUIBehaviour.Get (instance);
		dicUI.Add(baseUIBehaviour.indexTag ,instance);

		//添加mask  并设置全屏
		PanelMaskBehaviours mask = PopMaskMaskManager.create (instance.transform,maskAlpha);
		UITool.setFullScreen (mask.gameObject);
		mask.transform.SetSiblingIndex(0);


		return instance;
	}


	/**
	 * 删除 
	 * */
	public static void deletePopUp(GameObject  instance){
		BaseUIBehaviour baseUIBehaviour = BaseUIBehaviour.Get (instance);
		string tag = baseUIBehaviour.indexTag;
		if( dicUI.ContainsKey (tag) )//判断有 则移除  
			dicUI.Remove (tag);
	}



	/**
	 * 通过tag  删除 
	 * */
	public static void deletePopUpByIndex(string index){
		if( dicUI.ContainsKey (index) )
			dicUI.Remove (index);
	}
	
}






/**
* PopMessageManager
*   tips的提示管理静态类
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-9-6 上午11:05
* @version 1.1
*/
public class PopMessageManager  {

	public static PanelMessageBehaviours panelMessageBehaviours;
	/**
	 * 显示 创建  
	 * */
	public static void show(string msg){
		//var Panel_UI_Tips= RootCanvasBehviour.getInstance().Panel_UI_Tips; //跟舞台
		//Transform Prefabs_UI_PopMessage = Panel_UI_Tips.transform.Find("Prefabs_UI_PopMessage"); 
		//popMessageBehaviours = Prefabs_UI_PopMessage.gameObject.GetComponent<PopMessageBehaviours>();
		panelMessageBehaviours.createItem(msg);

	}
}






/**
* PopAlertManager
*   Alert 的提示管理静态类
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-9-6 上午10:05
* @version 1.1
*/
public class PopAlertManager  {

	/**
	 * 显示 创建  
	 * */
	public static void show(string title,string msg){
		PanelAlertBehaviours.show (title,msg);
	}
}






/**
 * 
 * 遮罩面板
 *  在项目创建启动  tips层会自动创建一个全局的遮罩层 Panel_Mask
 *  其他每个页面ui需要遮罩单个ui  需要自动手动创建 
 * 
 * 说明：ugui如果需要其禁止接触鼠标事件 
 * 	加個UGUI內建的CanvasGroup組件, 把Interactable和Blocks Raycasts選項取消。
 * */
public class PopMaskMaskManager  {

	public static PanelMaskBehaviours PanelMaskBehaviours;

	/**
	 * 显示
	 * */
	public static void show(float alpha){
		if (PanelMaskBehaviours != null) {
			PanelMaskBehaviours.gameObject.SetActive(true);
			PanelMaskBehaviours.SetAlpha(alpha);
		}
	}

	/**
	 * 隐藏
	 * */
	public static void hide(){
		if (PanelMaskBehaviours != null) {
			PanelMaskBehaviours.gameObject.SetActive(false);
		}
	}




	/**
	 * 创建遮罩 
	 * @param Transform_parent 父类
	 * */
	public static PanelMaskBehaviours create(Transform Transform_parent,float alpha){
		Object obj = Resources.Load("Prefabs/UI/Commons/Prefabs_UI_Mask");
		GameObject instance = UITool.createUGUI(obj,Transform_parent);
		PanelMaskBehaviours PanelMaskBehaviours = instance.GetComponent<PanelMaskBehaviours>();
		PanelMaskBehaviours.SetAlpha (alpha);

		return PanelMaskBehaviours;
	}
}


