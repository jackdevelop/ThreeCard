using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using DG.Tweening;



/**
 * PanelMessageBehaviours
 * 
 * tips 的提示  
* 
*  
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-10-5 上午10:11
* @version 1.0
*/
public class PanelMessageBehaviours : MonoBehaviour {


	/**某一个item**/
	public GameObject item;

	//消失时间  
	const float TIME_ITEM_DESTROY = 0.5f;






	/**
	 * 创建某一个item
	 * */
	public void createItem(string text){
		GameObject itemPref = Instantiate(item) as GameObject;
		itemPref.SetActive(true); 
		itemPref.transform.parent  = transform;
		itemPref.transform.localScale = Vector3.one; 
		itemPref.transform.localPosition = Vector3.zero;


		Text Txt_label = itemPref.GetComponentInChildren<Text>(); 
		Txt_label.text = text;


		/**
		if (label.height > label.fontSize) {
			label.alignment = NGUIText.Alignment.Left;
			float scal = 1.0f*label.height / label.fontSize;
			UISprite box = itemPref.transform.FindChild("box").GetComponent<UISprite>();
			box.height =(int)(box.height*scal);
		}
		**/

		//itemPref.transform.d .DOMove(new Vector3(2,2,2), 2).SetEase(Ease.OutQuint).SetLoops(4).OnComplete(myFunction);
		//TweenAlpha.Begin (itemPref, 0.5f, 1);
		StartCoroutine(StartDestroyItem(itemPref,TIME_ITEM_DESTROY));
	}


	IEnumerator StartDestroyItem(GameObject obj,float time){ 
		yield return new WaitForSeconds(time);
		if (obj != null) {
			obj.transform.DOLocalMoveY(Screen.height+10,4f,false);
			//TweenAlpha.Begin (obj, 0.5f, 0);
			yield return new WaitForSeconds(4f);
			if(obj!=null) Destroy(obj);	
		}   
	}
}
