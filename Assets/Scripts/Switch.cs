using System;
using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
    public bool isOn;
    private Animator animator;
    public MovingPlatform movingPlatform;

    public enum SwitchType
    {
        MovePlatform, StopPlatform
    }

    public SwitchType switchType;

    void Start ()
	{
	    animator = GetComponent<Animator>();


        // Argument Validation
        switch (switchType)
        {
            case SwitchType.MovePlatform:
            case SwitchType.StopPlatform:
                if (movingPlatform == null) {
                    throw new ArgumentNullException("Please Set MovingPlatform for this switch!!!!!");
                }
                break;
        }

        // Initialization
        switch (switchType)
        {
            case SwitchType.MovePlatform:
                movingPlatform.speed = 0;
                break;
        }
	}
	
	void Update () {
        animator.SetBool("switch", isOn);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        isOn = true;
        switch (switchType)
        {
            case SwitchType.MovePlatform:
                movingPlatform.speed = 3;
                break;
            case SwitchType.StopPlatform:
                movingPlatform.speed = 0;
                break;
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        isOn = false;
        switch (switchType) {
            case SwitchType.MovePlatform:
                movingPlatform.speed = 0;
                break;
            case SwitchType.StopPlatform:
                movingPlatform.speed = 3;
                break;
        }
    }
}
