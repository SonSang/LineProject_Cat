using UnityEngine;
using System.Collections;

public class ChangeThorn_AlCatmist : MonoBehaviour
{
    public Sprite Flower;
    public float ChangeTime;
    public Sprite[] Thorn;

    private GameObject[] Thorns;
    private Sprite[] OriginThorns;

    private float StartChangingTime;
    private float NowTime;
    private bool IsChanged;

    void Start()
    {
        Thorns = GameObject.FindGameObjectsWithTag("Thorn");
        OriginThorns = new Sprite[Thorns.Length];
        for (int i = 0; i < Thorns.Length; i++)
        {
            OriginThorns[i] = Thorns[i].GetComponent<SpriteRenderer>().sprite;
        }

        IsChanged = false;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Z) || PlayerPrefs.GetString("Action") == "true") && !IsChanged)
        {
            StartChangingTime = Time.time;
            IsChanged = true;
        }

        if (IsChanged)
        {
            NowTime = Time.time;

            for (int i = 0; i < Thorns.Length; i++)
            {
                Thorns[i].GetComponent<SpriteRenderer>().sprite = Flower;
                Thorns[i].GetComponent<Collider2D>().enabled = true;
            }
        }

        if (NowTime - StartChangingTime > ChangeTime)
        {
            IsChanged = false;
                
            for (int i = 0; i < Thorns.Length; i++)
            {
                Thorns[i].GetComponent<SpriteRenderer>().sprite = OriginThorns[i];
                Thorns[i].GetComponent<Collider2D>().enabled = true;
            }
        }
    }
}
