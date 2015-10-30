using UnityEngine;
using System.Collections;

/**
 * 资源管理 
 * http://www.xuanyusong.com/archives/3229
 * */
public class ResourcesManager : MonoBehaviour {


	/**获取单例 **/
	private static ResourcesManager _instance;
	public static ResourcesManager getInstance(){
		if(_instance == null)
			_instance = new ResourcesManager();
		
		return _instance;
	}






}
