using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.XR.Hands;
/// <summary>
/// Has simple to use information about the hand position
/// </summary>
public class HandPositionInfo : MonoBehaviour
{

    public bool isLeftHand;

    [SerializeField]
    public Transform wrist;

    [SerializeField]
    Transform palm;
    [Header("Index References")]
    [SerializeField]
    Transform indexIntermediate;
    [SerializeField]
    Transform indexDistal;
    [SerializeField]
    Transform indexTip;
    [Header("Middle References")]

    [SerializeField]
    Transform middleIntermediate;
    [SerializeField]
    Transform middleDistal;
    [SerializeField]
    Transform middleTip;

    [Header("Ring References")]
    [SerializeField]
    Transform ringIntermediate;
    [SerializeField]
    Transform ringDistal;
    [SerializeField]
    Transform ringTip;

    [Header("Pinky References")]
    [SerializeField]
    Transform pinkyIntermediate;
    [SerializeField]
    Transform pinkyDistal;
    [SerializeField]
    Transform pinkyTip;

    [Header("Thumb References")]
    [SerializeField]
    Transform thumbMetacarpal;
    [SerializeField]
    Transform thumbProximal;
    [SerializeField]
    Transform thumbDistal;
    [SerializeField]
    Transform thumbTip;

    [Header("Variables to tweak")]
    public float indexThumbDistanceThreshhold = 0.08f;
    public float fingersTogetherDistanceThreshhold = 0.03f;

    public float fingersFromWristDistanceThreshhold = 0.17f;

    public float nonIndexfingersToWristDistanceThreshhold = 0.11f;
    public float IndexToWristThreshhold = 0.15f;
    // Start is called before the first frame update

    private void Update() {
        if(isLeftHand) return;
        
        string log = "";
        log += CanCastTeleport() ? "Teleport " : "No Teleport ";
        Debug.Log(log);
        
    }
    public bool PalmFacingDirection(PalmDirection direction){
        Vector3 wristPalmDirection = -palm.up; 
        float alignmentThreshold = 0.9f; 
        Vector3 goal = Vector3.up;
        if(direction == PalmDirection.Down){
            goal = Vector3.down;
        }
        
        
        // Check if the palm direction is roughly aligned with the world up vector
        if (Vector3.Dot(wristPalmDirection, goal) > alignmentThreshold) {
            return true;
        } else {
            return false;
        }
    }

  
    public Transform GetPalm(){
        return palm;
    }

    public bool CanCastSlingshot(){
        return getThumbToIndexIntermediateDistance() > indexThumbDistanceThreshhold;
    }
    public bool CanCastLight(){
        return GetAverageDistanceBetweenFingers() < fingersTogetherDistanceThreshhold && GetFingersToWrist() > fingersFromWristDistanceThreshhold && PalmFacingDirection(PalmDirection.Up);
    }

    public bool CanCastTeleport(){
        //Index Finger stretched out, rest of hand balled up
        return GetFingersToWrist(new Finger[]{Finger.Middle, Finger.Ring, Finger.Pinky, Finger.Thumb}) < nonIndexfingersToWristDistanceThreshhold && GetFingersToWrist(Finger.Index) > IndexToWristThreshhold;
       
    }
    /// <summary>
    /// Calculates the summed up distance of index to middle to ring to pinky.
    /// </summary>
    /// <returns></returns>
    public float GetAverageDistanceBetweenFingers(){
        float distance = 0;
        distance += GetAverageDistanceBetweenTwoFingers(Finger.Index, Finger.Middle);
        distance += GetAverageDistanceBetweenTwoFingers(Finger.Middle, Finger.Ring);
        distance += GetAverageDistanceBetweenTwoFingers(Finger.Ring, Finger.Pinky);
        distance+= getThumbToIndexIntermediateDistance();
        return distance/4;
    }
    public float GetAverageDistanceBetweenTwoFingers(Finger f1, Finger f2){
        float distance = 0;
        distance += Vector3.Distance(GetFingerTip(f1).position, GetFingerTip(f2).position) * 2f;
        distance += Vector3.Distance(GetFingerIntermediate(f1).position, GetFingerIntermediate(f2).position) * 1.5f;
        distance += Vector3.Distance(GetFingerDistal(f1).position, GetFingerDistal(f2).position);
        return distance/4.5f;
    }
    public float getIndexThumbTipDistance(){
        return Vector3.Distance(indexTip.position, thumbTip.position);
    }
    public float getThumbToIndexIntermediateDistance(){
        return Vector3.Distance(indexIntermediate.position, thumbTip.position);
    }
    public float GetFingersToWrist(){
        return GetFingersToWrist(new Finger[]{Finger.Index, Finger.Middle, Finger.Ring, Finger.Pinky});
    }
    public float GetFingersToWrist(Finger[] fingersToCheck){
        float distance = 0;
        foreach(Finger f in fingersToCheck){
            distance += Vector3.Distance(GetFingerTip(f).position, wrist.position);
        }
        return distance/fingersToCheck.Length;
    }
    public float GetFingersToWrist(Finger fingerToCheck){
        return Vector3.Distance(GetFingerTip(fingerToCheck).position, wrist.position);
    }

    public Vector3 GetIndexThumbTipPosition(){
        //position right between index and thumb
        return (indexTip.position + thumbTip.position)/2;
    }

    public Transform GetFingerTip(Finger f){
        switch(f){
            case Finger.Index:
                return indexTip;
            case Finger.Middle:
                return middleTip;
            case Finger.Ring:
                return ringTip;
            case Finger.Pinky:
                return pinkyTip;
            case Finger.Thumb:
                return thumbTip;
        }
        return null;
    }
    public Transform GetFingerIntermediate(Finger f){
        switch(f){
            case Finger.Index:
                return indexIntermediate;
            case Finger.Middle:
                return middleIntermediate;
            case Finger.Ring:
                return ringIntermediate;
            case Finger.Pinky:
                return pinkyIntermediate;
            case Finger.Thumb:
                return thumbProximal;
        }
        return null;
    }
    public Transform GetFingerDistal(Finger f){
        switch(f){
            case Finger.Index:
                return indexDistal;
            case Finger.Middle:
                return middleDistal;
            case Finger.Ring:
                return ringDistal;
            case Finger.Pinky:
                return pinkyDistal;
            case Finger.Thumb:
                return thumbDistal;
        }
        return null;
    }
    public enum Finger{
        Index,
        Middle,
        Ring,
        Pinky,
        Thumb
    }

    public enum PalmDirection{
        Up, 
        Down


    }
}
