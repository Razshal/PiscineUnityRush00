using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    public float speed;
    private bool hasBeenShootedByPlayer = false;
    private GameObject shooter;

    public void InitBullet(bool _hasBeenShootedByPlayer, GameObject _shooter)
    {
        hasBeenShootedByPlayer = _hasBeenShootedByPlayer;
        shooter = _shooter;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Player" && !hasBeenShootedByPlayer)
            || (collision.gameObject.tag == "Enemy" && hasBeenShootedByPlayer))
        {
            collision.gameObject.GetComponent<LivingBeing>().Die();
        }
        if (!collision.gameObject == shooter)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        gameObject.transform.Translate(Vector3.down * speed);
    }
}
