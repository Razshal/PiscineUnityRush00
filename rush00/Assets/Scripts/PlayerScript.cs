using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : LivingBeing
{
    public float movementSpeed = 0.2f;

    public enum State
    {
        STAY,
        MOVING
    }
    public State state = State.STAY;

    private void PickupWeapon(GameObject groundWeapon)
    {
        weapon = Instantiate(groundWeapon.GetComponent<GroundWeaponScript>().playerWeapon);
        weapon.transform.parent = weaponContainer.transform;
        weapon.transform.position = weaponContainer.transform.position;
        weapon.transform.rotation = weaponContainer.transform.rotation;
        weapon.GetComponent<WeaponScript>().isOwnedByPlayer = true;
        Destroy(groundWeapon);
    }

    private void OnTriggerStay2D(Collider2D other)
	{
        if (Input.GetKeyDown(KeyCode.E) && !weapon && other.gameObject.tag == "Weapon")
            PickupWeapon(other.gameObject);
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
            weapon.GetComponent<WeaponScript>().Attack();
    }
}
