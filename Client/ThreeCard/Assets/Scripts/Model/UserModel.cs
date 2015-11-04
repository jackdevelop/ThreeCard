using UnityEngine;
using System.Collections;     
using System.Collections.Generic;  

using System.Linq;
using System;

using LitJson;

/**
 * 用户模型
 * */
public class UserModel {


	/**用户的uservo**/
	private UserVo userVo;
	public UserVo UserVo{
		get{
			if(userVo == null)
				userVo = new UserVo();
			return userVo;
		}
		set{userVo = value;}
	}





	/**获取单例 **/
	private static UserModel _instance;
	public static UserModel getInstance(){
		if(_instance == null)
			_instance = new UserModel();

		return _instance;
	}



	/**
	 * 登录 
	 * （一）：
		登陆：LoginAction
		参数：1：UserName 用户名
		      2：PassWaorld 密码（md5加密）
		 	  3：MacAddress 用户的mac地址
		      4：IPAddress  用户的ip地址（若服务器应该可以获取用户的ip地址，如果可以就
		不需要传输）
		返回：1：Result 结果（0表示成功，其他均是错误码，
		-1：用户异地登陆，须管理员先注销上次登陆ip，-2：Token不正确
		1：没有该用户，2：用户名错误）
		  2：Token 用户的seeion标示符
		 	  3：Money 如果上次用户还有钱没结算，吧剩余钱返回，否则为0即可
	 * */
	public void LoginAction(string userName,string passWorld,Action<object,object> callBack){

		PopMaskMaskManager.show (0.5f);

		Dictionary<string,object> param = new Dictionary<string,object>();
		//param.Add("Action","LoginAction");
		param.Add("username",userName);
		param.Add("password",passWorld);//Util.getMD5CodeByString(passWorld));

		string ip = Util.getUserIp ()+"";
		PopMessageManager.show (ip);

		param.Add("macAddress",ip);
		param.Add("ipAddress",ip);

		//PopMessageManager.show ();
		//Echo.Log("mac:" + Util.getUserIp());
		//Echo.Log("passworld:" + Util.getMD5CodeByString(passWorld));
		/**
		//测试Func委托
		Func<Dictionary<string,string>, bool> fun;
		fun = (Dictionary<string,string> data) => { 
			int result = System.Convert.ToInt32( data["Result"] );
			if(result == Config.successCode){
				if(callBack != null ) callBack();
			}


			return false;
		};
		**/


		//测试Action委托
		Action<object,object> actionFun;
		actionFun = (object data,object str) => {
			userVo = (UserVo)data;
			int result =  userVo.result;//System.Convert.ToInt32( data["Result"] );
			PopMaskMaskManager.hide();
			if(result == Config.CODE_SUCCESS){
				userVo.nickName = "张三";
				userVo.userName = userName;
				PlayerPrefs.SetString("nickName",userVo.nickName);
				if(callBack != null ) callBack(result,str);
			}else if(result == 1){
				PopMessageManager.show("username is error");
			}else if(result == 2){
				PopMessageManager.show("password is error");
			}

		};
		HttpLoadManager.getInstance.json<UserVo>("api/v1/login",actionFun,param);
	}

	
}
