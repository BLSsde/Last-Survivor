using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class Tilling : MonoBehaviour
{
    public int offsetX = 2;
    public bool hasARightBody = false; 
    public bool hasALeftBody = false; 
    
    public bool reverseScale = false; // used if the object is not tilable
    private float spritWidth = 0f; // width of the our element
    private Camera cam;
    private Transform myTransform;

    private void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }   
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spritWidth = sRenderer.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        // does it still need buddies? if not, do nothing
        if(hasALeftBody == false || hasARightBody == false)
        {
            //calculate the camera exted(half the width) of what the camera can see in world cordinates
            float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;

            // calculate the x position where the camera can see the edge of the sprit(element)
            float edgeVisiblePositionRight = (myTransform.position.x + spritWidth/2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (myTransform.position.x - spritWidth/2) + camHorizontalExtend;

            // checking for the edge and when need call MakeNewBuddy()
            if(cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBody == false)
            {
                MakeNewBuddy(1);
                hasARightBody = true;
            }
            else if(cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBody == false)
            {
                MakeNewBuddy(-1);
                hasALeftBody = true;
            }
        }
    }

    private void MakeNewBuddy(int rightOrLeft)
    {
        // calculating new position for our new buddy
        Vector3 newPosition = new Vector3(myTransform.position.x + spritWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        // if not tilable let's reverse the x size of our object to get rid of ugly seams
        if(reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x* -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent;
        if(rightOrLeft >0)
        {
            newBuddy.GetComponent<Tilling>().hasALeftBody = true;
        }
        else
	    {
            newBuddy.GetComponent<Tilling>().hasARightBody = true;
	    }
    }
}
