using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.Inputs;




public class TestingScript : MonoBehaviour
{
    [SerializeField]
    private InputActionProperty leftHandPositonProperty;
    [SerializeField]
    public InputActionProperty LefthandRotationProperty;

    private float leftHandPosition;
    private Quaternion leftHandRotation;

    [SerializeField]
    private InputActionProperty rightHandPositonProperty;
    [SerializeField]
    public InputActionProperty RightHandRotationProperty;

    private float rightHandPosition;
    private Quaternion rightHandRotation;


    private void Update() {
        //Print if is tracked
        //Update variables
        leftHandPosition = leftHandPositonProperty.action.ReadValue<float>();
        leftHandRotation = LefthandRotationProperty.action.ReadValue<Quaternion>();
        rightHandPosition = rightHandPositonProperty.action.ReadValue<float>();
        rightHandRotation = RightHandRotationProperty.action.ReadValue<Quaternion>();


        //Print if palm facing up
        if (leftHandRotation.eulerAngles.x > 180 && leftHandRotation.eulerAngles.x < 360) {
            Debug.Log("Left hand palm facing up");
        }
        //left facing down
        else if (leftHandRotation.eulerAngles.x > 0 && leftHandRotation.eulerAngles.x < 180) {
            Debug.Log("Left hand palm facing down");
        }
    }
}
