using UnityEngine;
using System.Collections;     
using System.Collections.Generic;  

using System.Linq;
using System;
using System.Reflection;

using LitJson;




/**
* BaseVo
*  基础对象
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-9-11 上午11:05
* @version 1.1
*/
//[Serializable]
public class BaseVo {


	

	public int result{set;get;}

	public string msg{ set; get;}


	public bool wwwError{ set; get; }

	/**
	 * 反射初始化类型 
	 * 
	public UserVo init(object obj){
		LitJson.JsonData json = (LitJson.JsonData)obj;

		Type oType = typeof(UserVo);//.GetType();
		PropertyInfo[] propertys1 = oType.GetProperties();  
		foreach (PropertyInfo pi in propertys1)  
		{  
			object value = null;
			if (((IDictionary)json).Contains(pi.Name)) {
				value = json[pi.Name];

				object tt = Util.ChangeType(value,pi.GetType());
				var newVules = tt.ToString();

				pi.SetValue(this,Convert.ChangeType(newVules, pi.PropertyType),null);
			}

		}
		return this;
	}
	**/

}
