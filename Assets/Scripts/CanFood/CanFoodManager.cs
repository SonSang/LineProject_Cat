using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanFoodManager : MonoBehaviour {

    // About the stage's can food acquisition
    public string stageNum;
    private GameObject[] canfoodArr;
    public int canfoodNum;
    private int gottenCanNum = 0;
    private RawImage canfoodUIZone;
    private Image[] cans;

    void Awake()
    {
        SetCanFoodTag();
    }

    // set "Stage0-0_can_num_"(total number of can on the level), "0-0_can_0"(code for each of cans)
    void SetCanFoodTag()
    {
        canfoodArr = GameObject.FindGameObjectsWithTag("CanFood");

        canfoodNum = canfoodArr.Length;
        PlayerPrefs.SetInt("Stage" + stageNum + "_can_num_", canfoodNum);

        SortArr(canfoodArr);
        string[] canfoodTagStr = new string[canfoodArr.Length];
        for (int i = 0; i < canfoodTagStr.Length; i++)
        {
            canfoodTagStr[i] = stageNum + "_can_" + i;                      // set variable about can food!!!
            canfoodArr[i].GetComponent<CanFood>().SetTag(canfoodTagStr[i]);
        }
    }

    void SortArr(GameObject[] arr)
    {
        for (int j = 1; j < arr.Length; j++)
        {
            for (int i = 0; i < arr.Length - j; i++)
            {
                if (arr[i].transform.position.x > arr[i + 1].transform.position.x)
                {
                    GameObject temp = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = temp;
                }
            }
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

    public void SetUI(RawImage can)
    {
        canfoodUIZone = can;
        cans = canfoodUIZone.GetComponentsInChildren<Image>();
        SortUIImageArr(cans);
        for (int i = 0; i < cans.Length; i++)
            cans[i].color = Color.clear;
        for (int i = 0; i < canfoodNum; i++)
            cans[i].color = new Color(1, 1, 1, 0.5f);
    }

    public void GetCanFood()
    {
        cans[gottenCanNum++].color = new Color(1, 1, 1, 1);
    }
}
