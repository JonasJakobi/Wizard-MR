using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DoorSnowTrigger : MonoBehaviour
{

    public Transform player;
    public float distance;
    public AudioSource audio;
    public bool played = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < distance && !played) {
            audio.Play();
            played = true;
        }

        if (Vector3.Distance(player.position, transform.position) > distance && played) {
            played = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; // Set the color of the sphere

        // Draw a sphere at the current position with the specified radius
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
