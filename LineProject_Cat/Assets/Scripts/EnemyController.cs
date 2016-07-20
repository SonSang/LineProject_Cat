using UnityEngine;
using System.Collections;

public class EnemyController : KillPlayer {

    public float moveSpeed;
    public bool isRight;

    public int moveRange;

    public bool isAttacking;
    public int attackRange;
    public int attackOffset;

    public Transform groundCheck;
    public Transform wallCheck;
    public float groundCheckRadius;
    public float wallCheckRadius;
    public bool isGroundedAhead;
    public bool isWallAhead;
    public LayerMask whatIsGround;

    private Rigidbody2D rb2d;
    private Animator anim;
    private PlayerController player;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
        levelManager = FindObjectOfType<LevelManager>();
        moveSpeed = (float)(FindObjectOfType<PlayerController>().moveSpeed * 0.8);
	}

    float CheckPlayer()
    {
        return (transform.position.x - player.transform.position.x);
    }

    void Attack()
    {
        anim.SetBool("Attack", true);

        if (CheckPlayer() < -attackOffset) // player is on the right side
        {
            if (!isRight)
                Flip();
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
        }
        else if (CheckPlayer() > attackOffset) // player is on the left side
        {
            if (isRight)
                Flip();
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
        }

        if (!isGroundedAhead || isWallAhead)
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
    }

    void Patrol()
    {
        anim.SetBool("Attack", false);

        if (isRight)
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
        else
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);

        if (!isGroundedAhead || isWallAhead)
            Flip();
    }
	
	// Update is called once per frame
	void Update () {

        isGroundedAhead = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isWallAhead = Physics2D.OverlapCircle(wallCheck.position, groundCheckRadius, whatIsGround);

        if (Mathf.Abs(CheckPlayer()) < attackRange)
            isAttacking = true;
        else
            isAttacking = false;

        // attack mode
        if(isAttacking)
            Attack();
        // patrol mode
        else
            Patrol();
    }

    void Flip()
    {
        isRight = !isRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
