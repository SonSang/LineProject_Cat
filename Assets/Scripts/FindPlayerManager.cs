using UnityEngine;
using System.Collections;

public class FindPlayerManager : MonoBehaviour, FindPlayerInterface {

    public CatChangeManager catChange;
    private EnemyController[] enemy;
    private LevelManager level;

    void Start()
    {
        enemy = FindObjectsOfType<EnemyController>();
        level = FindObjectOfType<LevelManager>();
    }

    public void FindPlayer()
    {
        catChange.FindPlayer();
        level.FindPlayer();
        for(int i = 0; i < enemy.Length; i++)
        {
            enemy[i].FindPlayer();
        }
    }
}
