using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZoneScript : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
            if (player.alive)
                player.Win();
        }
	}
}
