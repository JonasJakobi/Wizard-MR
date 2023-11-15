using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlaceAtHand : MonoBehaviour
{
    bool isGrabbed;
    public Transform hand;
    public Transform otherHandPinchPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=hand.position;
        transform.rotation = hand.rotation;
        /*
        if(isGrabbed && Vector3.Distance(transform.position,otherHandPinchPosition.position) > 0.1f )
        {
            Debug.Log("Distance is greater than 0.1f");
        }
        else if (isGrabbed && Vector3.Distance(transform.position,otherHandPinchPosition.position) > 0.3f )
        {
            Debug.Log("Distance is greater than 0.3f");
        }
        else if (isGrabbed && Vector3.Distance(transform.position,otherHandPinchPosition.position) > 0.5f )
        {
            Debug.Log("Distance is greater than 0.5f");
        }
        */
    }
    //if player 
    public void TouchingHand()
    {
        Debug.Log("Touching Hand");
        isGrabbed = true;
    }
    public void LeavingHand()
    {
        Debug.Log("Leaving Hand");
        isGrabbed = false;
    }
    
}
