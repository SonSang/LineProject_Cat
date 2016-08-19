using UnityEngine;
using System.Collections;

public class WallJump : MonoBehaviour
{
	public GameObject WallChecker;

	private bool IsOnWall;
	private bool Jumping;
    private bool WhileOnWall;

	void Start()
	{
		IsOnWall = false;
		Jumping = false;
        WhileOnWall = false;
	}

	void Update()
	{
        if (IsOnWall && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            GetComponent<Rigidbody2D>().gravityScale = 0.03f;
            GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
            WhileOnWall = true;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 3f;
            WhileOnWall = false;
        }

        if (WhileOnWall && Jumping)
        {
            GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 5f);
        }

        if (!Jumping && Input.GetKey("space"))
        {
            Jumping = true;
        }
        else if (!Input.GetKey("space"))
        {
            Jumping = false;
        }
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		IsOnWall = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
        IsOnWall = false;
	}
}
