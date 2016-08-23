using UnityEngine;
using System.Collections;

public class RocketJump : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Collider2D col;

    public bool IsRocketJumping;
	private bool WhileRocketJumping;
	private GameObject Camera;

    private AudioSource[] catSE;
    private AudioSource rocketJumpSE;

    float jumpStartPoint;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();

        catSE = GetComponents<AudioSource>();
        for(int i = 0; i < catSE.Length; i++)
        {
            if (catSE[i].clip.name == "Cat_RocketJump")
                rocketJumpSE = catSE[i];
        }

		Camera = GameObject.Find ("Main Camera");
    }

    void Update()
    {
		if (IsRocketJumping)
			GetComponent<PlayerController> ().moveSpeed	 = 0;

		if ((Input.GetKeyDown(KeyCode.Z) || PlayerPrefs.GetString("Action") == "true") && GetComponent<PlayerController>().isGrounded == true)
        {
            jumpStartPoint = transform.position.y;
			IsRocketJumping = true;
			WhileRocketJumping = true;
            col.enabled = false;
            rb2d.velocity = new Vector2(0, 30);
			Camera.GetComponent<CameraMove> ().enabled = false;
            rocketJumpSE.Play();
        }
		else if (GetComponent<PlayerController>().isGrounded == true && IsRocketJumping == true && Camera.GetComponent<CameraMove> ().enabled == false && WhileRocketJumping == false)
		{
			Camera.GetComponent<CameraMove> ().enabled = true;
			IsRocketJumping = false;
			GetComponent<PlayerController> ().moveSpeed	 = 5;
		}
        else if (FindObjectOfType<LevelManager>().IsPlayerDead)
        {
            Camera.GetComponent<CameraMove>().enabled = true;
            IsRocketJumping = false;
            GetComponent<PlayerController>().moveSpeed = 5;
        }

		if (transform.position.y > jumpStartPoint + 10 && WhileRocketJumping)
		{
			WhileRocketJumping = false;
			rb2d.velocity = new Vector2(0, 0);
            col.enabled = true;
            if (GetComponent<PlayerController>().isRight)
				transform.position = new Vector3 (transform.position.x + 6, jumpStartPoint + 10, transform.position.z);
			else
				transform.position = new Vector3 (transform.position.x - 6, jumpStartPoint + 10, transform.position.z);
		}
    }
}
