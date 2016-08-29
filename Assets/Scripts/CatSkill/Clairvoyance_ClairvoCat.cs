using UnityEngine;
using System.Collections;

public class Clairvoyance_ClairvoCat : MonoBehaviour
{
    private bool ClairvoyanceMode;
    private GameObject MainCamera;

    void Start()
    {
        ClairvoyanceMode = false;
        MainCamera = GameObject.Find("Main Camera");
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Z) || PlayerPrefs.GetString("Action") == "true"))
        {
            if (ClairvoyanceMode)
            {
                GetComponent<PlayerController>().enabled = true;
                MainCamera.GetComponent<Camera>().orthographicSize = 3;
                MainCamera.GetComponent<CameraMove>().enabled = true;
                ClairvoyanceMode = false;
            }
            else
            {
                GetComponent<PlayerController>().enabled = false;
                MainCamera.GetComponent<Camera>().orthographicSize = 6;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                MainCamera.GetComponent<CameraMove>().enabled = false;
                ClairvoyanceMode = true;
            }
        }

        if (ClairvoyanceMode)
        {
            if(Input.GetKey(KeyCode.RightArrow))
                MainCamera.GetComponent<Transform>().position = new Vector3(MainCamera.GetComponent<Transform>().position.x + 0.05f, MainCamera.GetComponent<Transform>().position.y, MainCamera.GetComponent<Transform>().position.z);
            else if (Input.GetKey(KeyCode.LeftArrow))
                MainCamera.GetComponent<Transform>().position = new Vector3(MainCamera.GetComponent<Transform>().position.x - 0.05f, MainCamera.GetComponent<Transform>().position.y, MainCamera.GetComponent<Transform>().position.z);
        }
    }
}
