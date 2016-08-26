using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubStageButton : MonoBehaviour {

    public string stageCode; // form of 0-0(main-sub stage num)
    private RawImage canfoodUIZone;
    private Image[] cans;

    // when this substage button became "active", not when scene starts
    void Start()
    {
        string stageCodeForCan = "Stage" + stageCode + "_can_num_";
        canfoodUIZone = GetComponentInChildren<RawImage>();

        cans = canfoodUIZone.GetComponentsInChildren<Image>();
        SortUIImageArr(cans);

        if (!this.GetComponent<Button>().interactable)
        {
            canfoodUIZone.gameObject.SetActive(false);
            return;
        }

        if (PlayerPrefs.GetInt(stageCodeForCan) >= 0)
        {
            int canfoodNum = PlayerPrefs.GetInt(stageCodeForCan);
            canfoodUIZone = GetComponentInChildren<RawImage>();

            cans = canfoodUIZone.GetComponentsInChildren<Image>();
            SortUIImageArr(cans);

            for (int i = 0; i < cans.Length; i++)
                cans[i].color = Color.clear;

            int totalGottenCanNum = 0;

            for(int i = 0; i < canfoodNum; i++)
            {
                cans[i].color = new Color(1, 1, 1, 0.5f);
                string canTag = stageCode + "_can_" + i;
                if (PlayerPrefs.GetInt(canTag) == 1)
                    totalGottenCanNum += 1;
            }

            for(int i = 0; i < totalGottenCanNum; i++)
                cans[i].color = new Color(1, 1, 1, 1);
        }    
        
    }

    void SortUIImageArr(Image[] arr)
    {
        for (int j = 1; j < arr.Length; j++)
        {
            for (int i = 0; i < arr.Length - j; i++)
            {
                if (arr[i].rectTransform.position.x > arr[i + 1].rectTransform.position.x)
                {
                    Image temp = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = temp;
                }
            }
        }
    }
}
