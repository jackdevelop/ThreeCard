using UnityEngine;
using System.Collections;     
using System.Collections.Generic;  

using System.Linq;
using System;
using System.Reflection;

using LitJson;




/**
* UserVo
*   用户的对象
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-9-11 上午10:05
* @version 1.1
*/
//[Serializable]
public class UserVo :BaseVo{


	/**用户请求的 token***/
	public string token{set;get;}

	/**用户名**/
	public string userName{set;get;}
	/**用户的称号名称**/
	public string nickName{set;get;}

	/**用户的钱**/
	public int money{set;get;}



}
