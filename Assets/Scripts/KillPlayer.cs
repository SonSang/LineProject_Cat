using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

    protected LevelManager levelManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            levelManager.respawnPlayer(this);
    }
}
