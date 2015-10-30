using System;
using System.Collections.Generic;
using System.Linq;


/**
* CardUtil
*   棋牌的工具类 
* 
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-10-28 上午9:05
* @version 1.1
*/
public class CardUtil
{



	/**
	 * 获取某一张卡牌的点数 
	 *  0-9点
	 * */
	private static int getOnePoint(int Card){
		int mCard = Card % 13;
		if(mCard > 9){// 10 11 12 13/0 都为0点 
			mCard = 0;
		}

		return mCard;
	}

	/**
	 * 获取牌的数字  牌面大小
	 * 1-13 大小 
	 * */
	private static int getOneNums(int Card){
		int mCard = Card % 13;
		if(mCard == 0){// 10 11 12 13/0 都为0点 
			mCard = 13;
		}
		
		return mCard;
	}

	/**
	 * * 获取每一方的点数，并提取最后输赢是谁  
	 *  我的牌的数字是 
	 *  黑 40-52
	 *  红 27-39
	 *  梅 14-26
	 *  方 1-13
	 * 
	 * @param  MasterCards 庄家的牌 
	 * @param  GuestCards 自己的牌 
	 * @return 数组，依次为庄家点数，闲家即自己的点数，输赢情况（闲家即自己赢是0，庄家赢是-1，不会有和的情况）
	 * */
	public static int[] getCompareCards(List<int> MasterCards,List<int> GuestCards){
		//每张牌的点数 
		//MasterCards.Sort ();//默认List的排序是升序排序
		int MasterCards1 = CardUtil.getOnePoint (MasterCards [0]);
		int MasterCards2 = CardUtil.getOnePoint (MasterCards [1]);
		
		//GuestCards.Sort ();//默认List的排序是升序排序
		int GuestCards1 = CardUtil.getOnePoint (GuestCards [0]);
		int GuestCards2 = CardUtil.getOnePoint (GuestCards [1]);
		
		
		//计算总点数
		//int masterCardsMax= MasterCards1 + MasterCards2;
		int GuestCardsMax = GuestCards1 + GuestCards2;
		int masterCardsMax= MasterCards1 + MasterCards2;
		
		int MasterCardsPoint = masterCardsMax % 10;
		int GuestCardsPoint = GuestCardsMax % 10;
		
		
		// 计算最后的输赢
		int GuestWin = Config.CODE_SUCCESS;//默认为 guest 赢 
		if(MasterCardsPoint > GuestCardsPoint){//1：谁的点数大，谁赢
			GuestWin = -1;//客户输 
		}else if(MasterCardsPoint == GuestCardsPoint){//2：如果点数一样大，就看谁的大牌牌面大，谁赢
			//获取牌的数字牌面大小
			int MasterCardsNum1 = CardUtil.getOneNums(MasterCards [0]);
			int MasterCardsNum2 = CardUtil.getOneNums(MasterCards [1]);
			
			int GuestCardsNum1 = CardUtil.getOneNums(GuestCards [0]);
			int GuestCardsNum2 = CardUtil.getOneNums(GuestCards [1]);
			
			int maxCardNumMasterCards = MasterCardsNum1>MasterCardsNum2?MasterCardsNum1:MasterCardsNum2;
			int maxCardNumGuestCards = GuestCardsNum1>GuestCardsNum2?GuestCardsNum1:GuestCardsNum2;
			
			if(maxCardNumMasterCards > maxCardNumGuestCards){//3:谁的大牌牌面数字大 则谁赢,
				GuestWin = -1;//客户输 
			}else if(maxCardNumMasterCards == maxCardNumGuestCards){//4:如果大牌的牌面大小一样,那么黑红梅方 依次大小
				if( MasterCards [1]  >  GuestCards [1] ){
					GuestWin = -1;//客户输 
				}
			}
		}

		List<int > result = new List<int>();
		result.Add (MasterCardsPoint);
		result.Add (GuestCardsPoint);
		result.Add (GuestWin);

		int[] results = {MasterCardsPoint,GuestCardsPoint,GuestWin};
		return results;
	}
}

