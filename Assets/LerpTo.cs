using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Makes the object this is attached to lerp to the goal.
/// Can also be used to rotate to a goal.
/// </summary>
public class LerpTo : MonoBehaviour
{
    public bool lerpPosition = true;
    public Transform goal;
    public float speed = 1f;
    public bool lerpRotation = false;
    public Transform goalRotation;
    public float rotationSpeed = 1f;
    
  
    // Update is called once per frame
    void Update()
    {
        if(lerpPosition){
            transform.position = Vector3.Lerp(transform.position, goal.position, speed * Time.deltaTime);
        }
        if(lerpRotation){
            transform.rotation = Quaternion.Lerp(transform.rotation, goalRotation.rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
