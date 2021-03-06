﻿using UnityEngine;
using System.Collections;

public class BambooHelicopter_BambooCat : MonoBehaviour
{
    public float HoveringTime;

    private bool IsHovering;
    private float StartHoveringTime;
    private float NowTime;

    private AudioSource bambooFly;
    private AudioSource[] catSE;

    void Start()
    {
        IsHovering = false;
        catSE = GetComponents<AudioSource>();
        for (int i = 0; i < catSE.Length; i++)
        {
            if (catSE[i].clip.name == "Cat_BambooFlying")
                bambooFly = catSE[i];
        }
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Z) || PlayerPrefs.GetString("Action") == "true") && !IsHovering)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 400));
            GetComponent<Rigidbody2D>().gravityScale = 0.8f;
            StartHoveringTime = Time.time;
            IsHovering = true;
        }

        if (IsHovering)
        {
            NowTime = Time.time;

            if (NowTime - StartHoveringTime > HoveringTime)
            {
                IsHovering = false;
                GetComponent<Rigidbody2D>().gravityScale = 3f;
            }

			if ((PlayerPrefs.GetString("Jump") == "Jump" || Input.GetKeyDown("space")))
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 2);
            bambooFly.Play();
        }
        else
        {
            bambooFly.Stop();
        }
    }
}
