using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float movementSpeed = 0.2f;
    private Vector3 relativeTarget;
    private float rotationRadians;
    private GameObject bodyContainer;
    public enum State
    {
        STAY,
        MOVING,
        ATTACKING
    }
    public State state = State.STAY;

    // Use this for initialization
    void Start()
    {
        bodyContainer = gameObject.transform.GetChild(0).gameObject;
    }

    void FixedUpdate()
    {
        gameObject.transform.Translate(
            new Vector3(Input.GetAxis("Horizontal") * movementSpeed,
                        Input.GetAxis("Vertical") * movementSpeed,
                        0)
        );
    }

    // Update is called once per frame
    void Update()
    {
        // Rotation caclulation
        relativeTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotationRadians = Mathf.Atan2(relativeTarget.y, relativeTarget.x) * Mathf.Rad2Deg - 90;
        bodyContainer.transform.rotation = Quaternion.Euler(0f, 0f, rotationRadians);

        if (!gameObject.GetComponent<Rigidbody2D>().velocity.Equals(Vector2.zero))
            state = State.MOVING;
    }
}
