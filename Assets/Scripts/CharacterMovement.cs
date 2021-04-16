using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public AudioClip platformLandSound;

    public float runSpeed = 20f;
    public float m_JumpForce = 400f;							// Amount of force added when the player jumps.
    public float gravityScale = 2.4f;
    public Animator animator;

    public LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
    public Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
    public float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded

    float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    bool m_FacingRight = false;  // For determining which way the player is currently facing.
    Vector3 m_Velocity = Vector3.zero;
    bool isGrounded;
    bool jump;
    public Sprite falling;
    public Sprite two;
    public Sprite three;
    public Sprite four;
    public Sprite five;
    int timer = 0;
    float horizontalMove = 0f;
    float prevVelocityY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        body.gravityScale = gravityScale;
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
<<<<<<< HEAD
        Vector3 characterScale = transform.localScale; 
=======
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        Vector3 characterScale = transform.localScale;
>>>>>>> adcd1e5f5053d0c219f839de61cddabd74b44178
        if (Input.GetAxis("Horizontal") < 0) {
            characterScale.x = 5; //initial file is facing left
        }
        if (Input.GetAxis("Horizontal") > 0){
            characterScale.x = -5;
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        bool currentlyMidAir = !isGrounded;
        float move = horizontalMove * Time.fixedDeltaTime * runSpeed;

        Vector3 targetVelocity = new Vector2(move, body.velocity.y);
        body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        if (move > 0 && !m_FacingRight || move < 0 && m_FacingRight)
        {
            Flip();
        }

<<<<<<< HEAD
        if (!isGrounded)
=======
        // just landed
        if (prevVelocityY < 0 && Mathf.Abs(body.velocity.y) < Mathf.Epsilon)
        {
            AudioSource.PlayClipAtPoint(platformLandSound, new Vector3(0,0,0));
        }

        if (jump && isGrounded)
>>>>>>> adcd1e5f5053d0c219f839de61cddabd74b44178
        {
            //  this.body.gameObject.GetComponent<SpriteRenderer>().sprite = falling;
             animator.SetBool("falling", true);
        } else {
            animator.SetBool("falling", false);
        }

        prevVelocityY = body.velocity.y;
    }

	void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
