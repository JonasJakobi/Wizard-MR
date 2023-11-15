using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBall : MonoBehaviour
{

    private float shrinkGrowSpeed = 7f;
    [SerializeField]
    private float lightBallMinSize = 0.1f;
    [SerializeField]
    private float lightBallMaxSize = 0.5f;
    
    private bool expand = true;
    private void Start() {
        transform.localScale = Vector3.one * lightBallMinSize;
    }
    private void Update() {
        if(expand){
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * lightBallMaxSize, shrinkGrowSpeed * Time.deltaTime);
        }else{
            if(transform.localScale.x < lightBallMinSize + 0.0002f){
                Destroy(gameObject);
            }
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 0, shrinkGrowSpeed * Time.deltaTime);
        }
    }

    public void SetExpand(bool expand){
        this.expand = expand;
    }

}
