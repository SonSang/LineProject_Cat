using UnityEngine;
using System.Collections;

public class DeathManager : MonoBehaviour {

    private GameObject corpse;

    // Create corpse when player die
	public float makeCorpse(PlayerController player, KillPlayer kp)
    {
        corpse = player.corpse;

        DeathHorizonManager horizon = kp as DeathHorizonManager;

        if (horizon != null)
        {
            Vector3 corpsePos = new Vector3(player.transform.position.x, horizon.respawnHeight, player.transform.position.z);
            (Instantiate(corpse, corpsePos, player.transform.rotation) as GameObject).transform.localScale = player.transform.localScale;
            return horizon.respawnHeight;
        }

        GameObject c = Instantiate(corpse, player.transform.position, player.transform.rotation) as GameObject;

        c.transform.localScale = player.transform.localScale;

        return c.transform.position.y;
    }
}
