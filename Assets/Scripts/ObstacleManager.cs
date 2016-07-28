using UnityEngine;
using System.Collections;

public class ObstacleManager : KillPlayer {

    public float corpseOffset;

    void Start()
    {
        MakeCorpse = true;
        levelManager = FindObjectOfType<LevelManager>();
    }
}
