using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour
{
    public int offsetX = 2;

    public bool hasRightBuddy = false;
    public bool hasLeftBuddy = false;

    //used for non-tilable object
    public bool reverseScale = false;

    private float spriteWidth = 0f;

    private Camera cam;
    private Transform myTransform;

    void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLeftBuddy == false || hasRightBuddy == false) {
            //calc camera's extend (half width) of what camera can see in world coordinates.
            float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;

            //calc the x position where camera can see edge of sprite.
            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth/2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth/2) + camHorizontalExtend;

            if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasRightBuddy == false) {
                MakeNewBuddy(1);
                hasRightBuddy = true;
            }
            else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasLeftBuddy == false) {
                MakeNewBuddy(-1);
                hasLeftBuddy = true;
            }
        }
    }

    void MakeNewBuddy(int rightOrLeft) 
    {
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        //mirrors the object if not tilable
        if (reverseScale == true) {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent;

        if (rightOrLeft > 0) {
            newBuddy.GetComponent<Tiling>().hasLeftBuddy = true;
        }
        else {
            newBuddy.GetComponent<Tiling>().hasRightBuddy = true;
        }
    }
}
