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
 * FightPanelBehviour
 * 战斗面板的 Behaviour 
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-10-16 下午10:11
* @version 1.0
*/
public class FightPanelBehviour : MonoBehaviour {
//public class FightPanelBehviour :BaseCommonPanelBehviour {

	/**我的用户名 **/
	public Text Txt_NickName_My;
	/**我的 金币 **/
	public Text Txt_Money_My;


	/**挂载卡牌的地方**/
	public Transform Transform_CardSprite_MasterCards;//庄家的牌
	public Text Txt_Point_MasterCards;
	public Transform Transform_CardSprite_GuestCards;//自己的牌
	public Text Txt_Point_GuestCards;

	//挂载胜利的地方 
	public Transform Transform_Results;

	/**下注的注数 **/
	public Text Txt_Current_Bet;//当前的下注数
	public Transform Transform_Bet;
	public Texture[] prefable_BetArr;
	public Button[] Btn_BetArr;//名称 Btn_Bet_0
	public int[] BtnNumArr;//下注的数字 

	/**充值按钮**/
	public Button	Btn_Charge;
	/**下注确定按钮**/
	public Button	Btn_BetOk;



	

	void Awake() {
		//Btn_Login = transform.Find("Btn_Login").GetComponent<Button>();//可以通过面板去查找 
		EventTriggerListener.Get(Btn_Charge.gameObject).onClick =Btn_ChargeClick;
		EventTriggerListener.Get(Btn_BetOk.gameObject).onClick =Btn_BetOkClick;
		
		for(int i =0;i<Btn_BetArr.Length;i++){
			Button btn = Btn_BetArr[i];
			EventTriggerListener.Get(btn.gameObject).onClick =Btn_BetClick;
		}
	}


	void Start () {

	}




	/**
	 * 下注
	 * */
	private void Btn_BetClick(GameObject go){
		//在这里监听按钮的点击事件
		string name = go.name;
		string[] nameArr = name.Split(new char[] { '_' });//Btn_Bet_0
		int index = int.Parse( nameArr[2] );

		int btnNum = BtnNumArr[index];//当前的注数
		int flag = FightModel.getInstance().setBenAmount(btnNum);
		if(flag ==  Config.CODE_SUCCESS){
			Txt_Current_Bet.text = FightModel.getInstance().getBenAmount().ToString();

			/**
			//制作一个新的筹码到桌子上  
			GameObject obj_Bet =  Instantiate(prefable_BetArr[index]) as GameObject;
			obj_Bet.transform.parent  = Transform_Bet;
			obj_Bet.transform.localScale = Vector3.one; 
			
			System.Random ran = new System.Random();
			Vector3 vect3 = new Vector3(ran.Next(0,300),ran.Next(0,300),0);
			obj_Bet.transform.localPosition = vect3;
			**/



			/**
			//第二种方法创建 
			object obj = Resources.Load("Resources/Textures/Bets/Bet_1.PNG", typeof(Sprite));
			UnityEngine.Sprite sp = obj as Sprite;
			//UnityEngine.UI.RawImage rawImage = new RawImage();
			UnityEngine.UI.Image  image =  Image.Instantiate(obj);
			image.sprite = sp;
			image.transform.parent = Transform_Bet;

			GameObject button = 	GameObject.Instantiate(Resources.Load<GameObject>("Textures/Bets/Bet_1.PNG"))as GameObject;
			button.transform.parent = Transform_Bet;
			button.transform.localPosition = Vector3.zero;
			button.transform.localScale = Vector3.one;
			//GameObject AObj = transform.Find("A").gameObject;
			//GameObject BObj = transform.Find("B").gameObject;
			//button.transform.SetSiblingIndex(AObj.transform.GetSiblingIndex());
			**/


			//第三种方法  创建图片 
			GameObject obj = UITool.createUGUIImage("Sprite/sprite_Bet_" + index,Transform_Bet);
			System.Random ran = new System.Random();//随机坐标 
			Vector3 vect3 = new Vector3(ran.Next(-100,100),ran.Next(-70,70),0);
			obj.transform.localPosition = vect3;
			obj.name = "sprite_Bet_" + index;
			EventTriggerListener.Get(obj).onClick =Img_BetOkClick;//点击事件

		}else if(flag == -1){
			PopMessageManager.show("添加下注失败,下注不能超过：" + Config.MaxBet);
		}else if(flag == -2){
			PopMessageManager.show("添加下注失败,下注不能超过用户钱袋");
		}
	}



