using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public Texture2D texture;

    private void Start()
    {
        if (texture)
            Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
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
