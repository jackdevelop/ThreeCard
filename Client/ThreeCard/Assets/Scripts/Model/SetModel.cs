using UnityEngine;
using System.Collections;     
using System.Collections.Generic;  

using System.Linq;
using System;

using LitJson;

/**
 * 设置模型
 * */
public class SetModel {

	/**获取单例 **/
	private static SetModel _instance;
	public static SetModel getInstance(){
		if(_instance == null)
			_instance = new SetModel();

		return _instance;
	}


	/**用户的 SetVo **/
	public SetVo setVo;



	/**
	 * 结算处理 
	 * 结算：BalanceAction
		参数：1：Token 用户的seeion标示符
		返回：1：Result 结果（0表示成功，其他均是错误码，
		-1：用户异地登陆，须管理员先注销上次登陆ip，-2：Token不正确）

	 * */
	public void BalancAction(Action<object> callBack){
		Dictionary<string,object> param = new Dictionary<string,object>();
		param.Add("token",UserModel.getInstance().UserVo.token);
		
		PopMaskMaskManager.show (0.5f);


		Action<object,object> action;
		action = (object data,object str) => {
			setVo = (SetVo)data;
			int result = setVo.result;//System.Convert.ToInt32( data["Result"] );
			if(result == Config.CODE_SUCCESS){
				UserModel.getInstance().UserVo.money = 0;
				if(callBack != null ) callBack(result);
			}
			
			PopMaskMaskManager.hide();
		};

		HttpLoadManager.getInstance.json<SetVo>("api/v1/users/balance",action,param);
	}

	/**
	 * 充值：ChargeAction
	参数：1：Token 用户的seeion标示符
	  2：Money 充值的金额
	返回：1：Result 结果（0表示成功，其他均是错误码，
	-1：用户异地登陆，须管理员先注销上次登陆ip，-2：Token不正确）
	  2：Money 返回用户充值完后，账户的总余额
  **/
	public void ChargeAction(int Money,Action<object> callBack){
		Dictionary<string,object> param = new Dictionary<string,object>();
		param.Add("money",Money);
		param.Add("token",UserModel.getInstance().UserVo.token);
		
		PopMaskMaskManager.show (0.5f);
		//测试Action委托
		//Action<object> action;
		Action<object,object> action;
		action = (object data,object str) => {
			BaseVo baseVo = data as BaseVo;
			int result = baseVo.result;//System.Convert.ToInt32( data["Result"] );
			if(result == Config.CODE_SUCCESS){
				//UserModel.getInstance().UserVo.money = UserModel.getInstance().UserVo.money + Money;
				JsonData json = JsonMapper.ToObject(str.ToString());
				UserModel.getInstance().UserVo.money = int.Parse( json["money"].ToString()  );

				if(callBack != null ) callBack(result);
			}

			PopMaskMaskManager.hide();
		};
		
		//Action action 
		HttpLoadManager.getInstance.json<BaseVo>("api/v1/users/charge",action,param);
	}





	
}
