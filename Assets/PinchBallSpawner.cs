using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
/// <summary>
/// Spawns a ball when the player pinches several time
/// </summary>
public class PinchBallSpawner : MonoBehaviour
{
     [SerializeField]
    protected HandPositionInfo hand;
    [SerializeField]
    private Transform Directinteractor;

    [SerializeField]
    private float timeAllowedBetweenPinches = 1f;

    [SerializeField]
    private GameObject pinchBallPrefab;

    [SerializeField]
    private int timesPinched = 0;
  

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void Pinch(){
        Debug.Log("Pinched");
        timesPinched++;
        if(timesPinched == 1){
            StartCoroutine(ResetPinchCount());
        }
        else if(timesPinched == 2){
            SpawnPinchBall();
            timesPinched = 0;
        }
    }
    private IEnumerator ResetPinchCount(){
        yield return new WaitForSeconds(timeAllowedBetweenPinches);
        timesPinched = 0;
    }
    private void SpawnPinchBall(){
        Quaternion orientation = Directinteractor.rotation;
        //The ball goes about 70 degree too far up, so rotate it
        orientation *= Quaternion.Euler(70,0,0);
        var o = Instantiate(pinchBallPrefab, hand.GetIndexThumbTipPosition() , orientation);
        o.GetComponent<Rigidbody>().AddForce(o.transform.forward * 100);
    }
}
