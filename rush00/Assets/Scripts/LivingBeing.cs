using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingBeing : MonoBehaviour {

	public GameObject attachedWeapon;
    public GameObject looseMenu;
	protected GameObject weaponContainer;
	protected GameObject bodyContainer;
    public Animator legs;
    protected AudioSource audioSource;
    public AudioClip deathSound;
    protected GameObject player;
    protected Vector3 movement;
	public bool alive = true;
	public float movementSpeed = 0.2f;
    protected float rotationRadians;
    public static int enemyCount = 0;
    new public Rigidbody2D rigidbody2D;
    public bool deathAnimation;
    public float throwForce = 1000;

    public enum State
    {
        STAY,
        MOVING
    }
    public State state = State.STAY;

    protected void PlaySound(AudioClip clip)
    {
        if (audioSource)
        {
			audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void Die()
    {
        if (alive)
        {
            alive = false;
            deathAnimation = true;
            PlaySound(deathSound);
            if (gameObject.CompareTag("Player"))
            {
                GameObject instantiatedMenu;

                instantiatedMenu = Instantiate(looseMenu);
                instantiatedMenu.GetComponent<Canvas>().worldCamera = Camera.main;
                instantiatedMenu.SetActive(true);
                GameObject.Find("HUD").SetActive(false);
            }
            else 
            {
                enemyCount--;
                player.GetComponent<PlayerScript>().DeclareEnemyDeath(gameObject);
                Destroy(gameObject, 5);
            }

            if (enemyCount <= 0)
                PlayerScript.Player().GetComponent<PlayerScript>().Win();
        }
    }

    // Rotation caclulation (look at this pos trough Z axis)
    public void RotateToPos(Vector3 pos)
    {
        pos -= transform.position;
        rotationRadians = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg - 90;
        bodyContainer.transform.rotation = Quaternion.Euler(0f, 0f, rotationRadians);
    }

    protected void Start()
    {
        player = PlayerScript.Player();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        bodyContainer = gameObject.transform.GetChild(0).gameObject;
        weaponContainer = bodyContainer.transform.Find("WeaponContainer").gameObject;
        audioSource = gameObject.GetComponent<AudioSource>();
        if (gameObject.CompareTag("Enemy"))
            enemyCount++;
    }

    protected void FixedUpdate()
    {
        // Define if entity is moving by his translation vector
        if (!movement.Equals(Vector3.zero))
            state = State.MOVING;
        else
            state = State.STAY;

        if (deathAnimation)
        {
            rigidbody2D.AddForce(gameObject.transform.up * -throwForce);
            deathAnimation = false;
        }

        if (!alive && !rigidbody2D.velocity.Equals(Vector2.zero))
        {
            rigidbody2D.velocity *= 0.8f;
            bodyContainer.transform.Rotate(new Vector3(0, 0, 10 * rigidbody2D.velocity.y));
        }
    }

    protected void Update()
    {
        // Animation control
        legs.SetBool("isMoving", state == State.MOVING);
    }
}
