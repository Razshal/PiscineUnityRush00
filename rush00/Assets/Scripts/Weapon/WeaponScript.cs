using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {
    public GameObject bullet;
    public GameObject groundWeapon;
    private GameObject lastShootedBullet;
	private GameObject collidingEnemy;

    public bool isMeleeWeapon = false;
	public bool isOwnedByPlayer = false;
	public bool canTouchEnemy = false;
    public int ammoNumber = 1;
    public float fireRate = 0.5f;
    public string displayName = "Weapon";
    private AudioSource audioSource;
    public AudioClip attackSound;
    private float coolDown;

	private void Start()
	{
        audioSource = gameObject.GetComponent<AudioSource>();
	}

	private string LayerName()
    {
        return isOwnedByPlayer ? "Player" : "Enemy";
    }

    public void Attack()
    {
        if (coolDown <= 0)
        {
            audioSource.clip = attackSound;

            if (!isMeleeWeapon && ammoNumber > 0)
            {
                lastShootedBullet = Instantiate(bullet,
                                                gameObject.transform.position,
                                                gameObject.transform.rotation);
                lastShootedBullet.GetComponent<BulletScript>()
                                 .InitBullet(LayerName());
                ammoNumber--;
                audioSource.Play();
            }

            if (isMeleeWeapon && canTouchEnemy)
            {
                gameObject.layer = LayerMask.NameToLayer(LayerName());
                collidingEnemy.GetComponent<LivingBeing>().Die();
                audioSource.Play();
            }
            coolDown = fireRate;
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
