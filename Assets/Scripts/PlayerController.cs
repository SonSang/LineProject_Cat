using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Speed
    public int moveSpeed;
    private float moveVelocity;
    public int jumpSpeed;

    // Move State
    private bool isRight = true;
    private bool jump = false;
    private bool readyToJump = true;

    // Ground Check
    public Transform groundCheck;
    public float groundCheckRadius;
    public bool isGrounded;
    public LayerMask whatIsGround;

    // Life
    public int life;
    public GameObject corpse;

    private Rigidbody2D rb2d;
    private Animator anim;

    private AudioSource[] catSE;
    private AudioSource catJump;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        catSE = GetComponents<AudioSource>();
        
        for(int i = 0; i < catSE.Length; i++)
        {
            if (catSE[i].clip.name == "Cat_Jump")
                catJump = catSE[i];
        }
    }
	
	void Update ()
	{
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        moveVelocity = 0;

		if(PlayerPrefs.GetString("HorizontalDirection") == "Right" || Input.GetKey(KeyCode.RightArrow))
        {
            moveVelocity = moveSpeed;
        }

		if(PlayerPrefs.GetString("HorizontalDirection") == "Left"  || Input.GetKey(KeyCode.LeftArrow))
        {
            moveVelocity = (-1) * moveSpeed;
        }

        rb2d.velocity = new Vector2(moveVelocity, rb2d.velocity.y);

        if(isGrounded && moveVelocity != 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
        
        if(gameObject.GetComponent<RocketJump>() != null)
        {
            if (gameObject.GetComponent<RocketJump>().IsRocketJumping == false)
                rb2d.velocity = new Vector2(moveVelocity, rb2d.velocity.y);
        }

        if ((PlayerPrefs.GetString("Jump") == "Jump" || Input.GetKeyDown("space")) && isGrounded)
        {
            anim.SetBool("Jumping", true);
            catJump.Play();
            jump = true;
        }
    }

    void FixedUpdate()
    {
        if (moveVelocity > 0 && !isRight)
            Flip();
        else if (moveVelocity < 0 && isRight)
            Flip();

        if (isGrounded)
        {
            anim.SetBool("Jumping", false);
        }
        else
        {
            anim.SetBool("Jumping", true);
        }

        if (jump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            jump = false;
        }

    }

    void Flip()
    {
        isRight = !isRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
