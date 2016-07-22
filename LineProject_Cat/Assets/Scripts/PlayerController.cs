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
    
	public GameObject MobileControl;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
	
	void Update ()
	{
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
		MobileControl.transform.position = new Vector3 (transform.position.x, transform.position.y, MobileControl.transform.position.z);

        moveVelocity = 0;

        /*if(PlayerPrefs.GetString("HorizontalDirection") == "Right")
        {
            moveVelocity = moveSpeed;
        }

		if(PlayerPrefs.GetString("HorizontalDirection") == "Left")
        {
            moveVelocity = (-1) * moveSpeed;
        }

        rb2d.velocity = new Vector2(moveVelocity, rb2d.velocity.y);

		if (PlayerPrefs.GetString("Jump") == "Jump" && isGrounded)
        {
            anim.SetBool("Jumping", true);
            jump = true;
        }*/
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVelocity = moveSpeed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVelocity = (-1) * moveSpeed;
        }

        rb2d.velocity = new Vector2(moveVelocity, rb2d.velocity.y);

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            anim.SetBool("Jumping", true);
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
            anim.SetBool("Jumping", false);
        else
            anim.SetBool("Jumping", true);

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
