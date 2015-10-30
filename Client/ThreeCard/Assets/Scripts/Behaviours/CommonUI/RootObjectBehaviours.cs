using UnityEngine;
using System.Collections;
//using System;

/**
 * RootObjectBehaviours
 * RootObject 的 Behaviour 
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-10-5 上午10:11
* @version 1.0
*/
public class RootObjectBehaviours : MonoBehaviour
{
	 
	/**当前场景对应的 ui的 prefable **/
	public Object Prefable_Panel;


	void Awake() {

		//添加 SceneRoot_Panel 
		Object obj = Resources.Load("Prefabs/UI/Panel/Canvas_RootCanvas");
		GameObject  instance = Instantiate(obj) as GameObject;
		instance.transform.parent  = this.transform;
		instance.transform.localScale = Vector3.one; 
		instance.transform.localPosition = Vector3.zero;
		//PanelSceneRootBehviour panelSceneRootBehviour = instance.gameObject.GetComponent<PanelSceneRootBehviour>();


		//mask 遮罩 
		PanelMaskBehaviours PanelMaskBehaviours = PopMaskMaskManager.create (RootCanvasBehviour.getInstance().Panel_UI_Tips.transform,100f);
		PopMaskMaskManager.PanelMaskBehaviours = PanelMaskBehaviours;
		PopMaskMaskManager.hide ();




		//tips的 Prefabs_UI_PopMessage
		obj = Resources.Load("Prefabs/UI/Commons/Prefabs_UI_Message");
		GameObject  Prefabs_UI_PopMessage = Instantiate(obj) as GameObject;
		Prefabs_UI_PopMessage.transform.parent  = RootCanvasBehviour.getInstance().Panel_UI_Tips.transform;
		Prefabs_UI_PopMessage.transform.localScale = Vector3.one; 
		Prefabs_UI_PopMessage.transform.localPosition = Vector3.zero;
		Prefabs_UI_PopMessage.name = "Prefabs_UI_Message";
		PopMessageManager.panelMessageBehaviours = Prefabs_UI_PopMessage.GetComponent<PanelMessageBehaviours>();




		//添加对应的ui 
		//var Panel_UI_UI =  RootCanvasBehviour.getInstance().Panel_UI_UI;
		//instance = UITool.createUGUI(Prefable_Panel,Panel_UI_UI);
		PopUpManager.createPopUp (Prefable_Panel,0f);


	}



	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}
}

