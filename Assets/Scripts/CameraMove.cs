using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour, FindPlayerInterface {

    private PlayerController player;

    public float xOffset;
    public float yOffset;
    public float camSpeed;      
    private Vector3 camPos;

	// Use this for initialization
	void Start () {
        FindPlayer();
    
	}
	
	// Update is called once per frame
	void Update () {
        camPos = transform.position;
        Debug.DrawLine(new Vector3(camPos.x - xOffset, camPos.y), new Vector3(camPos.x + xOffset, camPos.y), Color.red);
        Debug.DrawLine(new Vector3(camPos.x, camPos.y - yOffset), new Vector3(camPos.x, camPos.y + yOffset), Color.red);

        if(transform.position != player.transform.position)
        {
            transform.position = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y),
                new Vector2(player.transform.position.x, player.transform.position.y), Time.deltaTime * camSpeed);
            transform.position = new Vector3(transform.position.x, transform.position.y, camPos.z);
        }
    }


    public void FindPlayer()
    {
        player = FindObjectOfType<PlayerController>();
    }
}
