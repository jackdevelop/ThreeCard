using UnityEngine;
using System.Collections;

/**
* BaseUIBehaviour
*   ui面板的BaseUIBehaviour
*  主要是一个唯一标识符  
*  为了在popmanager中管理 
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-9-6 上午12:00
* @version 1.1
*/
public class BaseUIBehaviour : MonoBehaviour
{
	[SerializeField]
	private static int index;

	[SerializeField]//[HideInInspector]
	public string indexTag{ get; set;}



	//清除对象 
	void OnDestroy()
	{  
		PopUpManager.deletePopUpByIndex (indexTag);
	}


	/**
	 * 获取 BaseUIBehaviour 
	 * 并重置 他的 indexTag
	 * */
	static public BaseUIBehaviour Get (GameObject go)
	{
		BaseUIBehaviour listener = go.GetComponent<BaseUIBehaviour>();
		if (listener == null) { 
			listener = go.AddComponent<BaseUIBehaviour> ();

			listener.indexTag = go.name +"_" + index;
			index ++ ;
		}
		return listener;
	}
}

