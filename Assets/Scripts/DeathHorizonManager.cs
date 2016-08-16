using UnityEngine;
using System.Collections;

public class DeathHorizonManager : KillPlayer {

    public float respawnHeight;

	// Use this for initialization
	void Start () {
        levelManager = FindObjectOfType<LevelManager>();
    }
}
