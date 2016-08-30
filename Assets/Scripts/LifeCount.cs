using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LifeCount : MonoBehaviour {

    public List<Image> Hearts;
    private PlayerController player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        countHeart();
	}

    void countHeart()
    {
        player = FindObjectOfType<PlayerController>();
        for (int i = 0; i < Hearts.Count; i++)
        {
            if (i > player.life - 1) Hearts[i].gameObject.SetActive(false);
            else Hearts[i].gameObject.SetActive(true);
        }
    }
}
