using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {
	public bool PlayerDetected = false;
	private float delay = -1;
	private float TimeStart = 0;

	void Update() {
		if (PlayerDetected && delay == -1) {
			delay = 3;
			TimeStart = Time.timeSinceLevelLoad;
		}
		if (delay != -1 && Time.timeSinceLevelLoad - TimeStart > delay) {
			delay = -1;
			PlayerDetected = false;
		}
	}

	void OnTriggerStay2D(Collider2D col)
    {
		if (col.tag == "Player") {
			int layerMask = ~(LayerMask.GetMask("Enemy"));
			Vector2 dir = new Vector2(col.gameObject.transform.position.x - transform.position.x, col.gameObject.transform.position.y - transform.position.y);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 7, layerMask);
			if (hit && hit.transform.gameObject.tag == "Player") {
				PlayerDetected = true;
				TimeStart = Time.timeSinceLevelLoad;
			}
		}
    }
}
