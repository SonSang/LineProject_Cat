using UnityEngine;
using System.Collections;

public class ObstacleManager : KillPlayer {

    public float corpseOffset;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
}
