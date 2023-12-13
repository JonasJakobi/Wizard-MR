using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class igniteCandle : MonoBehaviour
{
    public GameObject candle;
    public Transform player;
    public float distance;
    public AudioSource candleSound;
    public bool lit = true;
    public MagicManager magicManagerLeft;
    public MagicManager magicManagerRight;
    public float lightingDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < distance && lit) {
            candle.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
            candle.transform.GetChild(2).GetComponent<Light>().enabled = false;
            candleSound.Play();
            lit = false;
        }

        GameObject lightBallLeft = magicManagerLeft.GetSpellInstance();
        GameObject lightBallRight = magicManagerRight.GetSpellInstance();
        if (Vector3.Distance(lightBallLeft.transform.position, candle.transform.GetChild(0).position) < lightingDistance || Vector3.Distance(lightBallRight.transform.position, candle.transform.GetChild(0).position) < lightingDistance&& !lit) {
            candle.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
            candle.transform.GetChild(2).GetComponent<Light>().enabled = true;
            StartCoroutine(delay());            
        }

    }
    
    private IEnumerator delay (){
        yield return new WaitForSeconds(30);
        lit = true;
    }

}
