//using UnityEngine;
//using System.Collections;
//using UnityEditor;
//using System.Collections.Generic;
//using System.IO;
//
///**
// * 
// * Unity3D研究院编辑器之不实例化Prefab获取删除更新组件
// * 
// * */
//public static class Delete //: MonoBehaviour
//{
//
//	[MenuItem("MyMenu/Delete")]
//	static void delete () 
//	{
//
//		//GameObject prefab = Resources.LoadAssetAtPath<GameObject>("Assets/GameObject.prefab");
//		//GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GameObject.prefab");
//		GameObject prefab = AssetDatabase.LoadMainAssetAtPath("Assets/GameObject.prefab");
//		//然后获取你想要的组件后，直接用DestroyImmediate(com,true),对，此处也是传true... 然后记得可以设置一下EditorUtility.SetDirty(o);即可
//
//
//
//		//删除MeshCollider
//		MeshCollider [] meshColliders = prefab.GetComponentsInChildren<MeshCollider>(true);
//		foreach(MeshCollider meshCollider in meshColliders){
//			
//			GameObject.DestroyImmediate(meshCollider,true);
//		}
//		
//		//删除空的Animation组件
//		Animation [] animations = prefab.GetComponentsInChildren<Animation>(true);
//		foreach(Animation animation in animations){
//			if(	animation.clip == null){
//				GameObject.DestroyImmediate(animation,true);
//			}
//			
//		}
//		
//		//删除missing的脚本组件
//		MonoBehaviour [] monoBehaviours = prefab.GetComponentsInChildren<MonoBehaviour>(true);
//		foreach(MonoBehaviour monoBehaviour in monoBehaviours){
//			
//			
//			if(monoBehaviour == null){
//				Debug.Log("有个missing的脚本");
//				//GameObject.DestroyImmediate(monoBehaviour,true);
//				
//			}
//		}
//		
//		//遍历Transform的名子， 并且给某个游戏对象添加一个脚本
//		Transform [] transforms = prefab.GetComponentsInChildren<Transform>(true);
//		foreach(Transform transfomr in transforms){
//			if(transfomr.name == "GameObject (1)"){
//				Debug.Log(transfomr.parent.name);
//				transfomr.gameObject.AddComponent<BoxCollider>();
//				return;
//			}
//			
//		}
//		//遍历Transform的名子， 删除某个GameObject节点
//		foreach(Transform transfomr in transforms){
//			if(transfomr.name == "GameObject (2)"){
//				GameObject.DestroyImmediate(transfomr.gameObject,true);
//				return;
//			}
//			
//		}
//		EditorUtility.SetDirty(prefab);
//	}
//}
//
