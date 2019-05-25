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
    private bool canTouchEnemy = false;
    private GameObject collidingEnemy;

	private void Update()
	{
        if (coolDown > 0)
            coolDown -= Time.deltaTime;
	}

	private void OnTriggerStay(Collider other)
	{
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            canTouchEnemy = true;
            collidingEnemy = other.gameObject;
        }
	}

	private void OnTriggerExit(Collider other)
	{
        canTouchEnemy = false;
        collidingEnemy = null;
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
                                 .InitBullet(isOwnedByPlayer ? "Player" : "Enemy");
                ammoNumber--;
                coolDown = fireRate;
            }
            if (isMeleeWeapon && canTouchEnemy)
            {
                collidingEnemy.GetComponent<LivingBeing>().Die();
            }
        }
    }
}
