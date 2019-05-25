using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float movementSpeed = 0.2f;
    public bool alive = true;
    public GameObject weapon;
    public Animator legs;
	private GameObject bodyContainer;
    private Vector3 relativeTarget;
    private Vector3 movement;
    private float rotationRadians;

    public enum State
    {
        STAY,
        MOVING
    }
    public State state = State.STAY;

    void Start()
    {
        bodyContainer = gameObject.transform.GetChild(0).gameObject;
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
        legs.SetBool("isMoving", state == State.MOVING);
    }

    void Update()
    {
        // Rotation caclulation
        relativeTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotationRadians = Mathf.Atan2(relativeTarget.y, relativeTarget.x) * Mathf.Rad2Deg - 90;
        bodyContainer.transform.rotation = Quaternion.Euler(0f, 0f, rotationRadians);

        if (Input.GetMouseButtonDown(0) && weapon)
        {
            Debug.Log("boom");
        }
    }
}
