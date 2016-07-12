using UnityEngine;
using System.Collections;

public class StageButtonManager : MonoBehaviour {

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
        for (int i = 0; i < substages.Length; i++)
        {
            substages[i].SetActive(true);
        }
    }
}
