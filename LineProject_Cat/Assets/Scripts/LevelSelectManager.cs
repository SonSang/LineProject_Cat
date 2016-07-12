using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelectManager : MonoBehaviour {

    public Button[] stages;
    private Button selectedStage;

    private StageButtonManager stageButtonManager;

    public void LoadSelectedStage(int stage)
    {
        for(int i = 0; i < stages.Length; i++)
        {
            if (i != (stage - 1))
                Destroy(stages[i].gameObject);
            else
                selectedStage = stages[i];
        }

        stageButtonManager = selectedStage.GetComponent<StageButtonManager>();
        stageButtonManager.setSubActive();
    }

    public void LoadSelectedSubStage(string stageNum)
    {
        SceneManager.LoadScene("Level" + stageNum);
    }
}
