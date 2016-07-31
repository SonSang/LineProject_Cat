using UnityEngine;
using System.Collections;

public class DeathManager : MonoBehaviour {

    private GameObject corpse;

    // Create corpse when player die
	public void makeCorpse(PlayerController player, KillPlayer kp, float lastYPos)
    {
        corpse = player.corpse;

        if((kp as DeathHorizonManager) != null)
        {
            Vector3 corpsePos = new Vector3(player.transform.position.x, lastYPos - 1, player.transform.position.z);
            (Instantiate(corpse, corpsePos, player.transform.rotation) as GameObject).transform.localScale = player.transform.localScale;
            return;
        }

        GameObject c = Instantiate(corpse, player.transform.position, player.transform.rotation) as GameObject;

        c.transform.localScale = player.transform.localScale;
    }
}
