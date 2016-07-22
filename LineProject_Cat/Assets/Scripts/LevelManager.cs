using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour {
   
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

    private KillPlayer kp;

    public Text lifeText;

    public bool IsPlayerDead { get { return isDead; } }

	// Use this for initialization
	void Start () {
        isDead = false;
        player = FindObjectOfType<PlayerController>();
        deathManager = FindObjectOfType<DeathManager>();
        lifeText.text = "X " + player.life;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void respawnPlayer(KillPlayer kp)
    {
        this.kp = kp;
        StartCoroutine("respawnPlayerCo");
    }

    public IEnumerator respawnPlayerCo()
    {
        kill();
        yield return new WaitForSeconds(respawnDelay);
        respawn();
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
            deathManager.makeCorpse(player, kp);

            isDead = true;

            if(player.life > 0)
                player.life -= 1;

            lifeText.text = "X " + player.life;
        }
    }

    // Respawn Player
    void respawn()
    {
        if(isDead)
        {
            Vector3 originalPosition = player.transform.position;
            player.transform.position = originalPosition + new Vector3(0, respawnHeight, 0);

            player.enabled = true;
            player.GetComponent<Renderer>().enabled = true;
            player.GetComponent<Rigidbody2D>().isKinematic = false;

            Instantiate(respawnParticle, player.transform.position, player.transform.rotation);
            isDead = false;
        }
    }
    

}
