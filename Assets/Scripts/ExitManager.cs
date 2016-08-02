using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ExitManager : MonoBehaviour {

    public string nextLevel;
    private bool isOnExit;

	// Use this for initialization
	void Start () {
        isOnExit = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.UpArrow) || PlayerPrefs.GetString("Interact") == "true")
        {
            PlayerPrefs.SetInt(nextLevel + "Unlocked", 1);
            if (isOnExit)
                SceneManager.LoadScene(nextLevel);
        }
	}

    void OnTriggerEnter2D(Collider2D otherr)
    {
        isOnExit = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isOnExit = false;
    }
}
