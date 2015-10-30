using UnityEngine;
using System;

public class TimerItem
{
	/// <summary>
	/// 当前时间
	/// </summary>
	public float currentTime;
	
	/// <summary>
	/// 延迟时间
	/// </summary>
	public float delayTime;
	
	/// <summary>
	/// 回调函数
	/// </summary>
	public Action callback;
	
	public TimerItem(float time, float delayTime, Action callback)
	{
		this.currentTime = time;
		this.delayTime = delayTime;
		this.callback = callback;
	}
	
	public void Run(float time)
	{
		// 计算差值
		float offsetTime = time - this.currentTime;
		
		// 如果差值大等于延迟时间
		if(offsetTime >= this.delayTime)
		{
			float count = offsetTime / this.delayTime - 1;
			float mod = offsetTime % this.delayTime;
			
			for(int index = 0; index < count; index ++)
			{
				this.callback();
			}
			
			this.currentTime = time - mod;
		}
	}
}