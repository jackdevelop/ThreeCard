using UnityEngine;
using System.Collections;

public class DocmentNote : MonoBehaviour
{














	#if UNITY_EDITOR
	/**
	 * 如下图所示， Reset方法绑定脚本时会执行一次。以后每次点击”Reset”也会执行。
	 * OnValidate方法是脚本中序列化的数据发生改变，
	 * 比如这里字符串name变量发生变化后就会执行了。
	 * */
	void Reset()
	{
		Debug.Log("脚本添加事件");
	}
	
	
	void OnValidate()
	{
		Debug.Log("脚本对象数据发生改变事件");
	}
	
	#endif
}

