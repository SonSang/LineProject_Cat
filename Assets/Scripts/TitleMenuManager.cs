using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleMenuManager : MonoBehaviour {

    public int stageCount;
    public int subStageCount;

    public string newGameScene;
    public string loadGameScene;

    private bool loadScene = false;
    [SerializeField]
    private Text loadingText;

    
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
        loadingText.text = "로딩중...";
        StartCoroutine(LoadScene(newGameScene));
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

    void Update()
    {
        if (loadScene)
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
    }

    IEnumerator LoadScene(string sceneToLoad)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!async.isDone)
            yield return null;
    }
}
