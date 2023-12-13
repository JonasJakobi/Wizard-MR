using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    
    public AudioSource audio;
    public bool repeat = true;
    public float delayAmount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    if (repeat) {
            audio.Play();
            repeat = false;
            StartCoroutine(delay());
        }
    }

    private IEnumerator delay (){
        yield return new WaitForSeconds(delayAmount);
        repeat = true;
    }
}
