using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdMovement : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (Input.GetMouseButton(0) &&
            col.gameObject.tag == "WalkingGlitch" || col.gameObject.tag == "TeleportingGlitch")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
