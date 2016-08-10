using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseManager : MonoBehaviour {

    public string mainMenu;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
