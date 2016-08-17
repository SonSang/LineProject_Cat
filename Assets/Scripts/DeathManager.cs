using UnityEngine;
using System.Collections;

public class DeathManager : MonoBehaviour {

    private GameObject[] edgeSpot;
    private float respawnHeight;

    private GameObject corpse;

    void Start()
    {
        edgeSpot = GameObject.FindGameObjectsWithTag("EdgeSpot");
        SortEdgeSpot();
    }

    // Create corpse when player die
	public float makeCorpse(PlayerController player, KillPlayer kp)
    {
        corpse = player.corpse;

        if (kp as DeathHorizonManager != null)
        {
            for(int i = 0; i < edgeSpot.Length; i++)
            {
                if(edgeSpot[i].transform.position.x < player.transform.position.x && edgeSpot[i+1].transform.position.x > player.transform.position.x)
                {
                    respawnHeight = (edgeSpot[i].transform.position.y + edgeSpot[i + 1].transform.position.y) / 2 - 0.5f;
                    break;
                }
            }
            Vector3 corpsePos = new Vector3(player.transform.position.x, respawnHeight, player.transform.position.z);
            (Instantiate(corpse, corpsePos, player.transform.rotation) as GameObject).transform.localScale = player.transform.localScale;
            return respawnHeight;
        }

        GameObject c = Instantiate(corpse, player.transform.position, player.transform.rotation) as GameObject;

        c.transform.localScale = player.transform.localScale;

        return c.transform.position.y;
    }

    void SortEdgeSpot()
    {
        Debug.Log("Start sorgin");
        for(int j = 1; j < edgeSpot.Length; j++)
        {
            for (int i = 0; i < edgeSpot.Length - j; i++)
            {
                if (edgeSpot[i].transform.position.x > edgeSpot[i + 1].transform.position.x)
                {
                    Debug.Log("Change");
                    GameObject temp = edgeSpot[i];
                    edgeSpot[i] = edgeSpot[i + 1];
                    edgeSpot[i + 1] = temp;
                }
            }
        }
    }
}
