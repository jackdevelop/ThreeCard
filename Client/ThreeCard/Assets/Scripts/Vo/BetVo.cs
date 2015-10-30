using UnityEngine;
using System.Collections;     
using System.Collections.Generic;  

using System.Linq;
using System;
using System.Reflection;

using LitJson;




/**
* BetVo
*   下注对象
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-9-11 上午10:05
* @version 1.1
*/
//[Serializable]
public class BetVo :BaseVo{


	/**用户输的钱**/
	public int LoseAmount{set;get;}


	/**出的牌**/
	public Dictionary<string,List<int>> CardsInfo{set;get;}

}
