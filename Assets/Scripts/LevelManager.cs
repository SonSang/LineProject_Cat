using UnityEngine;
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
    public CatChangeManager catChanger;
    private PauseManager pause;
    public GameOverManager gameOver;

    private KillPlayer kp;

    public Text lifeText;

    public bool IsPlayerDead { get { return isDead; } }

    private float lastYPos;

    private int formerLife;

	// Use this for initialization
	void Start () {
        isDead = false;
        player = FindObjectOfType<PlayerController>();
        deathManager = FindObjectOfType<DeathManager>();
        //catChanger = FindObjectOfType<CatChangeManager>();
        pause = FindObjectOfType<PauseManager>();
        lifeText.text = "X " + player.life;
    }
	
	// Update is called once per frame
	void Update () {
        if(!isDead)
        {
            if (player.isGrounded)
                lastYPos = player.transform.position.y;
        }
	}

    public void respawnPlayer(KillPlayer kp)
    {
        if (player.life > 1)
        {
            this.kp = kp;
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

            player.enabled = false;
            player.GetComponent<Renderer>().enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().isKinematic = true;
            deathManager.makeCorpse(player, kp, lastYPos);

            isDead = true;

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

            if(kp as DeathHorizonManager != null)
                player.transform.position = new Vector3(player.transform.position.x, lastYPos, 
                    player.transform.position.z);
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
}
