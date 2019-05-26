using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {
    public GameObject bullet;
    public GameObject groundWeapon;
    private GameObject lastShootedBullet;
    private GameObject collidingEnemy;
    public List<GameObject> listeningEnemies = new List<GameObject>();


    public bool isUnlimited = false;
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
        if (coolDown <= 0 || isMeleeWeapon)
        {
            audioSource.clip = attackSound;
            coolDown = fireRate;

            if (!isMeleeWeapon && (ammoNumber > 0 || isUnlimited))
            {
                // Create Bullets
                lastShootedBullet = Instantiate(bullet,
                                                gameObject.transform.position,
                                                gameObject.transform.rotation);
                lastShootedBullet.GetComponent<BulletScript>().InitBullet(LayerName());

                // Decrase weapon
                if (!isUnlimited)
                    ammoNumber--;

                // Pow !
				audioSource.Play();

                // Warn Enemies in a radius
                if (listeningEnemies.Count > 0)
                {
                    foreach (GameObject enemy in listeningEnemies)
                        enemy.GetComponent<EnemyScript>().Sight.PlayerDetected = true;
                }

            }

            if (isMeleeWeapon && canTouchEnemy)
            {
                gameObject.layer = LayerMask.NameToLayer(LayerName());
                collidingEnemy.GetComponent<LivingBeing>().Die();
            }

            if (isMeleeWeapon)
                audioSource.Play();

        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (!isMeleeWeapon && collision.gameObject.CompareTag("Enemy"))
        {
            listeningEnemies.Add(collision.gameObject);
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
        if (!isMeleeWeapon && other.gameObject.CompareTag("Enemy"))
        {
            listeningEnemies.Remove(other.gameObject);
        }
    }

    private void Update()
    {
        if (coolDown > 0)
            coolDown -= Time.deltaTime;
    }
}
