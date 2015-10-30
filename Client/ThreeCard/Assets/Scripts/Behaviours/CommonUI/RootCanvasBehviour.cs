using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


/**
 * StageObjectBehviour
 * 游戏场景的 Behaviour 
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-9-14 上午11:45
* @version 1.0
*/
public class RootCanvasBehviour : MonoBehaviour {


	/**舞台**/
	public Canvas stage;

	/**几个层级**/	
	public Transform Panel_UI_Back;
	public Transform Panel_UI_Scene;
	public Transform Panel_UI_UI;
	public Transform Panel_UI_Alert;
	public Transform Panel_UI_Tips;
	public Transform Panel_UI_Loading;



	//几个重要的引用



	/**获取单例 **/
	private static RootCanvasBehviour _instance;
	public static RootCanvasBehviour getInstance(){
		return _instance;
	}



	void Awake()
	{  
		if (_instance == null) {
			_instance = this;
		}
	} 


	/**
	 * 初始化一些基础信息
	 * */
	void init(){

	}


	void Update(){
		TimerManager.Run ();
	}
}
