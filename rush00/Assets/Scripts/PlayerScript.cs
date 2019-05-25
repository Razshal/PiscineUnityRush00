using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float movementSpeed = 0.2f;
    public bool alive = true;
    public GameObject weapon;
    private GameObject weaponContainer;
	private GameObject bodyContainer;
    public Animator legs;
    private Vector3 relativeTarget;
    private Vector3 movement;
    private float rotationRadians;

    public enum State
    {
        STAY,
        MOVING
    }
    public State state = State.STAY;

    private void OnTriggerStay2D(Collider2D other)
	{
        if (Input.GetKeyDown(KeyCode.E) && !weapon && other.gameObject.tag == "Weapon")
        {
            Debug.Log("Weapon + keycode");
            weapon = Instantiate(other.gameObject.GetComponent<WeaponScript>().playerWeapon);
			weapon.transform.parent = weaponContainer.transform;
            weapon.transform.position = weaponContainer.transform.position;
            weapon.transform.rotation = weaponContainer.transform.rotation;
        }
	}

	void Start()
    {
        bodyContainer = gameObject.transform.GetChild(0).gameObject;
        weaponContainer = bodyContainer.transform.Find("WeaponContainer").gameObject;
    }

    void FixedUpdate()
    {
        movement = new Vector3(Input.GetAxis("Horizontal") * movementSpeed,
                               Input.GetAxis("Vertical") * movementSpeed,
                               0);
        gameObject.transform.Translate(movement);

        // Define if player is moving by his translation vector
        if (!movement.Equals(Vector3.zero))
            state = State.MOVING;
        else
            state = State.STAY;
        // Start appropriate animation
    }

    void Update()
    {
        // Rotation caclulation
        relativeTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotationRadians = Mathf.Atan2(relativeTarget.y, relativeTarget.x) * Mathf.Rad2Deg - 90;
        bodyContainer.transform.rotation = Quaternion.Euler(0f, 0f, rotationRadians);

        // Animation control
        legs.SetBool("isMoving", state == State.MOVING);

        if (Input.GetMouseButtonDown(0) && weapon)
        {
            Debug.Log("boom");
        }


    }
}
