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

    void Start()
    {
        isStageSelected = false;
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
        SceneManager.LoadScene("Stage" + subStageCode);
    }
}
