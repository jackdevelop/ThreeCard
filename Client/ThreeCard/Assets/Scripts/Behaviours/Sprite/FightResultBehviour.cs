using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


/**
 * FightResultBehviour
 * 结算弹出面板 的 Behaviour 
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-10-16 下午10:11
* @version 1.0
*/
public class FightResultBehviour : MonoBehaviour {
	//点数
	public Text Txt_MyPoint;//自己的
	public Text Txt_MasterPoint;//庄家的


	/**注数**/
	public Text Txt_MyBetAmount;//下的注数
	public Text Txt_LoseAmount;//输的注数

	
	/**我的 金币 **/
	public Text Txt_Money_My;



	/**返回按钮**/
	public Button	Btn_Back;


	//胜利 失败 
	public Image	Img_failure;
	public Image	Img_victory;


	// Use this for initialization
	void Awake () {
		//EventTriggerListener.Get(Btn_Back.gameObject).onClick =Btn_BackClick;
	}
	

	public void init (int MasterPoin,int MyPoint,int LoseAmount) {
		this.gameObject.SetActive(true);

		Txt_Money_My.text = UserModel.getInstance ().UserVo.money.ToString ();

		Txt_MyBetAmount.text = System.Math.Abs(LoseAmount) + "";
		Txt_LoseAmount.text = -LoseAmount + "";

		Txt_MyPoint.text = MyPoint.ToString() ;//自己的
		Txt_MasterPoint.text = MasterPoin.ToString();//庄家的

		if (LoseAmount > 0) {
			Img_failure.gameObject.SetActive(true);
			Img_victory.gameObject.SetActive(false);
		} else {
			Img_failure.gameObject.SetActive(false);
			Img_victory.gameObject.SetActive(true);
		}
	}




	/**
	 * 返回按钮结算 

	private void Btn_BackClick(GameObject go){
		//在这里监听按钮的点击事件
		if(go == Btn_Back.gameObject){
			this.gameObject.SetActive(false);
		}
	}
	**/
}
