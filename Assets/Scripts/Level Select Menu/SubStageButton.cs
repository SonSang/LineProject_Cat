using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubStageButton : MonoBehaviour {

    private RawImage canfoodUIZone;
    private Image[] cans;

    void Start()
    {
        canfoodUIZone = GetComponentInChildren<RawImage>();
        
        cans = canfoodUIZone.GetComponentsInChildren<Image>();
        SortUIImageArr(cans);



        if (!this.GetComponent<Button>().interactable)
        {
            canfoodUIZone.gameObject.SetActive(false);
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
