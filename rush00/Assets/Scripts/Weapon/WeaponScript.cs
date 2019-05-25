using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {
    public GameObject bullet;
    public GameObject groundWeapon;
    private GameObject lastShootedBullet;
    public bool isMeleeWeapon = false;
    public int ammoNumber = 1;

    public void Attack(bool hasBeenShootedByPlayer)
    {
        lastShootedBullet = Instantiate(bullet,
                                        gameObject.transform.position,
                                        gameObject.transform.rotation);
        lastShootedBullet.GetComponent<BulletScript>().hasBeenShootedByPlayer 
                         = hasBeenShootedByPlayer;
    }
}
