using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

using UnityEngine;
using System.Collections;


/**
 * SetPanelBehviour
 * 设置面板的 Behaviour 
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-10-16 下午10:11
* @version 1.0
*/
public class SetPanelBehviour : MonoBehaviour {
//public class FightPanelBehviour :BaseCommonPanelBehviour {

	/**我自己当前的金钱**/
	public Text Txt_Money_My;

	/**充值按钮**/
	public Button	Btn_Charge;
	//public Text Txt_Charge;//充值数目
	public InputField Txt_Charge;

	/**结算按钮**/
	public Button	Btn_Balance;

	/**返回按钮**/
	public Button	Btn_Back;


	void Awake() {
		Txt_Money_My.text = UserModel.getInstance ().UserVo.money.ToString();
		//Txt_Charge.text = "0";
		//Btn_Login = transform.Find("Btn_Login").GetComponent<Button>();//可以通过面板去查找 
		EventTriggerListener.Get(Btn_Charge.gameObject).onClick =Btn_ChargeClick;
		EventTriggerListener.Get(Btn_Balance.gameObject).onClick =Btn_BalancClick;
		EventTriggerListener.Get(Btn_Back.gameObject).onClick =Btn_BackClick;
	}




	/**
	 * 返回按钮结算 
	 * */
	private void Btn_BackClick(GameObject go){
		//在这里监听按钮的点击事件
		if(go == Btn_Back.gameObject){
			Application.LoadLevel(SceneConfig.SceneLevelFight);
		}
	}



	/**
	 * 结算处理
	 * */
	private void Btn_BalancClick(GameObject go){
		//在这里监听按钮的点击事件
		if(go == Btn_Balance.gameObject){
			int myMoney = UserModel.getInstance().UserVo.money;
			if(myMoney > 0){
				SetModel.getInstance().BalancAction(balancCallBack);
			}else{
				PopMessageManager.show("当前账户余额必须大于0");
			}
		}
	}
	void balancCallBack(object obj){
		int result = (int)obj;
		if(result == Config.CODE_SUCCESS){
			Txt_Money_My.text = UserModel.getInstance().UserVo.money.ToString();
			Txt_Charge.text = "";
			int ownMoney = SetModel.getInstance().setVo.money;
			//PopMessageManager.show("结算获得:" + ownMoney);

			PopAlertManager.show("","结算获得:" + ownMoney);
			//Application.LoadLevel(SceneConfig.SceneLevelFight);
		}
	}




	/**
	 * 充值处理
	 * */
	private void Btn_ChargeClick(GameObject go){
		//在这里监听按钮的点击事件
		if(go == Btn_Charge.gameObject){
			int Money = int.Parse( Txt_Charge.text );
			if(Money > 0 ){
				SetModel.getInstance().ChargeAction(Money,betChargeCallBack);
			}else{
				PopMessageManager.show("充值金额必须大于0");
			}

		}
	}
	void betChargeCallBack(object obj){
		int result = (int)obj;
		if(result == Config.CODE_SUCCESS){
			Txt_Money_My.text = UserModel.getInstance().UserVo.money.ToString();
			Txt_Charge.text = "";
			PopMessageManager.show("充值成功！");

			//Application.LoadLevel(SceneConfig.SceneLevelFight);
		}
	}

}
