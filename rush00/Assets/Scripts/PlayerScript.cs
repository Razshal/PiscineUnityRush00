using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public float movementSpeed = 0.2f;

	// Use this for initialization
	void Start () {
		
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
	void Update () {
		
	}
}
