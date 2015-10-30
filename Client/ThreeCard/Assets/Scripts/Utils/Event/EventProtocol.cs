using UnityEngine;
using System.Collections;



/**
// 触发事件
EventProtocol.dispatcher.dispatchEvent(new UEvent(EventTypeName.CUBE_CLICK, "cube"), this);



// 添加事件侦听
EventProtocol.dispatcher.addEventListener (EventTypeName.CUBE_CLICK, OnClickHandler);

/// <summary>
/// 事件回调函数
/// </summary>
/// <param name="uEvent">U event.</param>
private void OnClickHandler(UEvent uEvent)
{
	this.targetAngle = this.targetAngle == 90f ? 0f : 90f;

	this.StopCoroutine (this.RotationOperater ());
	this.StartCoroutine (this.RotationOperater());
}


**/

public class EventProtocol : MonoBehaviour {


	public static readonly UEventDispatcher dispatcher = new UEventDispatcher();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
