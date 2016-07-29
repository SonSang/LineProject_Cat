using UnityEngine;
using System.Collections;

public class FindPlayerManager : MonoBehaviour, FindPlayerInterface {

    private CatChangeManager catChange;
    public EnemyController[] enemy;
    private LevelManager level;

    void Start()
    {
        catChange = FindObjectOfType<CatChangeManager>();
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
