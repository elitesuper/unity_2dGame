using UnityEngine;
using System.Collections;

public class AutoCanon : Building {
	public GameObject bullet;
	GameObject target;
	public float[] shootSpeedPerLevel = new float[]{1, .7f, .3f, .2f};

	protected  new void Start () {
		base.Start();

		mineralCostUpgrade = new int[]{5,10,20,40,80};
		//if (bullet == null) bullet = (Instantiate(Resources.Load("Prefabs/Bullet")) as GameObject);
		InvokeRepeating("shoot",shootSpeedPerLevel[level],shootSpeedPerLevel[level]);
		OnUpgraded += (o)=>{
			CancelInvoke("shoot");
			InvokeRepeating("shoot",shootSpeedPerLevel[level], shootSpeedPerLevel[level]);
		};
	}
	
	protected new void Update () {
		base.Update();

		target = gameObject.CloestToObject(Game.enemies.ToArray());
		if (target != null){
			transform.LookAt2d(target.transform,90);
		}
	}

	void shoot(){
		if (target != null) Instantiate (bullet, transform.position, transform.rotation);
	}

}
