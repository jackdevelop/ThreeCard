using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using DG.Tweening;



/**
 * PanelAlertBehaviours
 * 
 * alert 的提示  
* 
*  
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-10-5 上午10:11
* @version 1.0
*/
public class PanelAlertBehaviours : MonoBehaviour {

	/**标题**/
	public Text Txt_Title;

	/**内容**/
	public Text Txt_Msg;


	void Awake()
	{  
		EventTriggerListener.Get(this.gameObject).onClick =Btn_Click;
	}



	/**
	 * 充值场景跳转
	 * */
	void Btn_Click(GameObject go){
		this.transform.DOLocalMoveY(Screen.height+10,2f,false);
		//TweenAlpha.Begin (obj, 0.5f, 0);
		//yield return new WaitForSeconds(2f);
		Destroy (this);
	}




	/**
	 * 创建某一个item
	 * */
	public void setText(string title,string msg){
		Txt_Title.text = title;
		Txt_Msg.text = msg;
	}







	/**
	 *  静态方法  
	 *  alert显示  
	 * */
	public static PanelAlertBehaviours show(string title,string msg){
		Object obj = Resources.Load("Prefabs/UI/Commons/Prefabs_UI_Alert");
		GameObject  Prefabs_UI_Alert = Instantiate(obj) as GameObject;
		Prefabs_UI_Alert.transform.parent  = RootCanvasBehviour.getInstance().Panel_UI_Tips.transform;
		Prefabs_UI_Alert.transform.localScale = Vector3.one; 
		Prefabs_UI_Alert.transform.localPosition = Vector3.zero;
		Prefabs_UI_Alert.name = "Prefabs_UI_Alert";
		PanelAlertBehaviours panelAlertBehaviours = Prefabs_UI_Alert.GetComponent<PanelAlertBehaviours>();
		panelAlertBehaviours.setText (title,msg);

		return panelAlertBehaviours;
	}

}
