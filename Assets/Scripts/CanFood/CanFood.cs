using UnityEngine;
using System.Collections;

public class CanFood : MonoBehaviour {

    private string canfoodTag;
    private CanFoodManager canFoodManager;

    void Start()
    {
        canFoodManager = FindObjectOfType<CanFoodManager>();
    }

    public void SetTag(string tag)
    {
        canfoodTag = tag;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canFoodManager.GetCanFood();
            PlayerPrefs.SetInt(canfoodTag, 1);
            Destroy(this.gameObject);
        }            
    }
}
