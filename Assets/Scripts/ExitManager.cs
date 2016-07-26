using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ExitManager : MonoBehaviour {

    public string levelSelectMenu;
    public string nextLevel;
    private bool isOnExit;

	// Use this for initialization
	void Start () {
        isOnExit = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            PlayerPrefs.SetInt(nextLevel + "Unlocked", 1);
            if (isOnExit)
                SceneManager.LoadScene(levelSelectMenu);
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
