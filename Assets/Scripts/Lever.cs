using System;
using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

    public bool isOn;
    private Animator animator;
    public MovingPlatform movingPlatform;
    public GameObject platforms;

    private AudioSource[] buttonSE;

    public AudioSource leverOn;
    public AudioSource leverOff;

    public enum LeverType
    {
        MovePlatform, StopPlatform, CreatePlatform, RemovePlatform
    }

    public LeverType leverType;

    // Use this for initialization
    void Start () {

        animator = GetComponent<Animator>();

        buttonSE = GetComponents<AudioSource>();
        for (int i = 0; i < buttonSE.Length; i++)
        {
            if (buttonSE[i].clip.name == "Cat_ButtonOn")
                leverOn = buttonSE[i];
            if (buttonSE[i].clip.name == "Cat_ButtonOff")
                leverOff = buttonSE[i];
        }

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
    void Update()
    {
        animator.SetBool("switch", isOn);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        isOn = (isOn) ? false : true;

        //Sound Play
        if (isOn)
        {
            leverOn.Play();
        }
        else
        {
            leverOff.Play();
        }

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
