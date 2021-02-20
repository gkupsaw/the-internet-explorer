using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public bool exitable = false;

    //adding difficutly?
    void Start()
    {
        this.rb.velocity = new Vector2(0.0f, -0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.rb.position.y < -3.8f){
            exitable = true;
        }
    }
}
