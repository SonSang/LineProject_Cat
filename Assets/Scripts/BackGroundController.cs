using UnityEngine;
using System.Collections;

public class BackGroundController : MonoBehaviour {

    private GameObject[] backgrounds;
    private Transform[] bgTransform;
    private float[] parallaxScales;
    public float moveScale;
    public float smoothing;

    private Transform cam;
    private Vector3 previousCamPos;

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
            float yparallax = (previousCamPos.y - cam.position.y) * parallaxScales[i];

            float backgroundTargetPosX = bgTransform[i].position.x + xparallax;
            float backgroundTargetPosY = bgTransform[i].position.y + yparallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, bgTransform[i].position.z);

            bgTransform[i].position = Vector3.Lerp(bgTransform[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPos = cam.position;
    }
}
