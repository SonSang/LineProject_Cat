using UnityEngine;
using System.Collections;

public class CatChangeManager : MonoBehaviour {

    private PauseManager pause;
    public GameObject[] cats;
    private PlayerController player;
    private float originalTimeScale;

    public void SelectCat(string catName)
    {
        //Destroy(player.gameObject);
        if (catName == "BabyCat")
            Instantiate(cats[0], player.transform.position, player.transform.rotation);

        if (catName == "JustCat")
            Instantiate(cats[1], player.transform.position, player.transform.rotation);

        if (catName == "RoCat")
            Instantiate(cats[2], player.transform.position, player.transform.rotation);

        Destroy(player.gameObject);
        player = FindObjectOfType<PlayerController>();
        pause.Resume();
        this.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
        pause = FindObjectOfType<PauseManager>();
        originalTimeScale = Time.timeScale;
        Debug.Log(Time.timeScale);
        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
