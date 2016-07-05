using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

    private LevelManager levelManager;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        levelManager.respawnPlayer();
    }
}
