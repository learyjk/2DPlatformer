using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{

    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing = 1;

    private Transform cam;                  //reference to MainCamera's Transform.
    private Vector3 previousCamPosition;    //position of camera in previous frame.

    //Called before Start() but after GameObjects are set up.
    void Awake()
    {
        //set up camera reference
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        previousCamPosition = cam.position;

        //Assign coresponding parallaxScales
        parallaxScales = new float[backgrounds.Length];
        for (int i=0; i < backgrounds.Length; i++) {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++) {
            //parallax is opposite of camera movement
            float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];
            
            //set target x position (current pos + parallax)
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //create a target position (background current pos with target x position)
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPosition = cam.position;
    }
    
}
