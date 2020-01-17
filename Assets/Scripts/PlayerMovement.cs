using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Rigidbody rb represents whichever model this script is attached to
    public Rigidbody rb;

    //Public(editable) values
    public float forwardMovement = 1000f;//Modify how fast the tank accelerates
    public float slowdownRate = 0.9f;//Modify how fast the tank slows down(logarithmic)

    Vector3 EulerAngleVelocity;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateCharacter();
        //Moves character forward and backward
        //using vertical input axes mapping in unity
        rb.AddForce(Input.GetAxis("Vertical") * transform.forward * forwardMovement * Time.deltaTime, ForceMode.VelocityChange);
        Deceleration();
    }

    /*
    Function to rotate the camera on the Y axis
    using the horizontal mapping keys in unity
    */
    void RotateCharacter()
    {
        if (Input.GetAxis("Vertical") < 0.2f && Input.GetAxis("Vertical") > -0.2f)
        {
            EulerAngleVelocity = new Vector3(0, 100, 0);
            Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocity * Input.GetAxis("Horizontal") * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
       
    }
    /*
    Function to slow down the tank exponentially to allow
    faster turns and less slippery behavior
    */
    void Deceleration()
    {
        //If not applying forward/backward force then decelerate the vehicle
        if (Input.GetAxis("Vertical") < 0.1f && Input.GetAxis("Vertical") > -0.1f)
        {
            rb.velocity = new Vector3(rb.velocity.x * slowdownRate, rb.velocity.y, rb.velocity.z * slowdownRate);
        }
    }
}