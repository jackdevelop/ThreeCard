using UnityEngine;
using System.Collections;     
using System.Collections.Generic;  

using System.Linq;
using System;

using LitJson;

/**
 * 用户模型
 * */
public class FightModel {


	/**用户的 betVo **/
	public BetVo betVo;

	/**用户当前的注数***/
	private int BenAmount;



	/**获取单例 **/
	private static FightModel _instance;
	public static FightModel getInstance(){
		if(_instance == null)
			_instance = new FightModel();

		return _instance;
	}



	//添加
	public int setBenAmount(int betNum){
		int newBetNum = this.BenAmount + betNum;
		if(newBetNum > Config.MaxBet ){

			return -1;
		}

		int myMoney = UserModel.getInstance ().UserVo.money;
		if(newBetNum > myMoney ){
			
			return -2;
		}

		this.BenAmount = newBetNum;
		return Config.CODE_SUCCESS;
	}
	public int getBenAmount(){
		return this.BenAmount;
	}

	/**
		玩家下注：BetAction
		参数：1：Token 用户的seeion标示符
		 	  2：GameId 游戏id，以后可能各种游戏集成
		      3：BenAmount 当前下注的金额
		返回：1：Result 结果（0表示成功，其他均是错误码，
		-1：用户异地登陆，须管理员先注销上次登陆ip，-2：Token不正确
		1：用户当前下注金额超过设置最大金额
		）
		 	  2：LoseAmount 该盘下注用户输赢多少钱
		  2：CardsInfo 返回的游戏信息，不同游戏id，这个参数不一样
		{
		MasterCards：为数组，庄家的牌
		GuestCards：为数组，闲家即自己的牌
		}
	 * */
	public void BetAction(Action<object,object> callBack){
		Dictionary<string,object> param = new Dictionary<string,object>();
		param.Add("betAmount",BenAmount);
		param.Add("token",UserModel.getInstance().UserVo.token);
		param.Add("gameId",Config.GameId);

		PopMaskMaskManager.show(0.5f);
		//测试Action委托
		//Action<object> action;
		Action<object,object> action;
		action = (object data,object str) => {
			
			betVo = (BetVo)data;
			int result = betVo.result;//System.Convert.ToInt32( data["Result"] );
			if(result == Config.CODE_SUCCESS){
				UserModel.getInstance().UserVo.money = betVo.money;
				if(callBack != null ) callBack(result,str);
				return;
			}else if(result == 1){
				PopMessageManager.show("current is max Bet");
			}

			PopMaskMaskManager.hide();
		};

		//Action action 
		HttpLoadManager.getInstance.json<BetVo>("api/v1/users/bet",action,param);
	}





	
}
