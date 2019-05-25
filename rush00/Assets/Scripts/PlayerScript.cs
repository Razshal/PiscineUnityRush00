using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : LivingBeing
{
    private WeaponScript weaponScript;

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
    }

    private void ThrowWeapon()
    {
        Instantiate(weaponScript.groundWeapon,
                    attachedWeapon.transform.position,
                    attachedWeapon.transform.rotation)
            .GetComponent<GroundWeaponScript>()
            .hasBeenThrown = true;
        Destroy(attachedWeapon);
        attachedWeapon = null;
        weaponScript = null;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Pickup Weapon
        if (Input.GetKeyDown(KeyCode.E) && !attachedWeapon && other.gameObject.tag == "Weapon")
            PickupWeapon(other.gameObject);
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
        // Call parent fixed update
        base.FixedUpdate();

        // Player input
        movement = new Vector3(Input.GetAxis("Horizontal") * movementSpeed,
                               Input.GetAxis("Vertical") * movementSpeed,
                               0);
        gameObject.transform.Translate(movement);
    }

    new void Update()
    {
        // Call parent update
        base.Update();

        // Look at the mouse direction
        RotateToPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        // Attack !
        if (Input.GetMouseButtonDown(0) && attachedWeapon)
            weaponScript.Attack();

        // Throw weapon
        if (Input.GetMouseButtonDown(1) && attachedWeapon)
            ThrowWeapon();
    }
}
