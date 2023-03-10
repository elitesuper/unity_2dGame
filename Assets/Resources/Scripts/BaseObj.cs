using UnityEngine;
using System.Collections;

public abstract class BaseObj : MonoBehaviour {
		
	public void l(object o){
		Debug.Log(o);
	}
	public void e(object o){
		Debug.LogError(o);
	}
	public static GameController Game;
	protected static Config configs;
	
	protected void Awake () {
		if (Game == null) {
			Game = GameController.Instance;
			configs = Config.GetConfigs();
		}
	}


	protected void Start(){}
	protected void Update(){}
		

}
