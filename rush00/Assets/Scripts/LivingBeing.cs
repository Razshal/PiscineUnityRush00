using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingBeing : MonoBehaviour {

    public bool alive = true;
    public GameObject weapon;
	public Animator legs;
    protected GameObject weaponContainer;
    protected GameObject bodyContainer;
    protected Vector3 relativeTarget;
    protected Vector3 movement;
    protected float rotationRadians;


    void Start()
    {
        bodyContainer = gameObject.transform.GetChild(0).gameObject;
        weaponContainer = bodyContainer.transform.Find("WeaponContainer").gameObject;
    }

    public void Die()
    {
        Debug.Log(name + " dies");
        alive = false;
    }

}
