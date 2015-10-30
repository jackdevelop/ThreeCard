using UnityEngine;
using System.Collections;

using System.Xml.Linq;
using System.Xml;

public class test : MonoBehaviour {

	 

	// Use this for initialization
	void Start () {
		StartCoroutine(loadStreamingAssets());
	}
	






	/**
	1:资源读取使用 Resources.Load()。


	是作为一个Unity3D的保留文件夹出现的，也就是如果你新建的文件夹的名字叫Resources，
	那么里面的内容在打包时都会被无条件的打到发布包中。它的特点简单总结一下就是：
	只读，即不能动态修改。所以想要动态更新的资源不要放在这里。
	
	会将文件夹内的资源打包集成到.asset文件里面。因此建议可以放一些Prefab，因为Prefab在打包时会自动过滤掉不需要的资源，有利于减小资源包的大小。
	主线程加载。 会卡 
	**/
	IEnumerator loadResources () {
		//1:Resources 下加载文件 
		string path = "Test";
		Object obj = Resources.Load(path);
		XmlDocument doc = new XmlDocument();

		string str = obj.ToString();
		doc.LoadXml(str);//加载

		Debug.Log("加载完成：" + str);//获取他的内容

		//读取根节点
		//XmlNode root = doc.SelectSingleNode("Test");


		yield return null;
	}




	/**
	//(2): StreamingAssets 下加载文件 
	
	 1: 要说到StreamingAssets，其实和Resources还是蛮像的。同样作为一个只读的Unity3D的保留文件夹出现。
	  不过两者也有很大的区别，那就是Resources文件夹中的内容在打包时会被压缩和加密。
	 2:而StreamingAsset文件夹中的内容则会原封不动的打入包中，因此StreamingAssets主要用来存放一些二进制文件。各位在实际操作中切记不要直接把数据文件放到这个目录中打包
	 3:同样，只读不可写。
	 4:	主要用来存放二进制文件
	 5:	只能用过WWW类来读取。
	**/
	IEnumerator loadStreamingAssets () {
		//2: StreamingAssets 下加载文件 
		//不同平台下StreamingAssets的路径是不同的，这里需要注意一下。
		string PathURL;
		
		#if UNITY_ANDROID   //安卓
			PathURL = "jar:file://" + Application.dataPath + "!/assets/";
		#elif UNITY_IPHONE  //iPhone
			PathURL = Application.dataPath + "/Raw/";
		#elif UNITY_STANDALONE_WIN || UNITY_EDITOR  //windows平台和web平台
			PathURL = "file://" + Application.dataPath + "/StreamingAssets/";
		#else
			string.Empty;
		#endif

		//string path = "file://" + Application.streamingAssetsPath +  "/Test.xml";
		PathURL = PathURL + "/Test.xml";



		WWW www = new WWW(PathURL);
		//在处理网络传输问题时，经常需要处理异步传输，需要等文件下载完毕之后再执行其他任务，一般我们使用回调来解决这个问题
		yield return www;
		string str = www.text;
		Debug.Log("加载完成：" + str);//获取他的内容



		//第二种加载方法  File.ReadAllBytes(localPath)  貌似不行 
	}



	/**
		(3):AssetBundle 
		
		关于AssetBundle的介绍已经有很多了。简而言之就是把prefab或者二进制文件封装成AssetBundle文件（也是一种二进制）。
		但是也有硬伤，就是在移动端无法更新脚本。下面简单的总结下：
		1:是Unity3D定义的一种二进制类型。
		2:最好将prefab封装成AseetBundle，不过上面不是才说了在移动端无法更新脚本吗？
			那从Assetbundle中拿到的Prefab上挂的脚本是不是就无法运行了？也不一定，只要这个prefab上挂的是本地脚本，就可以。
		3:使用WWW类来下载。

	 * */
	IEnumerator loadAssetBundle () {
		AssetBundle AssetBundleCsv = new AssetBundle();
		
		//读取放入StreamingAssets文件夹中的bundle文件
		string str = Application.streamingAssetsPath + "/" + "Test.bundle";
		WWW www = new WWW(str);
		www = WWW.LoadFromCacheOrDownload(str, 0);    
		
		AssetBundleCsv = www.assetBundle;
		string path = "Test";
		TextAsset test = AssetBundleCsv.Load(path, typeof(TextAsset)) as TextAsset;
		string result = test.ToString();

		yield return null;
	}
	



	/**
	(4): PersistentDataPath

	看上去它只是个路径呀，可为什么要把它从路径里面单独拿出来介绍呢？因为它的确蛮特殊的，这个路径下是可读写。
	而且在IOS上就是应用程序的沙盒，但是在Android可以是程序的沙盒，也可以是sdcard。
	并且在Android打包的时候，ProjectSetting页面有一个选项Write Access，可以设置它的路径是沙盒还是sdcard。下面同样简单的总结一下：
	
	1:内容可读写，不过只能运行时才能写入或者读取。提前将数据存入这个路径是不可行的。
	2:无内容限制。你可以从StreamingAsset中读取二进制文件或者从AssetBundle读取文件来写入 PersistentDataPath 中。
	3:写下的文件，可以在电脑上查看。同样也可以清掉。
	 * */
	IEnumerator loadPersistentDataPath () {
		yield return null;
	}


}
