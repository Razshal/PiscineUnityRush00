using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : LivingBeing
{
    private WeaponScript weaponScript;
    private GameObject lastCollidedWeapon;
    public AudioClip pickupWeaponSound;
    public GameObject winMenu;


    public static GameObject Player()
    {
        return GameObject.FindWithTag("Player");
    }

    private void PickupWeapon(GameObject groundWeapon)
    {
        attachedWeapon = Instantiate(
            groundWeapon.GetComponent<GroundWeaponScript>().attachedWeapon);
        attachedWeapon.transform.parent = weaponContainer.transform;
        attachedWeapon.transform.position = weaponContainer.transform.position;
        attachedWeapon.transform.rotation = weaponContainer.transform.rotation;
        weaponScript = attachedWeapon.GetComponent<WeaponScript>();
        weaponScript.isOwnedByPlayer = true;
        Destroy(groundWeapon);
        PlaySound(pickupWeaponSound);
    }

    private void ThrowWeapon()
    {
        GameObject tempweapon = Instantiate(weaponScript.groundWeapon,
                    attachedWeapon.transform.position,
                                             attachedWeapon.transform.rotation);
        tempweapon.GetComponent<GroundWeaponScript>()
            .hasBeenThrown = true;
        Destroy(attachedWeapon);
        attachedWeapon = null;
        weaponScript = null;
    }

    private bool IsPickableWeapon(GameObject obj)
    {
        return !attachedWeapon && obj.tag == "Weapon";
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (IsPickableWeapon(collision.gameObject))
            lastCollidedWeapon = collision.gameObject;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (IsPickableWeapon(collision.gameObject) 
            && collision.gameObject == lastCollidedWeapon)
            lastCollidedWeapon = null;
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Pickup Weapon
        if (IsPickableWeapon(collision.gameObject))
            lastCollidedWeapon = collision.gameObject;
    }

    public void Win()
    {
        if (alive)
        {
            GameObject instantiatedMenu;

            instantiatedMenu = Instantiate(winMenu);
            instantiatedMenu.GetComponent<Canvas>().worldCamera = Camera.main;
            instantiatedMenu.SetActive(true);
            GameObject.Find("HUD").SetActive(false);
        }
    }

	new void Start()
    {
        // Call parent start
        base.Start();

        if (attachedWeapon)
            weaponScript = attachedWeapon.GetComponent<WeaponScript>();
    }

    new void FixedUpdate()
    {
        if (alive)
        {
            // Call parent fixed update
            base.FixedUpdate();

            // Player input
            movement = new Vector3(Input.GetAxis("Horizontal") * movementSpeed,
                                   Input.GetAxis("Vertical") * movementSpeed,
                                   0);
            gameObject.transform.Translate(movement);
        }
    }

    new void Update()
    {
        if (alive)
        {
            // Call parent update
            base.Update();

            // Look at the mouse direction
            RotateToPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            // Attack !
            if (Input.GetMouseButton(0) && attachedWeapon)
                weaponScript.Attack();

            // Throw weapon
            if (Input.GetMouseButtonDown(1) && attachedWeapon)
                ThrowWeapon();

            // Pickup weapon
            if (Input.GetKeyDown(KeyCode.E) && lastCollidedWeapon)
                PickupWeapon(lastCollidedWeapon);
        }
    }
}
