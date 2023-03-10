using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public static class Extensions {

	//gameobject extensions
	public static bool IsSubClassOf<T>(this GameObject o){
		MonoBehaviour[] list = o.GetComponents<MonoBehaviour>();
		foreach(MonoBehaviour mb in list)
			if (mb is T) return true;
		return false;
	}

	public static bool IsEmptyOrNull(this object[] list){
		return (list==null || list.Length == 0);
	}

	public static GameObject CloestToObject(this GameObject obj, object[] list, bool liveOnly=true){
		if (list.IsEmptyOrNull()) return null;
		List<GameObject> l = new List<GameObject>();
		for(var i = 0;i<list.Length;i++){
			var o = list[i];
			if ((o is BaseObj) && (null!=o) && (liveOnly && (o as Liveable).status == Liveable.StatusType.Live || !liveOnly))
				l.Add( ((BaseObj)o).gameObject);
		}
		return obj.CloestToObject(l);
	}

	public static GameObject CloestToObject(this GameObject obj, List<GameObject> list){
		if (list.Count == 0) return null;
		float minDistance = 100000f;
		GameObject closest = null;
		foreach(var entity in list){
			var distance = Vector3.Distance(entity.transform.position, obj.transform.position);
			if (distance < minDistance){ minDistance = distance; closest = entity;}
		}
		return closest;
	}

	public static void LookAt2d(this Transform thisTransform, Transform lookAt){
		LookAt2d(thisTransform,lookAt,-90);
	}

	public static void LookAt2d(this Transform thisTransform, Transform lookAt, float rotate){
		var dir = Camera.main.ScreenToWorldPoint(lookAt.position) - Camera.main.ScreenToWorldPoint(thisTransform.position);
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - rotate;
		thisTransform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, angle));
	}

	public static T FindInParents<T>(this GameObject go) where T : Component
	{
		if (go == null) return null;
		var comp = go.GetComponent<T>();
		if (comp != null)
			return comp;
		
		Transform t = go.transform.parent;
		while (t != null && comp == null)
		{
			comp = t.gameObject.GetComponent<T>();
			t = t.parent;
		}
		return comp;
	}

	public static float to_f(this int v){
		return (float)v;
	}

	public static List<Transform> childrenOnly(this GameObject go){
		return go.transform.Cast<Transform>().ToList();
	}

	public static T[] Join2<T>(this T[] a, T[] b) where T : BaseObj{
		T[] results = new T[a.Length + b.Length];
		Array.Copy(a,results,a.Length);
		Array.Copy(b, 0,results, a.Length,b.Length);
		return results;
	}

	public static object[] Join(this Array o, params object[] list){
		object[] results = new object[o.Length + list.Length];
		Array.Copy(o,results,o.Length);
		Array.Copy(list,0, results, o.Length,list.Length);
		return results;
	}

	public static float AngelBetween(Vector3 a, Vector3 b){
		Vector3 canonWorldPos = a; // Camera.main.WorldToScreenPoint (a);
		b.x = b.x - canonWorldPos.x;
		b.y = b.y - canonWorldPos.y;
		var angle = Mathf.Atan2 (b.y, b.x) * Mathf.Rad2Deg;
		if (angle < 0) angle+=360;
		return angle;
	}

	public static Transform SearchByName(this Transform target, string name)
	{
		if (target.name == name) return target;
		for (int i = 0; i < target.childCount; ++i)
		{
			var result = SearchByName(target.GetChild(i), name);
			if (result != null) return result;
		}
		return null;
	}
}
