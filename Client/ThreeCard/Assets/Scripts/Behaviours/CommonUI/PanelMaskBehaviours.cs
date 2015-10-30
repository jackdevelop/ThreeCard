using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using DG.Tweening;



/**
 * PanelMaskBehaviours
 * 
 * 全局遮罩
* 
*  
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-10-5 上午10:11
* @version 1.0
*/
public class PanelMaskBehaviours : MonoBehaviour {

	/**
	 * 设置透明度
	 * */
	public void SetAlpha(float alpha){
		CanvasRenderer CanvasRenderer = this.GetComponent<CanvasRenderer>();
		CanvasRenderer.SetAlpha (alpha);
	}


}
