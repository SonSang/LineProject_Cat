using System;
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

    // For GroundPatrol state management
    private bool enteringPatrol;

    // For AirPatrol
    private Vector3 targetPoint;
    private Vector3 lastPosition;
    private bool isAttackCooldown;
    private int stuckCounter;
    private int waitToStrikeCounter;


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
        waitToStrikeCounter = 0;

        groundCheckRadius = 0.1f;
	    wallCheckRadius = 0.5f;

        // Set patrol range with "LeftPoint" "RightPoint"
        leftPoint.gameObject.SetActive(false);  
        rightPoint.gameObject.SetActive(false);

        patrolLeftPoint = new Vector3(leftPoint.position.x, leftPoint.position.y);
        patrolRightPoint = new Vector3(rightPoint.position.x, rightPoint.position.y);

        if (patrolLeftPoint.x > patrolRightPoint.x)
        {
            var tmp = patrolLeftPoint;
            patrolLeftPoint = patrolRightPoint;
            patrolRightPoint = tmp;
        }
        // Turn off gravity for air units
        if (aiType == EnemyAIType.AirPatrol) {
            rb2d.gravityScale = 0;
        }
    }
    
    void Update() {
        switch (aiType)
        {
            case EnemyAIType.GroundPatrol:
                GroundPatrol();
                break;
            case EnemyAIType.AirPatrol:
                AirPatrol();
                break;
            case EnemyAIType.AvoidPlayer:
                AvoidPlayer();
                break;
        }
    }

    void AvoidPlayer()
    {
        // AI that avoids player. (except babycat) 
        if (player.name != "BabyCat" &&
            Vector2.Distance(transform.position, player.transform.position) < attackDistance)
        { // If player is close enough
            if (transform.position.x > player.transform.position.x){
                // Player is at left side
                if (!isRight) {
                    Flip();
                }
                SetXSpeed(moveSpeed);
            }
            else { // right side
                if (isRight) {
                    Flip();
                }
                SetXSpeed(-moveSpeed);
            }
        }
        else{
            SetXSpeed(0);
        }
    }

    void AirPatrol()
    {
        if (waitToStrikeCounter > 0) { // Wait a bit
            waitToStrikeCounter -= 1;
            return;
        }

        if (levelManager.IsPlayerDead) {
            isAttacking = false;
        }

        // Select destination
        Vector2 destination;
        if (isAttacking) {
            destination = targetPoint;
        }
        else if (isRight) {
            destination = patrolRightPoint;
        }
        else {
            destination = patrolLeftPoint;
        }

        // Go to the destination with set speed
        Vector2 speedVector = destination - (Vector2)transform.position;
        speedVector = moveSpeed * speedVector.normalized;
        rb2d.velocity = speedVector;

        float epsilon = 0.01f;
        if (Vector2.Distance(transform.position, lastPosition) < epsilon) {
            stuckCounter += 1;
        }
        else {
            stuckCounter = 0;
        }
        bool isStuck = stuckCounter > 10;

        if (Vector2.Distance(transform.position, destination) < epsilon 
            || isPointBetweenPoints(destination, lastPosition, transform.position)
            || isStuck) {
            // If destination is achieved or somehow went past it or got stuck
            // then change the target
            if (isAttacking) {
                isAttacking = false;
                isAttackCooldown = true;
            }
            else {
                isRight = !isRight;
                isAttackCooldown = false; // Set this here so that enemy needs to at least hit patrol point once before attacking
            }
        }

        // Attack lock on
        if (!isAttacking && !isAttackCooldown && Vector2.Distance(transform.position, player.transform.position) < attackDistance) {
            // Lock on to the current player position
            // The state must be 'Not attacking' since it needs to lock on
            targetPoint = player.transform.position;
            isAttacking = true;
            waitToStrikeCounter = 100;
        }

        lastPosition = transform.position;
    }

    bool isPointBetweenPoints(Vector2 p, Vector2 a, Vector2 b)
    {
        // Check if point p lies on the line ab
        float epsilon = 0.001f;
        if (Vector2.Distance(a, b) < epsilon) {
            return false;
        }

        // Triangle inequality
        return Vector2.Distance(a, p) + Vector2.Distance(p, b) - Vector2.Distance(a, b) < epsilon;
    }

    void GroundPatrol()
    {
        if (levelManager.IsPlayerDead || player.name == "BabyCat") {
            isAttacking = false;
        }
        else if (Mathf.Abs(transform.position.x - player.transform.position.x) > attackDistance) {
            isAttacking = false;
        }
        else {
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
        //anim.SetBool("Attack", false);

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
    void SetYSpeed(float ySpeed) {
        rb2d.velocity = new Vector2(rb2d.velocity.x, ySpeed);
    }
    void SetSpeed(float xSpeed, float ySpeed) {
        rb2d.velocity = new Vector2(xSpeed, ySpeed);
    }

    void Flip()
    {
        isRight = !isRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
