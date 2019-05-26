using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : LivingBeing {
	public EnemySight Sight;
	public bool	patrol = false;
	private GameObject Player;
	private float distance = 0;
    private WeaponScript weaponScript;
    private GameObject[] checkpoints;
    private GameObject ActualPath;
    private List<Vector3> PatrolCheckpoints = new List<Vector3>();
    private Vector3 NextPatrolCheckpoints;


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
			if (ActualPath && ActualPath != checkpoint)
				ActualPath.SetActive(false);
			Vector2 dir = new Vector2(checkpoint.transform.position.x - transform.position.x, checkpoint.transform.position.y - transform.position.y);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 50, ~(LayerMask.GetMask("Enemy", "Door")));
			if (ActualPath && ActualPath != checkpoint)
				ActualPath.SetActive(true);
			if (hit && hit.transform.gameObject == checkpoint)
				MaybePath.Add(checkpoint);
		}

		foreach(GameObject Path in MaybePath) {
			float diff = Vector2.Distance(Path.transform.position, Player.transform.position);
			if (diff < dist) {
				closest = Path;
				dist = diff;
			}
		}
		ActualPath = closest;
	}

	void FindPatrol() {
		PatrolCheckpoints.Clear();
		foreach(GameObject checkpoint in checkpoints) {
			Vector2 dir = checkpoint.transform.position - transform.position;
			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 50, ~(LayerMask.GetMask("Enemy")));
			if (hit && hit.transform.gameObject == checkpoint) {
				PatrolCheckpoints.Add(checkpoint.transform.position);
				NextPatrolCheckpoints = checkpoint.transform.position;
			}
		}
	}

	void Patrol() {
		if (PatrolCheckpoints.Count == 0)
			FindPatrol();
		RotateToPos(NextPatrolCheckpoints);
		if (transform.position != NextPatrolCheckpoints)
			transform.position = Vector3.MoveTowards(transform.position, NextPatrolCheckpoints, 4 * Time.deltaTime);
		else {
			Vector3 tmp = transform.position;
			foreach(Vector3 checkpoint in PatrolCheckpoints)
				if (checkpoint != NextPatrolCheckpoints)
					tmp = checkpoint;
			NextPatrolCheckpoints = tmp;
		}
	}

	new void Update() {
		base.Update();
		if (alive && Sight.PlayerDetected) {
			PatrolCheckpoints.Clear();
			RotateToPos(Player.transform.position);
			distance = Vector3.Distance(Player.transform.position, transform.position);
			Vector2 dir = new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 7, ~(LayerMask.GetMask("Enemy")));
			if (hit && hit.transform.gameObject.tag == "Player") {
				ActualPath = null;
				if (distance < 10)
					weaponScript.Attack();
				else
					transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 5 * Time.deltaTime);
			}
			else {
				if (ActualPath && Vector2.Distance(ActualPath.transform.position, transform.position) != 0)
					transform.position = Vector3.MoveTowards(transform.position, ActualPath.transform.position, 5 * Time.deltaTime);
				else
					FindPath();
			}
		}
		else if (alive && patrol)
			Patrol();
	}
}
