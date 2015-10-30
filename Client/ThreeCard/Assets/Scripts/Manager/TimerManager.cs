using UnityEngine;
using System;
using System.Collections.Generic;


/**
 * 
public GameObject roleObject;

void Awake()
{
	TimerManager.Register(this, 0.3f, ()=>
	{
		Debug.Log("0.3 -> " + System.DateTime.Now.ToString("hh:mm:ss.fff"));
	});
	TimerManager.Register(this.gameObject, 1f, ()=>
	{
		Debug.Log("1 -> " + System.DateTime.Now.ToString("hh:mm:ss.fff"));
	});
}


//这里已经在 PanelSceneRootBehviour 的 update 中 更改了  
void Update()
{
	TimerManager.Run ();
}

 * 
 * */
public class TimerManager
{
	public static float time;
	
	public static Dictionary<object, TimerItem> timerList = new Dictionary<object, TimerItem>();
	
	public static void Run()
	{
		// 设置时间值
		TimerManager.time = Time.time;
		
		TimerItem[] objectList = new TimerItem[timerList.Values.Count];
		timerList.Values.CopyTo(objectList, 0);
		
		// 锁定
		foreach(TimerItem timerItem in objectList)
		{
			if(timerItem != null) timerItem.Run(TimerManager.time);
		}
	}
	
	public static void Register(object objectItem, float delayTime, Action callback)
	{
		if(!timerList.ContainsKey(objectItem))
		{
			TimerItem timerItem = new TimerItem(TimerManager.time, delayTime, callback);			
			timerList.Add(objectItem, timerItem);
		}
	}
	
	public static void UnRegister(object objectItem)
	{
		if(timerList.ContainsKey(objectItem))
		{
			timerList.Remove(objectItem);
		}
	}
}