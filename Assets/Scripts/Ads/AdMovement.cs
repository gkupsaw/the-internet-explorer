using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdMovement : MonoBehaviour
{
    public AudioClip glitchDestroyedSound;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (Input.GetMouseButton(0) &&
            col.gameObject.tag == "WalkingGlitch" || col.gameObject.tag == "TeleportingGlitch")
        {
            AudioSource.PlayClipAtPoint(glitchDestroyedSound, new Vector3(0,0,0));
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
