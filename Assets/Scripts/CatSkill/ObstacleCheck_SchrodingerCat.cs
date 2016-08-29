using UnityEngine;
using System.Collections;

public class ObstacleCheck_SchrodingerCat : MonoBehaviour
{
    public bool ObstacleCheck;

    void Awake()
    {
        ObstacleCheck = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ObstacleCheck = true;
        Debug.Log("Something!");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ObstacleCheck = false;
        Debug.Log("Something Out!");
    }
}
