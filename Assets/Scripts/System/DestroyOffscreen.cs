using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffscreen : MonoBehaviour
{
    public GameObject background;
    public GameObject enemiesParent;
    public GameObject adsParent;
    public GameObject platformsParent;

    private Renderer bgRend;
    private float y_min;
    private float y_max;
    private float x_min;
    private float x_max;
    void Start()
    {
        bgRend = background.GetComponent<Renderer>();
        y_min = background.transform.position.y;
        y_max = y_min + bgRend.bounds.size.y;
        x_min = background.transform.position.x;
        x_max = x_min + bgRend.bounds.size.x;
    }

    void Update()
    {
        Debug.Log(transform.position.y);
        Debug.Log(y_max);
        if (transform.position.y < y_min || transform.position.y > y_max)
        {
            Destroy(gameObject);
        }
    }
}
