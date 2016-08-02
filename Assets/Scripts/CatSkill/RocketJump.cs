using UnityEngine;
using System.Collections;

public class RocketJump : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private double RocketJumpSpeed;
    private double RocketMoveSpeed;

    public bool IsRocketJumping;

    void Start()
    {
        RocketJumpSpeed = gameObject.GetComponent<PlayerController>().jumpSpeed * 2.4678;
        RocketMoveSpeed = gameObject.GetComponent<PlayerController>().moveSpeed / 6;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Z) || PlayerPrefs.GetString("Action") == "true") && IsRocketJumping == false)
        {
            IsRocketJumping = true;
            rb2d.velocity = new Vector2((float)RocketMoveSpeed, (float)RocketJumpSpeed);
        }

        if (gameObject.GetComponent<PlayerController>().isGrounded == true && IsRocketJumping == true)
        {
            IsRocketJumping = false;
        }
    }
}
