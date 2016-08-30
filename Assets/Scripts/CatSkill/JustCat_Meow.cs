using UnityEngine;
using System.Collections;

public class JustCat_Meow : MonoBehaviour {

    private AudioSource[] catSE;
    private AudioSource MeowSE;

    // Use this for initialization
    void Start () {
        catSE = GetComponents<AudioSource>();
        for (int i = 0; i < catSE.Length; i++)
        {
            if (catSE[i].clip.name == "Cat_Meow")
                MeowSE = catSE[i];
        }
    }
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKeyDown(KeyCode.Z) || PlayerPrefs.GetString("Action") == "true"))
        {
            MeowSE.Play();
        }
    }
}
