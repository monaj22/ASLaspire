using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float turnSpeed = 90f;

    void OnTriggerEnter(Collider other) 
    {
        // Check that the object we collided with is the player
        if (other.gameObject.name != "Player") {
            return;
        }

        // Add to the player's score


        // Destroy this coin object
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // regardless of frame rate the coin will always rotate based on time
        transform.Rotate(0,0, turnSpeed*Time.deltaTime);
    }
}
