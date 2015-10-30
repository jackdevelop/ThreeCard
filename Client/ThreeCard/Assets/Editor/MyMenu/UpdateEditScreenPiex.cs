using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

//public class UpdateEditScreenPiex : MonoBehaviour {
public class UpdateEditScreenPiex :Editor
{
	[MenuItem ("MyMenu/UpdateEditScreenPiex")]
	static void UpdateEditScreenPiex_()
	{

		//Screen.SetResolution (960,640,false);

		//var x = Handles.GetMainGameViewSize ();
		//Debug.Log (x.ToString());

		//Handles.color  = Color.red;
		EditorGUIUtility.RenderGameViewCameras (new Rect(0f,0f,960f,640f),true,true);

		GameObject []gos = (GameObject[]) GameObject.FindObjectsOfType(typeof(GameObject));
		foreach (GameObject go  in gos) {

		
		}
	}
}
