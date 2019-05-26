using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnerScript : MonoBehaviour {
    public GameObject[] weapons;

	// Use this for initialization
	void Start () {
        if (weapons.Length > 0)
        {
            Instantiate(weapons[Random.Range(0, weapons.Length)],
                        gameObject.transform.position,
                        gameObject.transform.rotation);
        }
	}
}
