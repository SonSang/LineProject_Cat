using UnityEngine;
using System.Collections;

public class BackGroundController : MonoBehaviour {

    private GameObject[] backgrounds;
    private Transform[] bgTransform;
    private float[] parallaxScales;
    public float moveScale;

    private Transform cam;
    private Vector3 previousCamPos;
    private Vector3 lastCamPosOnGround;

    private float backgroundTargetPosX;
    private float backgroundTargetPosY;

    public float GetBGPosY() { return backgroundTargetPosY; }

    // Use this for initialization
    void Start()
    {
        cam = Camera.main.transform;

        previousCamPos = cam.position;

        backgrounds = GameObject.FindGameObjectsWithTag("BackGround");
        bgTransform = new Transform[backgrounds.Length];

        for(int i = 0; i < backgrounds.Length; i++)
        {
            bgTransform[i] = backgrounds[i].transform;
        }

        parallaxScales = new float[bgTransform.Length];

        for (int i = 0; i < bgTransform.Length; i++)
        {
            parallaxScales[i] = bgTransform[i].transform.position.z * -moveScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float xparallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            //float yparallax = (previousCamPos.y - cam.position.y) * -Time.deltaTime;

            backgroundTargetPosX = bgTransform[i].position.x + xparallax;
            //backgroundTargetPosY = bgTransform[i].position.y + yparallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, bgTransform[i].position.y, bgTransform[i].position.z);

            bgTransform[i].position = Vector3.Lerp(bgTransform[i].position, backgroundTargetPos, Time.deltaTime);

            bgTransform[i].position = new Vector3(bgTransform[i].position.x, cam.position.y, bgTransform[i].position.z);
        }

        previousCamPos = cam.position;
    }

    /*
    public void MoveBackGroundInDeath(float lastCamYpos)
    {
        for(int i = 0; i < backgrounds.Length; i++)
        {
            bgTransform[i].position = new Vector3(bgTransform[i].position.x, lastCamYpos, bgTransform[i].position.z);
        }
    }
    */
}
