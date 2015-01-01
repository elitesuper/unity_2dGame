﻿using UnityEngine;
using System.Collections;

public class CanonDestroyer : Enemy {

	// Use this for initialization
	new void Start () {
		base.Start();
		speed = .6f;
		health = 70;
		killBonusMinerals = new int[]{3,7,12};

		findTarget();
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update();
		if (target != null){
			l (transform.position.x);
			transform.LookAt2d(target.transform,(transform.position.x > target.transform.position.x) ? 180 :  0);//transform.rotation.z > 180 ? 0 : 180
			//transform.Rotate(Vector3.forward, ((transform.rotation.z > 180) ? 90 : -90) );
			//transform.Rotate(0,0,90);
			transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed*Time.deltaTime);
		} else Invoke("findTarget",1);
	}

	void findTarget() {
		target = gameObject.CloestToObject(Game.autoCanons.ToArray());
		if (target == null) Invoke("findTarget",1);
	}
}
