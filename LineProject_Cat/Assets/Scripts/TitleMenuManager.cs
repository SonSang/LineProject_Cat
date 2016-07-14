using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleMenuManager : MonoBehaviour {

    public int stageCount;
    public int subStageCount;

    public string newGameScene;
    public string loadGameScene;
    
    public void NewGame()
    {
        for(int i = 0; i < stageCount; i++)
        {
            for(int j = 0; j < subStageCount; j++)
            {
                PlayerPrefs.SetInt("Level" + (i + 1) + "_" + (j + 1) + "Unlocked", 0);
            }
        }
        PlayerPrefs.SetInt("Level1_1Unlocked", 1);
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
