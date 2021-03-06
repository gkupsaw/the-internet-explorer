using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D scrollBar;
    public bool exitable;
    public Rigidbody2D exitButton;
    public bool spawned;
    public float difficultyFactor;
    public float scrollStartX;
    void Start()
    {
        spawned = false;
        // this.exitButton.transform.position = new Vector3(-10, -10, 10); //change to z later
        difficultyFactor = 1;
        scrollStartX = this.scrollBar.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.scrollBar.position.y > 1.8f){
            spawned = false;
        }
        if(this.scrollBar.position.y < 1.8f && spawned == false){
            spawned = true;
            this.exitButton.transform.position = new Vector2(0, -2.5f);
        }
    }
    void OnMouseDown()
    {   
        this.exitButton.transform.position = new Vector2(-10, -10);
        this.scrollBar.transform.position = new Vector3(scrollStartX, 3.65f, -1);
        difficultyFactor++;
        Debug.Log(difficultyFactor);
    }
}
