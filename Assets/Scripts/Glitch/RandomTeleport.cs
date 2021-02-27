using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTeleport : MonoBehaviour
{
    public bool teleportImmediately = false;
    public int teleportCooldown = 3;
    public GameObject outline;
    public GameObject platform;
    public GameObject[] platforms;

    private bool outlineTeleported = false;

    void Start()
    {
        platform = getNextPlatform();
        transform.position = spiderGetNextPos();
        // transform.position = new Vector3(platform.transform.position.x, platform.transform.position.y + .5f, platform.transform.position.z);
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
            outline.transform.position = spiderGetNextPos();
            outlineTeleported = true;
        }
    }

    // Vector3 goombaGetNextPos()
    // {
    //     float range = platform.GetComponent<Renderer>().bounds.size.x;
    //     return new Vector3(Random.Range(-range / 2, range / 2), platform.transform.position.y, platform.transform.position.z);
    // }

    Vector3 spiderGetNextPos()
    {
        return platform ? platform.transform.position : transform.position;
    }

    GameObject getNextPlatform()
    {
        return platforms[Random.Range(0, platforms.Length)];
    }
}
