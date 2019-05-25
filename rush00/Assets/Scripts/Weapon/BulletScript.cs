using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    public float speed;

    public void InitBullet(string layer)
    {
        gameObject.layer = LayerMask.NameToLayer(layer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<LivingBeing>().Die();
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        gameObject.transform.Translate(Vector3.down * speed);
    }
}
