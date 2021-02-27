using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite leftSprite;
    public Sprite rightSprite;

    public GameObject platform;
    public float movementDelay = .1f; // seconds
    public float movementSpeed = 1; // units/second
    private bool facingRight = true;

    void Start()
    {
        spriteRenderer.sprite = facingRight ? rightSprite : leftSprite;
        transform.position = platform.transform.position;
        InvokeRepeating("Move", movementDelay, movementDelay);
    }

    void Move()
    {
        transform.position = getNextPos();
    }

    Vector3 getNextPos()
    {
        float nextX = transform.position.x + (facingRight ? 1 : -1) * movementSpeed * movementDelay;
        float platformWidth = platform.GetComponent<Renderer>().bounds.size.x;
        float platformRight = platform.transform.position.x + platformWidth / 2;
        float platformLeft = platform.transform.position.x - platformWidth / 2;

        if (facingRight && nextX > platformRight)
        {
            facingRight = false;
            spriteRenderer.sprite = leftSprite;
            nextX = platformRight;
        }
        else if (!facingRight && nextX < platformLeft)
        {
            facingRight = true;
            spriteRenderer.sprite = rightSprite;
            nextX = platformLeft;
        }

        return new Vector3(nextX, transform.position.y, transform.position.z);
    }
}
