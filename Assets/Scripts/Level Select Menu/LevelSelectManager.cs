using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelectManager : MonoBehaviour {

    public Button[] stages;
    public string levelSelectMenu;
    public string mainMenu;

    private Button selectedStage;

    private StageButtonManager stageButtonManager;

    private bool isStageSelected;

    private bool loadScene = false;

    [SerializeField]
    private Text loadingText;

    void Start()
    {
        isStageSelected = false;
    }

    void Update()
    {
        if(loadScene)
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void LoadSelectedStage(int stage)
    {
        if(isStageSelected == false)
        {
            for (int i = 0; i < stages.Length; i++)
            {
                if (i != (stage - 1))
                    Destroy(stages[i].gameObject);
                else
                    selectedStage = stages[i];
            }

            stageButtonManager = selectedStage.GetComponent<StageButtonManager>();
            stageButtonManager.setSubActive();
            isStageSelected = true;
        }
        else
        {
            SceneManager.LoadScene(levelSelectMenu);
        }
    }

    // subStageCode = "mainstagenum"+"substagenum"
    public void LoadSelectedSubStage(string subStageCode)
    {
        loadingText.text = "로딩중...";
        StartCoroutine(LoadScene(subStageCode));
    }

    IEnumerator LoadScene(string subStageCode)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("Stage" + subStageCode);

        while (!async.isDone)
            yield return null;
    }
}
