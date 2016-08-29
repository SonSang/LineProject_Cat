using System;
using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

    public bool isOn;
    private Animator animator;
    public MovingPlatform movingPlatform;

    public enum LeverType
    {
        MovePlatform, StopPlatform
    }

    public LeverType leverType;

    // Use this for initialization
    void Start () {

        switch (leverType)
        {
            case LeverType.MovePlatform:
            case LeverType.StopPlatform:
                if (movingPlatform == null)
                {
                    throw new ArgumentNullException("Please Set MovingPlatform for this switch!!!!!");
                }
                break;
        }

        // Initialization
        switch (leverType)
        {
            case LeverType.MovePlatform:
                movingPlatform.speed = 0;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        isOn = true;
        switch (leverType)
        {
            case LeverType.MovePlatform:               
            case LeverType.StopPlatform:
                movingPlatform.speed = (movingPlatform.speed == 3) ? 0: 3;
                break;
        }
    }
}
