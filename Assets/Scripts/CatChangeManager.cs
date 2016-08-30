using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CatChangeManager : MonoBehaviour, FindPlayerInterface {

    private LevelManager levelManager;
    public GameObject[] cats;
    private PlayerController player;
    private FindPlayerManager findPlayer;
    private CameraMove cam;

    public int CatIndex { get; set; }
    public List<Button> buttonList;

    public void SelectCat(string catName)
    {
        if (catName == "BabyCat")
            Instantiate(cats[0], player.transform.position, player.transform.rotation).name = "BabyCat";

        if (catName == "JustCat")
            Instantiate(cats[1], player.transform.position, player.transform.rotation).name = "JustCat";

        if (catName == "RoCat")
            Instantiate(cats[2], player.transform.position, player.transform.rotation).name = "RoCat";

        if (catName == "AlCatmist")
            Instantiate(cats[3], player.transform.position, player.transform.rotation).name = "AlCatmist";

        if (catName == "BambooCat")
            Instantiate(cats[4], player.transform.position, player.transform.rotation).name = "BambooCat";

        if (catName == "ClairvoCat")
            Instantiate(cats[5], player.transform.position, player.transform.rotation).name = "ClairvoCat";

        if (catName == "DJCat")
            Instantiate(cats[6], player.transform.position, player.transform.rotation).name = "DJCat";

        if (catName == "WallCat")
            Instantiate(cats[7], player.transform.position, player.transform.rotation).name = "WallCat";

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
        CatIndex = 0;
	}

    void Update()
    {
        if (CatIndex == 0)
        {
            for(int i=0; i < buttonList.Count; i++)
            {
                if (i == 0 || i == 1) buttonList[i].gameObject.SetActive(true);
                else buttonList[i].gameObject.SetActive(false);
                buttonList[0].gameObject.transform.position = new Vector3(Screen.width/2,Screen.height*29/50,0);
                buttonList[1].gameObject.transform.position = new Vector3(Screen.width*39/50,Screen.height*27/50,0);

                buttonList[0].image.gameObject.transform.localScale = new Vector3(2, 2, 1);
                buttonList[1].image.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (CatIndex == cats.Length - 1)
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                if (i == buttonList.Count - 1 || i == buttonList.Count - 2) buttonList[i].gameObject.SetActive(true);
                else buttonList[i].gameObject.SetActive(false);
                buttonList[buttonList.Count - 1].gameObject.transform.position = new Vector3(Screen.width / 2, Screen.height * 29 / 50, 0);
                buttonList[buttonList.Count - 2].gameObject.transform.position = new Vector3(Screen.width * 23 / 100, Screen.height * 28 / 50, 0);

                buttonList[buttonList.Count - 1].image.gameObject.transform.localScale = new Vector3(2, 2, 1);
                buttonList[buttonList.Count - 2].image.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                if (i == CatIndex || i == CatIndex - 1 || i == CatIndex + 1) buttonList[i].gameObject.SetActive(true);
                else buttonList[i].gameObject.SetActive(false);
                buttonList[CatIndex - 1].gameObject.transform.position = new Vector3(Screen.width * 23 / 100, Screen.height * 28 / 50, 0);
                buttonList[CatIndex].gameObject.transform.position = new Vector3(Screen.width / 2, Screen.height * 29 / 50, 0);
                buttonList[CatIndex + 1].gameObject.transform.position = new Vector3(Screen.width * 39 / 50, Screen.height * 27 / 50, 0);

                buttonList[CatIndex].image.gameObject.transform.localScale = new Vector3(2, 2, 1);
                buttonList[CatIndex - 1].image.gameObject.transform.localScale = new Vector3(1, 1, 1);
                buttonList[CatIndex + 1].image.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void FindPlayer()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void MoveCatLeft()
    {
        if (CatIndex != 0) CatIndex -= 1;
    }
    public void MoveCatRight()
    {
        if (CatIndex != cats.Length - 1) CatIndex += 1;
    }
}
