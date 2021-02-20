using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTeleport : MonoBehaviour
{
    public bool teleportImmediately = false;
    public int teleportCooldown = 3;
    public GameObject outline;

    private bool outlineTeleported = false;

    void Start()
    {
        // transform.position = new Vector3(0, 0, 0);
        InvokeRepeating("Teleport", teleportImmediately ? 0.001f : teleportCooldown, teleportCooldown);
    }

    void Teleport()
    {
        if (outlineTeleported)
        {
            transform.position = outline.transform.position;
            outline.transform.position = transform.position; // not sure why this works...
            outlineTeleported = false;
        }
        else
        {
            outline.transform.position = new Vector3(outline.transform.position.x + .5f, outline.transform.position.y + .5f, outline.transform.position.z);
            outlineTeleported = true;
        }
    }
}
