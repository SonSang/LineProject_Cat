using UnityEngine;
using System.Collections;

public class CatChangeManager : MonoBehaviour, FindPlayerInterface {

    private LevelManager levelManager;
    public GameObject[] cats;
    private PlayerController player;
    private FindPlayerManager findPlayer;
    private CameraMove cam;

    public void SelectCat(string catName)
    {
        if (catName == "BabyCat")
            Instantiate(cats[0], player.transform.position, player.transform.rotation).name = "BabyCat";

        if (catName == "JustCat")
            Instantiate(cats[1], player.transform.position, player.transform.rotation).name = "JustCat";

        if (catName == "RoCat")
            Instantiate(cats[2], player.transform.position, player.transform.rotation).name = "RoCat";

        GameObject mobileControl = cam.MobileControl; // for test
        Destroy(player.gameObject);
        findPlayer.FindPlayer();
        cam.MobileControl = mobileControl;            // for test
        levelManager.respawn();
        this.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
        levelManager = FindObjectOfType<LevelManager>();
        findPlayer = FindObjectOfType<FindPlayerManager>();
        player = FindObjectOfType<PlayerController>();
        cam = FindObjectOfType<CameraMove>();
	}

    public void FindPlayer()
    {
        player = FindObjectOfType<PlayerController>();
    }
}
