using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;
using System;
using System.Text;

using System.Reflection;


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;




/**
* Util
*   常用工具类
*  如果要忽略 ugui  的事件相应 ：  加個UGUI內建的CanvasGroup組件, 把Interactable和Blocks Raycasts 選項取消。
* 
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-10-10 上午9:05
* @version 1.1
*/
public class Util 
{



	/**
	 * 获取用户的ip mac地址 
	//原理就是获取网卡的信息。	
	//下面这段代码是我在百度贴吧找来的，经检验是正确的
	**/
	public static string getUserIp(){	
		string userIp = "";

		/**
		NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces(); ;
		foreach (NetworkInterface adapter in adapters)
		{
			　　if (adapter.Supports(NetworkInterfaceComponent.IPv4))
			　　{
				　　　　UnicastIPAddressInformationCollection uniCast = adapter.GetIPProperties().UnicastAddresses;
				　　　　if (uniCast.Count > 0)
				　　　　{
					　　　　　　foreach (UnicastIPAddressInformation uni in uniCast)
					　　　　　　{

						　　　　　　　　//得到IPv4的地址。 AddressFamily.InterNetwork指的是IPv4
						　　　　　　　　if (uni.Address.AddressFamily == AddressFamily.InterNetwork)
						　　　　　　　　{
							　　　　　　　　　　userIp =uni.Address.ToString();
						　　　　　　　　}
					　　　　　　}
				　　　　}
			　　}
		}
		**/
		#if UNITY_STANDALONE_WIN || UNITY_EDITOR  //windows平台和web平台
			NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
			foreach(NetworkInterface adapter in adapters){
				if(adapter.Supports(NetworkInterfaceComponent.IPv4)){
					UnicastIPAddressInformationCollection uniCast = adapter.GetIPProperties().UnicastAddresses;
					if(uniCast.Count > 0 ){
						foreach(UnicastIPAddressInformation uni in uniCast){
							if(uni.Address.AddressFamily == AddressFamily.InterNetwork){
								userIp = uni.Address.ToString();
							}
						}
					}
				}
			}
		#elif UNITY_ANDROID   //安卓
			userIp = "192.168.132";
		#endif

		return userIp;
	}






	/**
	 * 
	 * md5加密 
	 * */
	public static string getMD5CodeByString(string input){	
		System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create(); 
		byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input); 
		byte[] hash = md5.ComputeHash(inputBytes); 

		StringBuilder sb = new StringBuilder(); 
		for (int i = 0; i < hash.Length; i++) 
		{ 
			sb.Append(hash[i].ToString("X2")); 
		} 
		return sb.ToString(); 
	}













	/**
	 * 反射转化类型 
	 * 
	 * 	object tt = Util.ChangeType(value,valueType);
		item.SetValue(entity,tt,null);
	 * */
	public static object ChangeType(object value, Type type)
	{
		if (value == null && type.IsGenericType) return Activator.CreateInstance(type);
		if (value == null) return null;
		if (type == value.GetType()) return value;
		if (type.IsEnum)
		{
			if (value is string)
				return Enum.Parse(type, value as string);
			else
				return Enum.ToObject(type, value);
		}
		if (!type.IsInterface && type.IsGenericType)
		{
			Type innerType = type.GetGenericArguments()[0];
			object innerValue = ChangeType(value, innerType);
			return Activator.CreateInstance(type, new object[] { innerValue });
		}
		if (value is string && type == typeof(Guid)) return new Guid(value as string);
		if (value is string && type == typeof(Version)) return new Version(value as string);
		if (!(value is IConvertible)) return value;
		return Convert.ChangeType(value, type);
	}
	


	/**
	 * 反射序列化 
	 * 

	public static T PopulateEntityFromCollection<T>(T entity,object obj) where T:new(){
		Type oType = Type.GetType(entity);
		LitJson.JsonData json = (LitJson.JsonData)obj;

		//var mMainFieldRule = new FieldRuleInfo[dtGet.Rows.Count];  
		foreach(string key in json){

				//这里直接获取类的字段名称，然后把数据库里对应字段的值赋值给它   
				FieldInfo fieldInfo = oType.GetField(key,
				                                      BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance| BindingFlags.Static);   
				
				if(fieldInfo != null){
					//fieldInfo.SetValue(entity, json[key].ToString());
				}
		}

		return entity;
	}
 * */





	/**
	 * 反射序列化 
	 * 
	 * */
	public static T PopulateEntityFromCollection<T>(T entity,object obj) where T:new(){
		if(entity == null){
			entity = new T();
		}
		Type type = typeof(T);
		PropertyInfo[] propertys1 = type.GetProperties();
		
		LitJson.JsonData json = (LitJson.JsonData)obj;
		foreach(PropertyInfo pi in propertys1){
		//for(int i= 0;i<pi.Length;i++){
			//PropertyInfo item = pi[i];
			object value = null;
			if (((IDictionary)json).Contains(pi.Name)) {
				value = json[pi.Name];
			}


			if(value != null){
				Type valueType = pi.PropertyType;
				object tt = Util.ChangeType(value,valueType);//object tt = Convert.ChangeType(value,item.PropertyType);//单纯这句不行

				//item.SetValue(entity,Convert.ChangeType(item.ToString(), valueType),null);
				pi.SetValue(entity,Convert.ChangeType(tt.ToString(), valueType),null);
				//item.SetValue(item,tt,null);
			}
		}
		
		return entity;
	}



	/**
	 * 
	 *  A a = new A();
    a.Name = "aa";
    a.Age = 1;
    B b = new B();
    a.CopyTo(b);
	 * */
	/**
	public static void CopyTo<T>(this object source, T target)
		where   T : class,new()
	{
		if (source == null)
			return;
		
		if (target == null)
		{
			target = new T();
		}
		
		foreach (var property in target.GetType().GetProperties())
		{
			var propertyValue = source.GetType().GetProperty(property.Name).GetValue(source, null);
			if (propertyValue != null)
			{
				if (propertyValue.GetType().IsClass)
				{ 
					
				}
				target.GetType().InvokeMember(property.Name, BindingFlags.SetProperty, null, target, new object[] { propertyValue });
			}
			
		}
		
		foreach (var field in target.GetType().GetFields())
		{
			var fieldValue = source.GetType().GetField(field.Name).GetValue(source);
			if (fieldValue != null)
			{
				target.GetType().InvokeMember(field.Name, BindingFlags.SetField, null, target, new object[] { fieldValue });
			}
		}
	}
	**/
}













