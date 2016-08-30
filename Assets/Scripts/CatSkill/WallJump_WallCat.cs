using UnityEngine;
using System.Collections;

public class WallJump_WallCat : MonoBehaviour
{
	public GameObject WallChecker;

	public bool IsOnWall;
	private bool Jumping;
    private bool WhileOnWall;
    private bool IsRight;

	void Start()
	{
		IsOnWall = false;
		Jumping = false;
        WhileOnWall = false;
	}

	void Update()
	{
        IsRight = GetComponent<PlayerController>().isRight;

        if (IsOnWall && ((Input.GetKey(KeyCode.LeftArrow) || PlayerPrefs.GetString("HorizontalDirection") == "Left") || (Input.GetKey(KeyCode.RightArrow) || PlayerPrefs.GetString("HorizontalDirection") == "Right")))
        {
            GetComponent<Rigidbody2D>().gravityScale = 0.8f;
            GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
            WhileOnWall = true;
			//GetComponent<Transform> ().rotation = new Quaternion (0, 0, 90, GetComponent<Transform> ().rotation.w);

        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 3f;
            WhileOnWall = false;
        }

        if (WhileOnWall && Jumping)
        {
            if (IsRight && (Input.GetKey(KeyCode.LeftArrow) || PlayerPrefs.GetString("HorizontalDirection") == "Left"))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, 14f);
            }
            else if (!IsRight && (Input.GetKey(KeyCode.RightArrow) || PlayerPrefs.GetString("HorizontalDirection") == "Right"))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 14f);
            }
            else
                GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 5f);
        }

        if (!Jumping && (Input.GetKey("space") || PlayerPrefs.GetString("Jump") == "Jump"))
        {
            Jumping = true;
        }
        else if (!(Input.GetKey("space") || PlayerPrefs.GetString("Jump") == "Jump"))
        {
            Jumping = false;
        }
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if(other.tag == "Untagged")
            IsOnWall = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
        IsOnWall = false;
	}
}
