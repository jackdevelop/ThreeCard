using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/**
 * 让UGUI监听的方式和NGUI差不多呢？ 
 * 这里我给大家引一个思路，把下面代码放在你的项目里。
 * 
  使用方法：

  	using System.Collections;
	using UnityEngine.UI;
	using UnityEngine.EventSystems;
	using UnityEngine.Events;
	public class UIMain : MonoBehaviour {
		Button	button;
		Image image;
		void Start () 
		{
			button = transform.Find("Button").GetComponent<Button>();
			image = transform.Find("Image").GetComponent<Image>();
			EventTriggerListener.Get(button.gameObject).onClick =OnButtonClick;
			EventTriggerListener.Get(image.gameObject).onClick =OnButtonClick;
		}
	 
		private void OnButtonClick(GameObject go){
			//在这里监听按钮的点击事件
			if(go == button.gameObject){
				Debug.Log ("DoSomeThings");
			}
		}
	}

* @author  jack   jackdevelop@sina.com
* @date  2015-9-14 上午11:05
* @version 1.0
*/
public class EventTriggerListener : UnityEngine.EventSystems.EventTrigger{
	public delegate void VoidDelegate (GameObject go);
	public VoidDelegate onClick;
	public VoidDelegate onDown;
	public VoidDelegate onEnter;
	public VoidDelegate onExit;
	public VoidDelegate onUp;
	public VoidDelegate onSelect;
	public VoidDelegate onUpdateSelect;
	
	static public EventTriggerListener Get (GameObject go)
	{
		EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
		if (listener == null) listener = go.AddComponent<EventTriggerListener>();
		return listener;
	}
	public override void OnPointerClick(PointerEventData eventData)
	{
		if (enable == false) return;
		if(onClick != null) 	onClick(gameObject);
	}
	public override void OnPointerDown (PointerEventData eventData){
		if (enable == false) return;
		if(onDown != null) onDown(gameObject);
	}
	public override void OnPointerEnter (PointerEventData eventData){
		if (enable == false) return;
		if(onEnter != null) onEnter(gameObject);
	}
	public override void OnPointerExit (PointerEventData eventData){
		if (enable == false) return;
		if(onExit != null) onExit(gameObject);
	}
	public override void OnPointerUp (PointerEventData eventData){
		if (enable == false) return;
		if(onUp != null) onUp(gameObject);
	}
	public override void OnSelect (BaseEventData eventData){
		if (enable == false) return;
		if(onSelect != null) onSelect(gameObject);
	}
	public override void OnUpdateSelected (BaseEventData eventData){
		if (enable == false) return;
		if(onUpdateSelect != null) onUpdateSelect(gameObject);
	}


	/** 设置是否可以点击 **/
	private bool enable = true;
	public void setEnable(bool enable){
		this.enable = enable;
	}

}