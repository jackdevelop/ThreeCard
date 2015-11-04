using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;



/**
 * LoginPanelBehviour
 * 登录面板的 Behaviour 
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-10-5 上午10:11
* @version 1.0
*/
public class LoginPanelBehviour : MonoBehaviour {

	/**用户名**/
	public InputField Txt_Name;
	/**密码**/
	public InputField Txt_Password;


	/**登录按钮**/
	public Button	Btn_Login;


	void Awake() {

	}


	void Start () {
		//Btn_Login = transform.Find("Btn_Login").GetComponent<Button>();//可以通过面板去查找 
		EventTriggerListener.Get(Btn_Login.gameObject).onClick =Btn_LoginClick;

		Txt_Name.text = PlayerPrefs.GetString("userName");
	}


	/**
	 * 登录点击
	 * */
	private void Btn_LoginClick(GameObject go){
		//在这里监听按钮的点击事件
		if(go == Btn_Login.gameObject){
			if(Txt_Name.text == "" || Txt_Password.text == ""){
				PopMessageManager.show("用户名，密码不能为空！");
				Echo.Log("用户名，密码不能为空！");
			}else{
				string userName = Txt_Name.text;
				string passWorld = Txt_Password.text;
				UserModel.getInstance().LoginAction(userName,passWorld,loginCallBack);
			}
		}
	}



	void loginCallBack(object obj,object str){
		int result = (int)obj;
		//UserVo userVo = UserModel.getInstance ().userVo;
		if(result == Config.CODE_SUCCESS){
			Application.LoadLevel(SceneConfig.SceneLevelFight);
			//Application.loadedLevelName("");
		}
	}

}
