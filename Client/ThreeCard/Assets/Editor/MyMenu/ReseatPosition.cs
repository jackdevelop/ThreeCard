﻿using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;


/**
 * Unity3D研究院之自动计算所有包围盒的中心点
 * 
 * */
public static class ReseatPosition 
{
	[MenuItem ("MyMenu/ReseatPosition")]
	static void onReseatPosition () 
	{
		Transform parent = 	Selection.activeGameObject.transform;
		Vector3 postion = parent.position;
		Quaternion rotation = parent.rotation;
		Vector3 scale = parent.localScale;
		parent.position = Vector3.zero;
		parent.rotation = Quaternion.Euler(Vector3.zero);
		parent.localScale = Vector3.one;
		
		
		Vector3 center = Vector3.zero;
		Renderer[] renders = parent.GetComponentsInChildren<Renderer>();
		foreach (Renderer child in renders){
			center += child.bounds.center;   
		}
		center /= parent.GetComponentsInChildren<Transform>().Length; 
		Bounds bounds = new Bounds(center,Vector3.zero);
		foreach (Renderer child in renders){
			bounds.Encapsulate(child.bounds);   
		}
		
		parent.position = postion;
		parent.rotation = rotation;
		parent.localScale = scale;
		
		foreach(Transform t in parent){
			t.position = t.position -  bounds.center;
		}
		parent.transform.position = bounds.center + parent.position;
		
	}
}