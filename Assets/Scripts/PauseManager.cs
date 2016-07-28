using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour {

    private float originalTimeScale;
    private bool isPaused;

	// Use this for initialization
	void Start () {
        originalTimeScale = Time.timeScale;
	}
	
	// Update is called once per frame
	void Update () {
        if (isPaused == true)
            Time.timeScale = 0f;
        else
            Time.timeScale = originalTimeScale;        
	}

    public void Pause()
    {
        isPaused = true;
    }

    public void Resume()
    {
        isPaused = false;
    }
}
