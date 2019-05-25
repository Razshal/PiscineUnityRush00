using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundWeaponScript : MonoBehaviour {
    public GameObject attachedWeapon;
    public bool hasBeenThrown = false;
    public float throwForce = 1000;
    new private Rigidbody2D rigidbody2D;
    private GameObject spriteContainer;

	private void Start()
	{
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        spriteContainer = transform.GetChild(0).gameObject;
	}

	private void FixedUpdate()
	{
        if (hasBeenThrown)
        {
            rigidbody2D.AddForce(gameObject.transform.up * -throwForce);
            hasBeenThrown = false;
        }
        if (!rigidbody2D.velocity.Equals(Vector2.zero))
        {
			rigidbody2D.velocity *= 0.8f;
            spriteContainer.transform.Rotate(new Vector3(0, 0, 10 * rigidbody2D.velocity.y));
        }
	}
}
