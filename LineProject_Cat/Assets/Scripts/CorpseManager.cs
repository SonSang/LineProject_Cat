using UnityEngine;
using System.Collections;

public class CorpseManager : MonoBehaviour {

    private GameObject corpse;

    // Create corpse when player die
	public void makeCorpse(PlayerController player)
    {
        corpse = player.corpse;
        Instantiate(corpse, player.transform.position, player.transform.rotation);
    }
}
