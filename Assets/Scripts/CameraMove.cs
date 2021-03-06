﻿using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour, FindPlayerInterface {

    private PlayerController player;

    public float xOffset;
    public float yOffset;
    public float camSpeed;      
    private Vector3 camPos;

    private bool canMoveUp;
    private bool canMoveLeft;
    private bool canMoveRight;

    //private float upLimit;
    private float leftLimit;
    private float rightLimit;

    private Transform leftLimitPoint;
    private Transform rightLimitPoint;   

    public GameObject MobileControl;

    // Use this for initialization
    void Start () {
        FindPlayer();
        canMoveUp = true;
        canMoveLeft = true;
        canMoveRight = true;

        GameObject[] endPos = GameObject.FindGameObjectsWithTag("EndSpot");
        if (endPos[0].transform.position.x < endPos[1].transform.position.x)
        {
            leftLimitPoint = endPos[0].transform;
            rightLimitPoint = endPos[1].transform;
        }
        else
        {
            leftLimitPoint = endPos[1].transform;
            rightLimitPoint = endPos[0].transform;
        }

        float leftLimitOffset = Camera.main.ScreenToWorldPoint(Vector3.zero).x - leftLimitPoint.position.x;
        leftLimit = Camera.main.transform.position.x - leftLimitOffset;
        float rightLimitOffset = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - rightLimitPoint.position.x;
        rightLimit = Camera.main.transform.position.x - rightLimitOffset;
    }
	
	// Update is called once per frame
	void Update () {

        camPos = transform.position;
        Debug.DrawLine(new Vector3(camPos.x - xOffset, camPos.y), new Vector3(camPos.x + xOffset, camPos.y), Color.red);
        Debug.DrawLine(new Vector3(camPos.x, camPos.y - yOffset), new Vector3(camPos.x, camPos.y + yOffset), Color.red);

        if (player.transform.position.x < leftLimit + 0.5)
            canMoveLeft = false;
        else
            canMoveLeft = true;

        if (player.transform.position.x > rightLimit - 0.5)
            canMoveRight = false;
        else
            canMoveRight = true;
/*
        if (player.transform.position.y > upLimit - 0.5)
            canMoveUp = false;
        else
            canMoveUp = true;
*/
        if (transform.position != player.transform.position)
        {
            /*
            if (!canMoveUp)
            {
                transform.position = Vector2.Lerp(new Vector2(transform.position.x, upLimit),
                    new Vector2(player.transform.position.x, upLimit), Time.deltaTime * camSpeed);
                transform.position = new Vector3(transform.position.x, transform.position.y, camPos.z);
            }*/
            if (!canMoveLeft)
            {
                transform.position = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y),
                    new Vector2(leftLimit, player.transform.position.y), Time.deltaTime * camSpeed);
                transform.position = new Vector3(transform.position.x, transform.position.y, camPos.z);
            }
            else if (!canMoveRight)
            {
                transform.position = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y),
                    new Vector2(rightLimit, player.transform.position.y), Time.deltaTime * camSpeed);
                transform.position = new Vector3(transform.position.x, transform.position.y, camPos.z);
            }
            else
            {
                transform.position = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y),
                    new Vector2(player.transform.position.x, player.transform.position.y), Time.deltaTime * camSpeed);
                transform.position = new Vector3(transform.position.x, transform.position.y, camPos.z);
            }
        }

        if (MobileControl != null)
        {
            MobileControl.transform.position = new Vector3(transform.position.x, transform.position.y, MobileControl.transform.position.z);
        }
    }


    public void FindPlayer()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void SetUI(GameObject mobileUI)
    {
        MobileControl = mobileUI;
    }
}
