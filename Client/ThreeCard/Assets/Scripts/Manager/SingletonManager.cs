using UnityEngine;
using System.Collections;



/**
 * SingletonManager
 * 单例管理
* 
* @author  jack   jackdevelop@sina.com
* @date  2015-10-15 下午3:05
* @version 1.1
*/
public class SingletonManager : MonoBehaviour {


	static Hashtable hash = new Hashtable();
	private static SingletonManager _instance;
	
	void Awake()
	{  
		if (_instance == null) {
			_instance = this;
		}  
	}

	
	void Start(){
		if (hash.ContainsKey(this.name))
			Debug.Log("Warning: There are more than one Player Manager at the level");
		else
			hash[this.name] = this;
	}
	


	
	/**
	 * 获取单例
	 * */
	public static SingletonManager Instance{ 
		get{
			if(_instance==null) { 
				lock (typeof(SingletonManager)) { 
					GameObject gom = GameObject.Find("/_SingletonManager"); 
					if(gom==null){
						gom = new GameObject("_SingletonManager");
						DontDestroyOnLoad(gom);
						if(_instance==null){
							gom.AddComponent<SingletonManager>(); 
						}
					}else{
						_instance = gom.GetComponent<SingletonManager>();
						if(_instance==null){
							gom.AddComponent<SingletonManager>(); 
						}
					} 	 
				}
			}
			return _instance;
		}
	}
	

}
