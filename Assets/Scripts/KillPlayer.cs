using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

    protected LevelManager levelManager;
    private bool makeCorpse;

    public bool MakeCorpse { get { return makeCorpse; } set { makeCorpse = value; } }

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            levelManager.respawnPlayer(this);
    }
}
