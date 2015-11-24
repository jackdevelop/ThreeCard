using UnityEngine;
using System.Collections;
using System.Runtime.Hosting;

/**
 * 以前我只能做一个初始化场景， 
 * 在这个场景进行初始化工作， 并且保证这个场景在游戏运行中只能进一次。
 * 
 * 现在好了，unity5提供了初始化回调方法。[RuntimeInitializeOnLoadMethod]  
 * 
 * 这样初始化就和传统游戏开发初始化一样了。 没有场景 没有 游戏对象 的初始化 启动脚本。
 * 
 * [RuntimeInitializeOnLoadMethod]
 * */
public class Instance : MonoBehaviour 
{
	
	
	//[RuntimeInitializeOnLoadMethod]
	static void Initialize()
	{
		GameObject.DontDestroyOnLoad(new GameObject("Instance",typeof(Instance)) {
			hideFlags = HideFlags.HideInHierarchy
		});
		Debug.Log( "RuntimeInitializeOnLoadMethod" );
	}
}