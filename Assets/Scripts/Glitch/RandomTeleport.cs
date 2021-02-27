using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTeleport : MonoBehaviour
{
    public bool teleportImmediately = false;
    public int teleportCooldown = 3;
    public GameObject outline;
    private GameObject platform;
    public GameObject[] platforms;

    private bool outlineTeleported = false;

    void Start()
    {
        platform = getNextPlatform();
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
            outline.transform.position = getNextPos();
            outlineTeleported = true;
            platform = getNextPlatform();
        }
    }

    Vector3 getNextPos()
    {
        return platform ? platform.transform.position : transform.position;
    }

    GameObject getNextPlatform()
    {
        if (platforms.Length == 1)
        {
            return platforms[0];
        }

        // make sure we don't just choose the same platform
        GameObject next = platform;
        while (next == platform)
        {
            platform = platforms[Random.Range(0, platforms.Length)];
        }
        return platform;
    }
}
