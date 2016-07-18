using UnityEngine;
using System.Collections;

public class EnemyController : KillPlayer {

    public float moveSpeed;
    public bool isRight;

    public int moveRange;

    public bool isScouting;
    public int scoutRange;

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
    private LevelManager levelManager;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
        levelManager = FindObjectOfType<LevelManager>();
        moveSpeed = (float)(FindObjectOfType<PlayerController>().moveSpeed * 0.8);
	}

    bool CheckPlayer()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < scoutRange)
            return true;

        return false;
    }
	
	// Update is called once per frame
	void Update () {
        if (CheckPlayer())
            isScouting = true;
        else
            isScouting = false;

        if (isRight)
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
        else
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);

        isGroundedAhead = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isWallAhead = Physics2D.OverlapCircle(wallCheck.position, groundCheckRadius, whatIsGround);

        if (!isGroundedAhead || isWallAhead)
            Flip();
    }

    void FixedUpdate()
    {
        
    }

    void Flip()
    {
        isRight = !isRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            levelManager.respawnPlayer(this);
        }
    }
}
