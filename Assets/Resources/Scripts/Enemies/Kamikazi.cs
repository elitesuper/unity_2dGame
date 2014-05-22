﻿using UnityEngine;
using System.Collections;

public class Kamikazi : Enemy {

	protected new void Start(){
		base.Start();
		findTarget();
		OnHit += (o) => {
		};
	}
	 
	protected new void Update(){
		if (Target != null){
			transform.LookAt2d(Target.transform);
			transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, Speed*Time.deltaTime);
		}
	}

	void findTarget() {
		Target = gameObject.CloestToObject(Game.Canons.ToArray());
		if (Target == null) Invoke("findTarget",1);
	}
	
}