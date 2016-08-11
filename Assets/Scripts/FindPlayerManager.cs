using UnityEngine;
using System.Collections;

public class FindPlayerManager : MonoBehaviour, FindPlayerInterface {

    private CatChangeManager catChange;
    private EnemyController[] enemy;
    private LevelManager level;
    private CameraMove camera;

    void Start()
    {
        enemy = FindObjectsOfType<EnemyController>();
        level = FindObjectOfType<LevelManager>();
        camera = FindObjectOfType<CameraMove>();
    }

    public void SetUI(CatChangeManager cat)
    {
        catChange = cat;
    }

    public void FindPlayer()
    {
        catChange.FindPlayer();
        level.FindPlayer();
        for(int i = 0; i < enemy.Length; i++)
        {
            enemy[i].FindPlayer();
        }
        camera.FindPlayer();
    }
}
