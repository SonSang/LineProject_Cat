using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleMenuManager : MonoBehaviour {

    public string newGameScene;
    public string loadGameScene;
    
    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(loadGameScene);
    }

    public void Settings()
    {
        Debug.Log("Setting panel opened");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
