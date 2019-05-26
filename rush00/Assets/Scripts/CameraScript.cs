using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public Texture2D texture;
    public AudioClip[] musics;
    private AudioSource audioSource;
    private AudioClip actualMusic;

    private void Start()
    {
        if (texture)
            Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
        audioSource = gameObject.GetComponent<AudioSource>();
        if (musics.Length > 0)
        {
			actualMusic = musics[Random.Range(0, musics.Length - 1)];
            audioSource.clip = actualMusic;
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position
                  = new Vector3(player.transform.position.x,
                                player.transform.position.y,
                                -10);
    }
}
