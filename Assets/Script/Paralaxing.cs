using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralaxing : MonoBehaviour
{
    // not in use
    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing = 1f;
    private Transform cam;
    private Vector3 previusCamPos;
    private void Awake()
    {
        cam = Camera.main.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        previusCamPos = cam.position;

        // assingning corresponding parallax scales
        parallaxScales = new float[backgrounds.Length];
        for( int i=0; i<backgrounds.Length; i++)
        {
            parallaxScales[i]= backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for( int i=0; i<backgrounds.Length; i++)
        {
            float parallax = (previusCamPos.x - cam.position.x) * parallaxScales[i];

            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3 ( backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing* Time.deltaTime);

        }
        
        // set previus cam pos to the camera's position at the end of the frame
        previusCamPos = cam.position;
    }
}
