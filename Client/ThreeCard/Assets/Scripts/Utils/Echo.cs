using UnityEngine;
using System.Collections;


/**
 * 输出打印 
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-9-14 上午12:05
* @version 1.0
*/
public class Echo  {

	/**
	#if UNITY_EDITOR
	public static Action Log= Debug.Log;
	#else
	static public void Log(object message)
	{
		Log(message,null);
	}
	#endif
	**/

	static private bool EnableLog = Config.EnableLog;
	static public void Log(object message)
	{
		Log(message,null);
	}
	static public void Log(object message, Object context)
	{
		if(EnableLog)
		{
			Debug.Log(message,context);
		}
	}
	static public void LogError(object message)
	{
		LogError(message,null);
	}
	static public void LogError(object message, Object context)
	{
		if(EnableLog)
		{
			Debug.LogError(message,context);
		}
	}
	static public void LogWarning(object message)
	{
		LogWarning(message,null);
	}
	static public void LogWarning(object message, Object context)
	{
		if(EnableLog)
		{
			Debug.LogWarning(message,context);
		}
	}
}