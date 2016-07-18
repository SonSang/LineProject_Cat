using UnityEngine;
using System.Collections;

public class ObstacleManager : KillPlayer {

    public float corpseOffset;

    private LevelManager levelManager;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
            levelManager.respawnPlayer(this);
    }
}
