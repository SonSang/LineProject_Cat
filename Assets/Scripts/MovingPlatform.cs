using UnityEngine;
using System.Collections;

public enum PlatformType
{
    Horizontal,
    Vertical
}

public class MovingPlatform : MonoBehaviour {

    public PlatformType platformType;

    private Transform leftLimit;
    private Transform rightLimit;
    private Vector3 leftLimitPos;
    private Vector3 rightLimitPos;

    private Transform leftEnd;
    private Transform rightEnd;

    private Rigidbody2D rb2d;

    private bool IsMovingRight = true;

    public float speed;

	// Use this for initialization
	void Start () {
        leftLimit = this.transform.Find("LeftLimit");
        rightLimit = this.transform.Find("RightLimit");
        leftLimitPos = leftLimit.position;
        rightLimitPos = rightLimit.position;

        leftEnd = this.transform.Find("LeftEnd");
        rightEnd = this.transform.Find("RightEnd");

        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update() {
        MaintainLimitPos();
        if (platformType == PlatformType.Horizontal)
            FindHorizontalDirection();
        else
            FindVerticalDirection();
        
        if(IsMovingRight)
        {
            float zPos = this.transform.position.z;
            this.transform.position = Vector3.MoveTowards(this.transform.position, rightLimit.position, Time.deltaTime * speed);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, zPos);
        }
        else
        {
            float zPos = this.transform.position.z;
            this.transform.position = Vector3.MoveTowards(this.transform.position, leftLimit.position, Time.deltaTime * speed);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, zPos);
        }
        
	}

    void MaintainLimitPos()
    {
        leftLimit.position = leftLimitPos;
        rightLimit.position = rightLimitPos;
    }

    void FindHorizontalDirection()
    {
        if(IsMovingRight)
        {
            if (rightEnd.position.x > rightLimit.position.x - 0.5)
                IsMovingRight = !IsMovingRight;
        }
        else
        {
            if (leftEnd.position.x < leftLimit.position.x + 0.5)
                IsMovingRight = !IsMovingRight;
        }
    }

    void FindVerticalDirection()
    {
        if (!IsMovingRight)
        {
            if (leftEnd.position.y > leftLimit.position.y - 0.5)
                IsMovingRight = !IsMovingRight;
        }
        else
        {
            if (rightEnd.position.y < rightLimit.position.y + 0.5)
                IsMovingRight = !IsMovingRight;
        }
    }
}
