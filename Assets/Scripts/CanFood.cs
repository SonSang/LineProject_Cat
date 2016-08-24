using UnityEngine;
using System.Collections;

public class CanFood : MonoBehaviour {

    private string canfoodTag;
    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void SetTag(string tag)
    {
        canfoodTag = tag;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            levelManager.GetCanFood();
            PlayerPrefs.SetInt(canfoodTag, 1);
            Destroy(this.gameObject);
        }            
    }
}
