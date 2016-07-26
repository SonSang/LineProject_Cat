using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageButtonManager : MonoBehaviour {

    public int stageNum; // number of this stage
    public GameObject[] substages;

	// Use this for initialization
	void Start () {
	    for(int i = 0; i < substages.Length; i++)
        {
            substages[i].SetActive(false);
        }
	}
	
	public void setSubActive()
    {
        checkIfUnlocked();
        for (int i = 0; i < substages.Length; i++)
        {
            substages[i].SetActive(true);
        }
    }

    // PlayerPrefs for unlocked substage is like "Level1_1Unlocked"
    // If it is not 1, the substage is not unlocked.
    private void checkIfUnlocked()
    {
        int subStageNum = substages.Length;
        for(int i = 0; i < subStageNum; i++)
        {
            if(PlayerPrefs.GetInt("Level" + stageNum + "_" + (i + 1) + "Unlocked") != 1)
            {
                Color c = substages[i].GetComponent<Image>().color;
                substages[i].GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0.5f);
                substages[i].GetComponent<Button>().interactable = false;
            }
        }
    }
}
