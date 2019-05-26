using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : LivingBeing {
	public EnemySight Sight;
	private GameObject Player;
	private float distance = 0;
    private WeaponScript weaponScript;

	new void Start() {
		base.Start();
		Player = GameObject.Find("Player");
        weaponScript = attachedWeapon.GetComponent<WeaponScript>();
	}

	new void Update() {
		base.Update();
		if (Sight.PlayerDetected) {
			RotateToPos(Player.transform.position);
			int layerMask = ~(LayerMask.GetMask("Enemy"));
			Vector2 dir = new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 7, layerMask);
			if (hit && hit.transform.gameObject.tag == "Player")
				weaponScript.Attack();
			else
				transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 5 * Time.deltaTime);
		}
	}
}
