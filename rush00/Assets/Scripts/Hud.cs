using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Hud : MonoBehaviour {
	public	PlayerScript	player;
	private	Text	weapon;
	private	Text	ammo;
	// Use this for initialization
	void Awake () {
		weapon = GameObject.Find("Name").GetComponent<Text>();
		ammo = GameObject.Find("Ammo").GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
		if (player.weapon)	{
			WeaponScript Weapon = player.weapon.GetComponent<WeaponScript>();
			ammo.text = (Weapon.isMeleeWeapon) ? "∞" : Weapon.ammoNumber.ToString();
		}
		else {
			ammo.text = "-";
			weapon.text = "No Weapon";
		}

	}
}
