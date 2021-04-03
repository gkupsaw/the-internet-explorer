using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D body;

    public float runSpeed = 20f;
    public float m_JumpForce = 400f;							// Amount of force added when the player jumps.
    public float gravityScale = 2.4f;

    public LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
    public Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
    public float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded

    float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    bool m_FacingRight = false;  // For determining which way the player is currently facing.
    Vector3 m_Velocity = Vector3.zero;
    bool isGrounded;
    bool jump;
    public Sprite one;
    public Sprite two;
    public Sprite three;
    public Sprite four;
    public Sprite five;
    int timer = 0;
    float horizontalMove = 0f;

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
        if (Input.GetButtonDown("Jump")) 
        {
            jump = true;
        }
        Vector3 characterScale = transform.localScale; 
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
        float move = horizontalMove * Time.fixedDeltaTime * runSpeed;

        Vector3 targetVelocity = new Vector2(move, body.velocity.y);
        body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        if (move > 0 && !m_FacingRight || move < 0 && m_FacingRight)
        {
            Flip();
        }

        if (jump && isGrounded)
        {
            jump = false;
			body.AddForce(new Vector2(0f, m_JumpForce));
        }
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

    public float force = 0.4f;

    void OnTriggerEnter2D(Collider2D collider){
    // void OnCollisionEnter(Collision collision){
        string tag = collider.gameObject.tag;
        if (tag == "Glitch" || tag == "TeleportingGlitch" || tag == "WalkingGlitch"){
            Debug.Log("!!! collided with enemy: " + tag);
            // add knockback


            GameObject gHit = collider.gameObject;
            Transform tHit = gHit.transform;
            Vector2 enemyPosition = new Vector3(tHit.position.x, tHit.position.y, tHit.position.z);
            Vector2 playerPosition = transform.position;
            Vector2 dir = enemyPosition - playerPosition;
            dir = -dir.normalized;

            body.velocity = new Vector2(0, 0);
            body.inertia = 0;

            // disable player controls
            // playerControlerScript.playerControles = false;
            // Invoke("EnablePlayerControles", 0.5f);

            // modify the values since the low gravity makes the knockback fly way too high
            Vector2 knockbackValues = new Vector2(dir.x / 0.4f, dir.y * 0.8f);

            body.AddForce(knockbackValues * force, ForceMode2D.Force);
        }
    }

}
