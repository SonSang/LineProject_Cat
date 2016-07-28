using UnityEngine;
using System.Collections;

public class DeathHorizonManager : KillPlayer {

	// Use this for initialization
	void Start () {
        MakeCorpse = false;
        levelManager = FindObjectOfType<LevelManager>();
    }
}
