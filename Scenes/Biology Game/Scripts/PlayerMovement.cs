
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5; 
    public Rigidbody rb; 
    float horizontalInput;
    public float horizontalMultiplier = 2;


    void FixedUpdate(){


        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * horizontalMultiplier * Time.fixedDeltaTime ;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
        rb.MovePosition(rb.position + forwardMove);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        
    }
}
