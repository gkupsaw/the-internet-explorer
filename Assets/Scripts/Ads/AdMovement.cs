using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdMovement : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Glitch")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