	/**
	 * 回注 
	 * */
	private void Img_BetOkClick(GameObject go){
		string name = go.name;
		string[] nameArr = name.Split(new char[] { '_' });//Btn_Bet_0
		int index = int.Parse( nameArr[2] );
		int btnNum = BtnNumArr[index];//当前的注数
		int flag = FightModel.getInstance().setBenAmount(-btnNum);
		if (flag == Config.CODE_SUCCESS) {
			Txt_Current_Bet.text = FightModel.getInstance ().getBenAmount ().ToString ();
			Destroy (go);
		}
	}



	/**
	 * 充值场景跳转
	 * */
	private void Btn_ChargeClick(GameObject go){
		Application.LoadLevel(SceneConfig.SceneSet);
	}



	/**
	 * 下注确定 
	 * */
	private void Btn_BetOkClick(GameObject go){
		//在这里监听按钮的点击事件
		if(go == Btn_BetOk.gameObject){
			if (FightModel.getInstance().getBenAmount() <= 0) {//下注数为0
				PopMessageManager.show("current is 0");
			}else{
				PopMaskMaskManager.show(0.5f);
				FightModel.getInstance().BetAction(betOkCallBack);
			}
		}
	}
	void betOkCallBack(object obj,object str){
		int result = (int)obj;
		if(result == Config.CODE_SUCCESS){
			int BenAmount = FightModel.getInstance().getBenAmount();

			//生产牌
			Dictionary<string,List<int>> CardsInfo = FightModel.getInstance().betVo.CardsInfo;
			List<int> MasterCards = CardsInfo["MasterCards"];MasterCards.Sort();//默认List的排序是升序排序
			List<int> GuestCards = CardsInfo["GuestCards"];GuestCards.Sort();

			//庄家的
			GameObject cardSpriteMaster = UITool.createUGUIImage("Sprite/sprite_CardSprite_" + MasterCards[0],Transform_CardSprite_MasterCards);
			cardSpriteMaster.transform.localPosition = new Vector3(-10,0,0);
			cardSpriteMaster = UITool.createUGUIImage("Sprite/sprite_CardSprite_" + MasterCards[1],Transform_CardSprite_MasterCards);
			cardSpriteMaster.transform.localPosition = new Vector3(10,0,0);

			//自己的 
			GameObject cardSpriteGuest = UITool.createUGUIImage("Sprite/sprite_CardSprite_" + GuestCards[0],Transform_CardSprite_GuestCards);
			cardSpriteGuest.transform.localPosition = new Vector3(-10,0,0);
			cardSpriteGuest = UITool.createUGUIImage("Sprite/sprite_CardSprite_" + GuestCards[1],Transform_CardSprite_GuestCards);
			cardSpriteGuest.transform.localPosition = new Vector3(10,0,0);


			int[] compareResult = CardUtil.getCompareCards(MasterCards,GuestCards);
			Txt_Point_MasterCards.text = compareResult[0] +" 点";
			Txt_Point_GuestCards.text = compareResult[1] +" 点";

			PopMaskMaskManager.show(0f);

			StartCoroutine(BetIEnumerator(compareResult[2]));
		}
	}
	IEnumerator BetIEnumerator(int GuestWin)
	{
		//yield return new WaitForSeconds(1f);

		string name = "sprite_failure";
		if (GuestWin == Config.CODE_SUCCESS) {//客户输 
			name = "sprite_victory";
		}
		//Transform_CardSprite_MasterCards.gameObject
		GameObject cardSpriteGuest = UITool.createUGUIImage("Sprite/"+name,Transform_Results);
		cardSpriteGuest.transform.localPosition = new Vector3(-10,0,0);

		yield return new WaitForSeconds(2f);
		cleanScene ();
	}


	/**
	 * 清除场景界面 并清空数据 
	 * */
	private void cleanScene(){
		UITool.ClearChild (Transform_CardSprite_MasterCards);
		UITool.ClearChild (Transform_CardSprite_GuestCards);
		UITool.ClearChild (Transform_Bet);

		//文字清除 
		Txt_Point_MasterCards.text = "";
		Txt_Point_GuestCards.text = "";

		Txt_Current_Bet.text = "";
		FightModel.getInstance ().setBenAmount (-FightModel.getInstance ().getBenAmount());

		PopMaskMaskManager.hide();
	}



}
