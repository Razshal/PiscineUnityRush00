using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public Texture2D texture;
    public AudioClip[] musics;
    public AudioClip looseSound;
    public AudioClip winSound;
    private AudioSource audioSource;
    private AudioClip actualMusic;

    public void ChangeSoundTrack(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    protected void PlaySound(AudioClip clip)
    {
        if (audioSource)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void LooseSound()
    {
        audioSource.loop = false;
        PlaySound(looseSound);
    }

    public void WinSound()
    {
        audioSource.loop = false;
        PlaySound(winSound);
    }

    private void Start()
    {
        if (texture)
            Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
        audioSource = gameObject.GetComponent<AudioSource>();
        if (musics.Length > 0)
        {
            actualMusic = musics[Random.Range(0, musics.Length - 1)];
            PlaySound(actualMusic);
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
