using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseManager : MonoBehaviour {

    public string mainMenu;

	// Use this for initialization
	void Start () {
        this.gameObject.SetActive(false);
	}

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
