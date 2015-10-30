using UnityEngine;  
using UnityEngine.UI;  
using UnityEngine.EventSystems;  


enum UIButtonEffectType {
	None,
	Light,
	Dark,
	Gray
}
/**
 * UIButton
 * 自定义拓展的按钮 
* 
* 	在这里修改shader可以让按钮的高亮和变暗达到比较好的效果，美术感觉还行并且也只用一张图，效果达到了，资源量也省了。
	在这里只封装了高亮和变暗以及设置灰色的效果，在一个大项目中这些还是不够的，比如需要按钮在点击时候做偏移，
	在变灰的时候还要能接受事件等。不过原理都一样，熟悉一下都比较好扩展。
	还有扩展之后这样使用：
	
        UIButton.Get(按钮对象).SetText("按钮显示文字");
        UIButton.Get(按钮对象).SetGray(true);
        
* @author  jack   jackdevelop@sina.com
* @date  2015-10-26 上午10:55
* @version 1.0
*/
public class UIButton : MonoBehaviour,  
IPointerDownHandler,  
IPointerUpHandler,  
IPointerEnterHandler,  
IPointerExitHandler 
{  
	public Image m_image;  
	public Text m_text;  
	
	private GameObject m_obj;  
	private bool m_gray = false;// 默认可用，变灰不可以再变色  
	private bool m_init = false;  


	public void Start()  {  
		m_init = true;  
		m_obj = gameObject; 

		if (null == m_image)  
			m_image = GetComponent<Image>();  
		if (null == m_text)  
			m_text = GetComponentInChildren<Text>();  
	}  



	/**
	 *  设置按钮文本
	 * */
	public void SetText(string text)  
	{  
		m_text.text = text;  
	}  

	/**
	 *  设置按钮变灰  
	 * */
	public void SetGray(bool bGray)  
	{  
		m_gray = bGray;  
		if (bGray)  
			SetShader(UIButtonEffectType.Gray);  
		else  
			SetShader(UIButtonEffectType.None);  
	}  




	public void OnPointerDown(PointerEventData eventData)  
	{  
		SetShader(UIButtonEffectType.Dark);  
	}  
	public void OnPointerUp(PointerEventData eventData)  
	{  
		SetShader(UIButtonEffectType.None);  
	}  
	public void OnPointerEnter(PointerEventData eventData)  
	{  
		SetShader(UIButtonEffectType.Light);  
	}  
	public void OnPointerExit(PointerEventData eventData)  
	{  
		SetShader(UIButtonEffectType.None);  
	}  


	private void SetShader(UIButtonEffectType type){
	//public void SetShader(UIButtonEffectType type)  {  
		if (m_image.material == null)  
			return;  
		if (m_gray && type != UIButtonEffectType.Gray)  
			return;  
		
		if (m_image.material.shader == Shader.Find("UI/Default"))  
			m_image.material = new Material(Shader.Find("UI/Button"));  

		//if (m_image.material.shader == Shader.Find("Custom/UIButton"))  
		//	m_image.material = new Material(Shader.Find("Custom/UIButton"));  
		m_image.material.SetInt("_type", (int)type);  
	}  




	/**
	 * 静态方法 
	 * 获取按钮的 UIButton  属性  
	 * 
	 * */
	public static UIButton Get(GameObject go)  
	{  
		UIButton load = go.GetComponent<UIButton>();  
		if (load == null) load = go.AddComponent<UIButton>();  
		if(!load.m_init) load.Start();  

		return load;  
	}  
}  