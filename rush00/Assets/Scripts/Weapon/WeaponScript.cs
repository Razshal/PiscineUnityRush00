using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {
    public GameObject bullet;
    public GameObject groundWeapon;
    private GameObject lastShootedBullet;
    public bool isMeleeWeapon = false;
    public int ammoNumber = 1;
    public float fireRate = 0.5f;
    public string displayName = "Weapon";
    public bool isOwnedByPlayer = false;
    private float coolDown;
    public bool canTouchEnemy = false;
    public GameObject collidingEnemy;

    private string LayerName()
    {
        return isOwnedByPlayer ? "Player" : "Enemy";
    }

    public void Attack()
    {
        if (coolDown <= 0)
        {
            if (!isMeleeWeapon && ammoNumber > 0)
            {
                lastShootedBullet = Instantiate(bullet,
                                                gameObject.transform.position,
                                                gameObject.transform.rotation);
                lastShootedBullet.GetComponent<BulletScript>()
                                 .InitBullet(LayerName());
                ammoNumber--;
                coolDown = fireRate;
            }
            if (isMeleeWeapon && canTouchEnemy)
            {
                gameObject.layer = LayerMask.NameToLayer(LayerName());
                collidingEnemy.GetComponent<LivingBeing>().Die();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            canTouchEnemy = true;
            collidingEnemy = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canTouchEnemy = false;
        collidingEnemy = null;
    }

    private void Update()
    {
        if (coolDown > 0)
            coolDown -= Time.deltaTime;
    }
}
