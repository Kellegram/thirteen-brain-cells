using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Rigidbody rb represents whichever model this script is attached to
    public Rigidbody rb;

    //Changeable value forwardMovement is the speed at which the character moves
    public float forwardMovement = 1000f;

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
    }

    /*
    Function to rotate the camera on the Y axis
    using the horizontal mapping keys in unity
    */
    void RotateCharacter()
    {
        if (Input.GetAxis("Vertical") < 0.1f && Input.GetAxis("Vertical") > -0.1f)
        {
            EulerAngleVelocity = new Vector3(0, 100, 0);
            Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocity * Input.GetAxis("Horizontal") * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }

}
