using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : LivingBeing {
	public EnemySight Sight;
	private GameObject Player;
	private float distance = 0;
    private WeaponScript weaponScript;
    private GameObject[] checkpoints;
    private GameObject ActualPath;

	new void Start() {
		base.Start();
		Player = GameObject.Find("Player");
        weaponScript = attachedWeapon.GetComponent<WeaponScript>();
		checkpoints = GameObject.FindGameObjectsWithTag("CheckPoint");
	}

	void FindPath() {
		List<GameObject> MaybePath = new List<GameObject>();
		GameObject closest = null;
		float dist = Mathf.Infinity;

		foreach(GameObject checkpoint in checkpoints) {
			Vector2 dir = new Vector2(checkpoint.transform.position.x - transform.position.x, checkpoint.transform.position.y - transform.position.y);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 30, ~(LayerMask.GetMask("Enemy", "Door")));
			if (hit && hit.transform.gameObject == checkpoint)
				MaybePath.Add(checkpoint);
		}

		foreach(GameObject Path in MaybePath) {
			float diff = Vector3.Distance(Path.transform.position, Player.transform.position);
			if (diff < dist) {
				closest = Path;
				dist = diff;
			}
		}
		Debug.Log(closest);
		ActualPath = closest;
	}

	new void Update() {
		base.Update();
		if (Sight.PlayerDetected) {
			RotateToPos(Player.transform.position);
			Vector2 dir = new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 7, ~(LayerMask.GetMask("Enemy")));
			if (hit && hit.transform.gameObject.tag == "Player") {
				//weaponScript.Attack();
				Debug.Log("Attack");
			}
			else {
				FindPath();
				if (ActualPath)
					transform.position = Vector3.MoveTowards(transform.position, ActualPath.transform.position, 5 * Time.deltaTime);
			}

		}
	}
}
