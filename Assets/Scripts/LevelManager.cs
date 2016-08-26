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
    private PauseManager pause;
    private BackGroundController background;

    // UIs
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

    // About the stage's can food acquisition
    public string stageNum;
    private GameObject[] canfoodArr;
    public int canfoodNum;
    private int gottenCanNum = 0;
    private RawImage canfoodUIZone;
    private Image[] cans;

    void Awake()
    {
        SetCanFoodTag();
    }

	// Use this for initialization
	void Start () {
        isDead = false;
        player = FindObjectOfType<PlayerController>();
        deathManager = FindObjectOfType<DeathManager>();
        pause = FindObjectOfType<PauseManager>();
        background = FindObjectOfType<BackGroundController>();
        catDie = GetComponent<AudioSource>();
        PlayerPrefs.SetString("Pause", "false");
    }

    // set "Stage0-0_can_num_"(total number of can on the level), "0-0_can_0"(code for each of cans)
    void SetCanFoodTag()
    {
        canfoodArr = GameObject.FindGameObjectsWithTag("CanFood");

        canfoodNum = canfoodArr.Length;
        PlayerPrefs.SetInt("Stage" + stageNum + "_can_num_", canfoodNum);

        SortArr(canfoodArr);
        string[] canfoodTagStr = new string[canfoodArr.Length];
        for(int i = 0; i < canfoodTagStr.Length; i++)
        {
            canfoodTagStr[i] = stageNum + "_can_" + i;                      // set variable about can food!!!
            canfoodArr[i].GetComponent<CanFood>().SetTag(canfoodTagStr[i]);
        }
    }

    void SortArr(GameObject[] arr)
    {
        for (int j = 1; j < arr.Length; j++)
        {
            for (int i = 0; i < arr.Length - j; i++)
            {
                if (arr[i].transform.position.x > arr[i + 1].transform.position.x)
                {
                    GameObject temp = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = temp;
                }
            }
        }
    }

    void SortUIImageArr(Image[] arr)
    {
        for (int j = 1; j < arr.Length; j++)
        {
            for (int i = 0; i < arr.Length - j; i++)
            {
                if (arr[i].rectTransform.position.x > arr[i + 1].rectTransform.position.x)
                {
                    Image temp = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = temp;
                }
            }
        }
    }

    public void SetUI(CatChangeManager catUI, GameOverManager gameoverUI, PauseManager pauseUI, Text lifeT, RawImage can)
    {
        catChanger = catUI;
        gameOver = gameoverUI;
        pauseScreen = pauseUI;
        player = FindObjectOfType<PlayerController>();

        lifeText = lifeT;
        lifeText.text = "X " + player.life;

        canfoodUIZone = can;
        cans = canfoodUIZone.GetComponentsInChildren<Image>();
        SortUIImageArr(cans);
        for (int i = 0; i < cans.Length; i++)
            cans[i].color = Color.clear;
        for (int i = 0; i < canfoodNum; i++)
            cans[i].color = new Color(1, 1, 1, 0.5f);        
    }

    public void GetCanFood()
    {
        cans[gottenCanNum++].color = new Color(1, 1, 1, 1);
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
}
