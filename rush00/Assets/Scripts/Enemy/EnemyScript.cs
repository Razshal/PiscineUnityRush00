using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : LivingBeing {
	public EnemySight Sight;
	private GameObject Player;
	private float distance = 0;

	new void Start() {
		base.Start();
		Player = GameObject.Find("Player");
	}

	new void Update() {
		base.Update();
		if (Sight.PlayerDetected) {
			RotateToPos(Player.transform.position);
			distance = Vector2.Distance(transform.position, Player.transform.position);
			transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 5 * Time.deltaTime);
		}
	}
}
