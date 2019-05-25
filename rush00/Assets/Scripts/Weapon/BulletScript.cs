using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    public float speed;
    public bool hasBeenShootedByPlayer = false;

	private void OnColliderEnter2D(Collider2D collision)
	{
        if ((collision.gameObject.tag == "Player" && !hasBeenShootedByPlayer)
            || (collision.gameObject.tag == "Enemy" && hasBeenShootedByPlayer))
            collision.gameObject.GetComponent<LivingBeing>().Die();
        Destroy(gameObject);
	}

	private void FixedUpdate()
	{
        gameObject.transform.Translate(Vector3.down * speed);
	}
}
