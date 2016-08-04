using UnityEngine;
using System.Collections;

public class RocketJump : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private double RocketJumpSpeed;
    private double RocketMoveSpeed;

    public bool IsRocketJumping;

    private AudioSource[] catSE;
    private AudioSource rocketJumpSE;

    void Start()
    {
        RocketJumpSpeed = gameObject.GetComponent<PlayerController>().jumpSpeed * 2.4678;
        RocketMoveSpeed = gameObject.GetComponent<PlayerController>().moveSpeed / 6;
        rb2d = GetComponent<Rigidbody2D>();

        catSE = GetComponents<AudioSource>();
        for(int i = 0; i < catSE.Length; i++)
        {
            if (catSE[i].clip.name == "Cat_RocketJump")
                rocketJumpSE = catSE[i];
        }
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Z) || PlayerPrefs.GetString("Action") == "true") && IsRocketJumping == false)
        {
            IsRocketJumping = true;
            rb2d.velocity = new Vector2((float)RocketMoveSpeed, (float)RocketJumpSpeed);
            rocketJumpSE.Play();
        }

        if (gameObject.GetComponent<PlayerController>().isGrounded == true && IsRocketJumping == true)
        {
            IsRocketJumping = false;
        }
    }
}
