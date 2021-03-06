﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;

public class LevelManager : MonoBehaviour, FindPlayerInterface {
   
    // Player is dead?
    private bool isDead;

    // Respawn
    public float respawnHeight;
    public float respawnDelay;

    // Particle Effects
    public GameObject deathParticle;
    public GameObject respawnParticle;

    private PlayerController player;
    private DeathManager deathManager;
    private PauseManager pause;
    private BackGroundController background;

    // UIs
    private GameObject mainCamera;
    private CatChangeManager catChanger;
    private GameOverManager gameOver;
    private PauseManager pauseScreen;
    private Text lifeText;

    private AudioSource catDie;

    private KillPlayer kp;

    public bool IsPlayerDead { get { return isDead; } }

    private float corpseYPos;
    private float lastCamYPos;
    
    private int formerLife;

	// Use this for initialization
	void Start () {
        isDead = false;
        player = FindObjectOfType<PlayerController>();
        deathManager = FindObjectOfType<DeathManager>();
        pause = FindObjectOfType<PauseManager>();
        background = FindObjectOfType<BackGroundController>();
        catDie = GetComponent<AudioSource>();
        PlayerPrefs.SetString("Pause", "false");
        mainCamera = GameObject.Find("Main Camera");
    }

    public void SetUI(CatChangeManager catUI, GameOverManager gameoverUI, PauseManager pauseUI, Text lifeT)
    {
        catChanger = catUI;
        gameOver = gameoverUI;
        pauseScreen = pauseUI;
        player = FindObjectOfType<PlayerController>();

        lifeText = lifeT;
        lifeText.text = "X " + player.life; 
    }
	
	// Update is called once per frame
	void Update () {
        if(!isDead && player.isGrounded)
        {
            lastCamYPos = background.GetBGPosY();
        }

        if (PlayerPrefs.GetString("Pause") == "true")
            Pause();
        else
            Resume();

        if(Input.GetKeyDown(KeyCode.Q) || PlayerPrefs.GetString("Suicide") == "true")
        {
            if(!IsPlayerDead && player.isGrounded)
            {
                Suicide();
            }
        }
    }

    public void respawnPlayer(KillPlayer kp)
    {
        if (player.life > 0)
        {
            this.kp = kp;
            StartCoroutine("respawnPlayerCo");
        }
        else
        {
            StartCoroutine("gameOverCo");
        }
        
    }

    void Suicide()
    {
        kp = new KillPlayer();
        if (player.life > 0)
        {
            StartCoroutine("respawnPlayerCo");
        }
        else
        {
            StartCoroutine("gameOverCo");
        }
    }

    public IEnumerator respawnPlayerCo()
    {
        kill();
        yield return new WaitForSeconds(2);
        Select(); 
    }

    public IEnumerator gameOverCo()
    {
        kill();        
        yield return new WaitForSeconds(2);
        gameOver.gameObject.SetActive(true);
    }

    // Kill Player
    void kill()
    {
        if(!isDead)
        {
            Instantiate(deathParticle, player.transform.position, player.transform.rotation);
            catDie.Play();

            player.enabled = false;
            player.GetComponent<Renderer>().enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().isKinematic = true;
            corpseYPos = deathManager.makeCorpse(player, kp);

            isDead = true;

            if(player.life > 0)
                player.life -= 1;

            formerLife = player.life;
            lifeText.text = "X " + player.life;

        }
    }

    // Respawn Player
    public void respawn()
    {
        if(isDead)
        {
            player.life = formerLife;

            ResetCamera();

            //background.MoveBackGroundInDeath(lastCamYPos);

            if(kp as DeathHorizonManager != null)
            {
                player.transform.position = new Vector3(player.transform.position.x, corpseYPos + 1,
                    player.transform.position.z);
            }                
            else
            {
                Vector3 originalPosition = player.transform.position;
                player.transform.position = originalPosition + new Vector3(0, respawnHeight, 0);
            }

            //player.enabled = true;
            //player.GetComponent<Renderer>().enabled = true;
            //player.GetComponent<Rigidbody2D>().isKinematic = false;

            Instantiate(respawnParticle, player.transform.position, player.transform.rotation);
            isDead = false;
        }
    }

    void Select()
    {
        catChanger.gameObject.SetActive(true);
    }

    public void FindPlayer()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseScreen.gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseScreen.gameObject.SetActive(false);
    }

    private void ResetCamera()
    {
        mainCamera.GetComponent<Camera>().orthographicSize = 3;
        mainCamera.GetComponent<CameraMove>().enabled = true;
    }
}
