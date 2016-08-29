using System;
using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

    public bool isOn;
    private Animator animator;
    public MovingPlatform movingPlatform;
    public GameObject platforms;

    public enum LeverType
    {
        MovePlatform, StopPlatform, CreatePlatform, RemovePlatform
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
                    throw new ArgumentNullException("Please Set MovingPlatform for this lever!!!!!");
                }
                break;
            case LeverType.CreatePlatform:
            case LeverType.RemovePlatform:
                if (platforms == null)
                {
                    throw new ArgumentNullException("Please Set Platforms for this lever!!!!!");
                }
                break;
        }

        // Initialization
        switch (leverType)
        {
            case LeverType.MovePlatform:
                movingPlatform.speed = 0;
                break;
            case LeverType.CreatePlatform:
                platforms.SetActive(false);
                break;
            case LeverType.RemovePlatform:
                platforms.SetActive(true);
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
            case LeverType.CreatePlatform:
            case LeverType.RemovePlatform:
                if (platforms.activeSelf) platforms.SetActive(false);
                else platforms.SetActive(true);
                break;
        }
    }
}
