using UnityEngine;
using System.Collections;

public class EnemyController : KillPlayer, FindPlayerInterface {

    // Public variables settable in editor
    private float moveSpeed;
    public float attackDistance; // Aggro range
    public float attackInertiaDistance; // Simulate inertia for more natural moving

    public Transform leftPoint;
    public Transform rightPoint;
    public Transform groundCheck; // Point where ai checks for ground
    public Transform wallCheck; // Point where ai checks for wall

    public enum EnemyAIType {
        GroundPatrol, AirPatrol, AvoidPlayer
    }

    public EnemyAIType aiType;
    /// Note you also need to set the "LeftPoint" and "RightPoint" in the editor.
    private Vector3 patrolLeftPoint; // Edit position of "LeftPoint"
    private Vector3 patrolRightPoint; // Edit position of "RightPoint"

    // private variables

    private Rigidbody2D rb2d;
    private Animator anim;
    private PlayerController player;

    private float groundCheckRadius;
    private float wallCheckRadius;

    private bool isRight;
    private bool isAttacking;

    private bool enteringPatrol;
    


    public void FindPlayer() {
        // Reload player when player respawns since the reference changes.
        player = FindObjectOfType<PlayerController>();
    }
    
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
        levelManager = FindObjectOfType<LevelManager>();
        moveSpeed = player.moveSpeed * 0.8f;
        enteringPatrol = true;
	    isRight = true;
        isAttacking = false;

	    groundCheckRadius = 0.1f;
	    wallCheckRadius = 0.5f;

        // Set patrol range with "LeftPoint" "RightPoint"
        leftPoint.gameObject.SetActive(false);  
        rightPoint.gameObject.SetActive(false);

        patrolLeftPoint = leftPoint.position;
        patrolRightPoint = rightPoint.position;
    }
    
    void Update() {
        switch (aiType)
        {
            case EnemyAIType.GroundPatrol:
                GroundPatrol();
                break;
            case EnemyAIType.AvoidPlayer:
                AvoidPlayer();
                break;
        }
    }

    void AvoidPlayer()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < attackDistance)
        {
            if (transform.position.x > player.transform.position.x){
                // Player is at left side
                SetXSpeed(moveSpeed);
            }
            else{ // right side
                SetXSpeed(-moveSpeed);
            }
        }
        else{
            SetXSpeed(0);
        }

    }
    
    void GroundPatrol()
    {
        if (levelManager.IsPlayerDead || player.name == "BabyCat") {
            isAttacking = false;
        }
        else if (Mathf.Abs(transform.position.x - player.transform.position.x) > attackDistance){
            isAttacking = false;
        }
        else{
            isAttacking = true;
        }


        // attack mode
        if (isAttacking) {
            enteringPatrol = true;
            GroundPatrolAttack();
        }
        // patrol mode
        else {
            if (enteringPatrol) {
                ReturnToPatrolPoint();
            }
            Patrol();
        }
    }
    void GroundPatrolAttack()
    {
        anim.SetBool("Attack", true);

        if (transform.position.x - player.transform.position.x < -attackInertiaDistance) // player is on the right side
        {
            if (!isRight)
                Flip();
            SetXSpeed(moveSpeed);
        }
        else if (transform.position.x - player.transform.position.x > attackInertiaDistance) // player is on the left side
        {
            if (isRight)
                Flip();
            SetXSpeed(-moveSpeed);
        }

        if (!isGroundAhead() || isWallAhead())
            SetXSpeed(0);
    }

    void Patrol()
    {
        // Patrol for GroundPatrol AI
        anim.SetBool("Attack", false);

        if (isRight)
            SetXSpeed(moveSpeed);
        else
            SetXSpeed(-moveSpeed);

        bool atPatrolEdge;
        if (transform.position.x <= patrolLeftPoint.x && !isRight)
            atPatrolEdge = true;
        else if (transform.position.x >= patrolRightPoint.x && isRight)
            atPatrolEdge = true;
        else
            atPatrolEdge = false;

        if (!isGroundAhead() || isWallAhead() || atPatrolEdge)
            Flip();
    }

    bool isGroundAhead()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, LayerMask.GetMask("Ground"));
    }

    bool isWallAhead()
    {
        return Physics2D.OverlapCircle(wallCheck.position, groundCheckRadius, LayerMask.GetMask("Ground"));
    }

    void ReturnToPatrolPoint()
    {
        if (transform.position.x - patrolRightPoint.x > 0.5)
        {
            if (isRight)
                Flip();
            SetXSpeed(-moveSpeed);
        }
        else if (transform.position.x - patrolLeftPoint.x < -0.5)
        {
            if (!isRight)
                Flip();
            SetXSpeed(moveSpeed);
        }
        else
        {
            enteringPatrol = !enteringPatrol;
        }
    }

    void SetXSpeed(float xSpeed) {
        rb2d.velocity = new Vector2(xSpeed, rb2d.velocity.y);
    }

    void Flip()
    {
        isRight = !isRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
