using UnityEngine;
using System.Collections;     
using System.Collections.Generic;  

using System.Linq;
using System;

using LitJson;







/**
 * HttpLoadManager
 * http管理
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-10-15 下午4:05
* @version 1.1
*/
public class HttpLoadManager : MonoBehaviour
{
	/**获取单例 **/
	private static HttpLoadManager _instance;


	void Awake()
	{  
		if (_instance == null) {
			_instance = this;
		}
	}





	/**
	 * 获取单例
	 * */
	public static HttpLoadManager getInstance{ 
		get{
			if(_instance==null) { 
				lock (typeof(HttpLoadManager)) { 
					GameObject gom = GameObject.Find("/_HttpLoadManager"); 
					if(gom==null){
						gom = new GameObject("_HttpLoadManager");
						DontDestroyOnLoad(gom);
						if(_instance==null){
							gom.AddComponent<HttpLoadManager>(); 
						}
					}else{
						_instance = gom.GetComponent<HttpLoadManager>();
						if(_instance==null){
							gom.AddComponent<HttpLoadManager>(); 
						}
					}
				}
			}
			return _instance;
		}
	}




	/**
	 * http的 json请求 
	 * @param callBack
	 * @param  param
	 * */
	public void json<T>(string action,Action<object,object> callBack,Dictionary<string,object> param)     
	{  
		if(param == null ) param = new Dictionary<string,object >();


		//param.Token = Config.Token;

		StartCoroutine(
			POST<T>(Config.URL+action,callBack,param)
		);  
	}








	



	/**
	 *   //注册请求 POST     
            Dictionary<string,string> dic = new Dictionary<string, string> ();     
            dic.Add("Action","1");     
            dic.Add("usrname","xys");     
            dic.Add("psw","123456");     
              
            StartCoroutine(POST("http://192.168.1.12/login.php",dic));     
	 * */
	//POST请求     
	IEnumerator POST<T>(string url,Action<object,object> callBack, Dictionary<string,object> param)  
	{   
		WWWForm form = new WWWForm();  

		if(param != null){
			foreach(KeyValuePair<string,object> post_arg in param)     
			{     
				if(post_arg.Value != null){
					form.AddField(post_arg.Key, post_arg.Value.ToString());  
				}
			}
		}

		
		

		//第一种方法
		//这是get请求的头参数 
		Hashtable headers = form.headers;  
		//headers.Remove ("Content-Type");
		//headers.Add("Content-Type", "application/json"); 
		//headers.Add("Content-Type", "application/x-www-form-urlencoded"); 

		if(param.ContainsKey("token"))
			headers.Add("Authorization",param["token"]);

		WWW www = new WWW(url, form.data,headers);     

		//第二种方法 
		//WWW www = new WWW(url, form);     

		yield return www;     
		if (www.isDone) {
			string data = "";
			if (www.error != null) {
				data = "{'result':-100,'msg':'"+ www.error +"'}";
			} else {
				data= www.text;
			}



			if (callBack != null) {
				Echo.Log("callBack:" + data);
				
				T t = JsonMapper.ToObject<T> (data);
				Filter (t);
				callBack (t,data);
			}
		}


	}     
	
	//GET请求     
	IEnumerator GET(string url)     
	{     
		
		WWW www = new WWW (url);     
		yield return www;     
		
		if (www.error != null)     
		{     
			//GET请求失败     
			PopMessageManager.show("www error !");
			Echo.Log("error is :"+ www.error);
		} else 
		{     
			//GET请求成功     
			Echo.Log("request ok : " + www.text);     
		}     
	}   




	/**
	 * 过滤公共的错误码接口 
	 * */
	private void Filter(object data)     
	{   
		BaseVo baseVo = (BaseVo)data;
		int code = baseVo.result;
		
		if (code == Config.CODE_SUCCESS) {
				return;
		} else if (code == Config.CODE_Remote_login) {//异地登录
				PopMessageManager.show ("remote login ");
		} else if (code == Config.CODE_Token_Error) {//token 不正确
				PopMessageManager.show ("token error ");
		} else if (code == Config.CODE_WWW_Error) {
			PopMessageManager.show ("www error !" + baseVo.msg);//POST请求失败   
			Echo.Log ("error is :" + baseVo.msg);
		}

	}

	private void FilterJson(JsonData json)     
	{   
		int code = int.Parse( json["result"].ToString() );
		
		if(code == Config.CODE_SUCCESS){
			return;
		}else if(code == Config.CODE_Remote_login){//异地登录
			PopMessageManager.show("remote login ");
		}else if(code == Config.CODE_Token_Error){//token 不正确
			PopMessageManager.show("token error ");
		}
	}

}

