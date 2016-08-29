using UnityEngine;
using System.Collections;

public class PassWall_SchrodingerCat : MonoBehaviour
{
    public bool SchrodingerMode;
    public GameObject ObstacleChecker;

    private bool IsGround;
    private bool ObstacleCheck;

    void Start()
    {
        SchrodingerMode = false;
        ObstacleCheck = false;
    }

    void Update()
    {
        IsGround = GetComponent<PlayerController>().isGrounded;
        Debug.Log(ObstacleCheck);
        Debug.Log(GetComponent<ObstacleCheck_SchrodingerCat>().ObstacleCheck);
        ObstacleCheck = GetComponent<ObstacleCheck_SchrodingerCat>().ObstacleCheck;

        if (IsGround)
        {
            if (Input.GetKeyDown(KeyCode.Z) && !SchrodingerMode)
            {
                Debug.Log("SchrodingerMode On!");
                SchrodingerMode = true;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Collider2D>().enabled = false;
                return;
            }
        }

        if (SchrodingerMode)
        {
            if (ObstacleCheck)
            {
                Debug.Log("Percentage Down");
            }

            if (!IsGround)
            {
                Debug.Log("SchrodingerMode Off!");
                SchrodingerMode = false;
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("SchrodingerMode Off!");
                SchrodingerMode = false;
            }
        }

        if (!SchrodingerMode)
        {
            GetComponent<Rigidbody2D>().gravityScale = 3;
            GetComponent<Collider2D>().enabled = true;
        }
    }
}
