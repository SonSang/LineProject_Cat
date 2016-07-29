using UnityEngine;
using System.Collections;

public class EnemyController : KillPlayer, FindPlayerInterface {

    private float moveSpeed;
    public bool isRight;

    public float moveRange;
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

    private bool enteringPatrol;
    private bool atPatrolEdge;
    private float patrolPoint;
    private float patrolLeftPoint;
    private float patrolRightPoint;
    private float turningPoint;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
        levelManager = FindObjectOfType<LevelManager>();
        moveSpeed = (float)(FindObjectOfType<PlayerController>().moveSpeed * 0.8);
        enteringPatrol = true;
        atPatrolEdge = false;
	}

    public void FindPlayer()
    {
        player = FindObjectOfType<PlayerController>();
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

        if (transform.position.x <= patrolLeftPoint && !isRight)
            atPatrolEdge = true;
        else if (transform.position.x >= patrolRightPoint && isRight)
            atPatrolEdge = true;
        else
            atPatrolEdge = false;

        if (!isGroundedAhead || isWallAhead || atPatrolEdge)
            Flip();
    }
	
	// Update is called once per frame
	void Update () {

        isGroundedAhead = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isWallAhead = Physics2D.OverlapCircle(wallCheck.position, groundCheckRadius, whatIsGround);

        if (!levelManager.IsPlayerDead)
        {
            if (Mathf.Abs(CheckPlayer()) < attackRange && player.name != "BabyCat")
                isAttacking = true;
            else
                isAttacking = false;
        }
        else
            isAttacking = false;
        

        // attack mode
        if(isAttacking )
        {
            enteringPatrol = true;
            Attack();
        }            
        // patrol mode
        else
        {
            if(enteringPatrol)
            {
                setPatrolPoints();
                enteringPatrol = false;
            }
            Patrol();
        }
            
    }

    void setPatrolPoints()
    {
        patrolPoint = transform.position.x;
        patrolLeftPoint = transform.position.x - (moveRange * (float)0.5);
        patrolRightPoint = transform.position.x + (moveRange * (float)0.5);
    }

    void Flip()
    {
        isRight = !isRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
