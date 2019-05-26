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
                player.GetComponent<PlayerScript>()
                      .weaponScript
                      .listeningEnemies
                      .Remove(gameObject);
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
    }

    protected void Update()
    {
        // Animation control
        legs.SetBool("isMoving", state == State.MOVING);
    }
}
