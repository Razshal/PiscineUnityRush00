using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingBeing : MonoBehaviour {

    public bool alive = true;
    public GameObject attachedWeapon;
    public Animator legs;
    protected GameObject weaponContainer;
    protected GameObject bodyContainer;
    protected Vector3 relativeTarget;
    protected Vector3 movement;
    protected float rotationRadians;

    public enum State
    {
        STAY,
        MOVING
    }
    public State state = State.STAY;

    public void Die()
    {
        Debug.Log(name + " dies");
        alive = false;
    }

    protected void Start()
    {
        bodyContainer = gameObject.transform.GetChild(0).gameObject;
        weaponContainer = bodyContainer.transform.Find("WeaponContainer").gameObject;
    }

    protected void FixedUpdate()
    {
        if (!movement.Equals(Vector3.zero))
            state = State.MOVING;
        else
            state = State.STAY;

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
