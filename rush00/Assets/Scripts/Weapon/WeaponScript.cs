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
    private float coolDown;

	private void Update()
	{
        if (coolDown > 0)
            coolDown -= Time.deltaTime;
	}

	private void Start()
	{
        
	}

	public void Attack(bool hasBeenShootedByPlayer)
    {
        if (coolDown <= 0)
        {
            if (!isMeleeWeapon && ammoNumber > 0)
            {
                lastShootedBullet = Instantiate(bullet,
                                    gameObject.transform.position,
                                    gameObject.transform.rotation);
                lastShootedBullet.GetComponent<BulletScript>().hasBeenShootedByPlayer
                                 = hasBeenShootedByPlayer;
                ammoNumber--;
                coolDown = fireRate;
            }
        }
    }
}
