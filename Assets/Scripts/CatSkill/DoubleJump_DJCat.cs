using UnityEngine;
using System.Collections;

public class DoubleJump_DJCat : MonoBehaviour
{
    private bool IsGround;
    private bool CanDoubleJump;

    void Update()
    {
        IsGround = GetComponent<PlayerController>().isGrounded;

        if (IsGround)
            CanDoubleJump = true;
        else
        {
            if (CanDoubleJump && (PlayerPrefs.GetString("Jump") == "Jump" || Input.GetKeyDown("space")))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 14);
                CanDoubleJump = false;
            }
        }
    }
}
