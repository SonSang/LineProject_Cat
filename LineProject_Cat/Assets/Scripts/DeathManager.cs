using UnityEngine;
using System.Collections;

public class DeathManager : MonoBehaviour {

    private GameObject corpse;

    // Create corpse when player die
	public void makeCorpse(PlayerController player, KillPlayer kp)
    {
        corpse = player.corpse;
        GameObject c = Instantiate(corpse, player.transform.position, player.transform.rotation) as GameObject;

        c.transform.localScale = player.transform.localScale;

        // if player was killed by obstacle
        if (kp as ObstacleManager != null)
        {
            ObstacleManager obstacle = kp as ObstacleManager;
            c.transform.parent = obstacle.transform;
            Vector3 corpseLP = c.transform.localPosition;
            c.transform.localPosition = new Vector3(corpseLP.x, obstacle.corpseOffset, corpseLP.z);
        }
        // if player was killed by moving enemy
        else if(kp as EnemyController != null)
        {
        }
    }
}
