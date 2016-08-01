using UnityEngine;
using System.Collections;

public class CatChangeManager : MonoBehaviour, FindPlayerInterface {

    private LevelManager levelManager;
    public GameObject[] cats;
    private PlayerController player;
    private FindPlayerManager findPlayer;

    public void SelectCat(string catName)
    {
        if (catName == "BabyCat")
            Instantiate(cats[0], player.transform.position, player.transform.rotation).name = "BabyCat";

        if (catName == "JustCat")
            Instantiate(cats[1], player.transform.position, player.transform.rotation).name = "JustCat";

        if (catName == "RoCat")
            Instantiate(cats[2], player.transform.position, player.transform.rotation).name = "RoCat";

        GameObject mobileControl = player.MobileControl; // for test
        Destroy(player.gameObject);
        findPlayer.FindPlayer();
        player.MobileControl = mobileControl;            // for test
        levelManager.respawn();
        this.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
        levelManager = FindObjectOfType<LevelManager>();
        findPlayer = FindObjectOfType<FindPlayerManager>();
        player = FindObjectOfType<PlayerController>();
        this.gameObject.SetActive(false);
	}

    public void FindPlayer()
    {
        player = FindObjectOfType<PlayerController>();
    }
}
