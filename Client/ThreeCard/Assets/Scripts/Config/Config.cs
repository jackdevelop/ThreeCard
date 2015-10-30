using UnityEngine;
using System.Collections;

/**
 *  基础的 config 配置文件   
 * 
 * @author  jack   jackdevelop@sina.com
 * @date  2015-9-14 上午11:25
 * @version 1.0
* */
public class Config  {

	/**是否打印echo到控制台**/
	public static bool EnableLog = true;



	/**成功的错误码**/
	public const int CODE_SUCCESS = 0;
	public const int CODE_Remote_login = -1;//1：用户异地登陆，须管理员先注销上次登陆ip，
	public const int CODE_Token_Error = -2;//-2：Token不正确



	/**游戏di**/
	public static int GameId = 1;
	public static int MaxBet = 500;
	/**请求的url地址**/
	public static string URL = "http://game_machine.meteor.com/";
}
