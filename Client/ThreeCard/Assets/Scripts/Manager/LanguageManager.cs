using UnityEngine;
using System.Collections;


using UnityEngine;
using System.Collections;     
using System.Collections.Generic;  

using System.Linq;
using System;

using LitJson;







/**
 * LanguageManager
 * LanguageManager  管理
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-10-15 下午4:05
* @version 1.1
*/
public class LanguageManager : MonoBehaviour
{
	//语言的字符集 
	private Dictionary<string , string> m_languageTxt;






	/**获取单例 **/
	private static LanguageManager _instance;
	public static LanguageManager getInstance(){
		if(_instance == null){
			_instance = new LanguageManager();
		}
		return _instance;
	}









	/**
	 * load 基础的数据
	 * 
	 * */
	public void init(){
		if(m_languageTxt == null){
			m_languageTxt = new Dictionary<string, string>();
		}


//		foreach(LanguageTextEntry entry in database.stringTable){
//			m_languageTxt[entry.key] = entry.text;	
//			
//		}
	}









	/**
	 * 通过key 获取相应的语言文字
	 * @param key
	 * @param string 
	 * */
	public string GetString(string key){
		string result = null;
		if (string.IsNullOrEmpty (key) == false   &&   m_languageTxt.ContainsKey (key)) {
			result = m_languageTxt [key];	
		} else {
			result = "null string： id:" + key;
		}
		

		return result;
	}




}
