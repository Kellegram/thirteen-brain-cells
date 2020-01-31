using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour
{
    //Rigidbody rb represents whichever model this script is attached to
    public Rigidbody rb;

    //Public(editable) values------------------------------------------------------------
    public float maxTurnRate = 1.0f;//Modify how fast the tank turns
    public float turnFactor = 25.0f;//For modifying how much the turn rate is reduced by speed
    public float velocityFactor = 20.0f;//For setting speed
    //-----------------------------------------------------------------------------------

    //Private values---------------------------------------------------------------------
    Vector3 EulerAngleVelocity;
    float turnRate = 1.0f;
    //-----------------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        turnRate = maxTurnRate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateCharacter();
        //Moves character forward and backward
        //Set velocity

        if (Input.GetKey(KeyCode.W)) 
        {
            Vector3 xzTransform = transform.forward * velocityFactor;
            rb.velocity = new Vector3(xzTransform.x, rb.velocity.y, xzTransform.z);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            Vector3 xzTransform = -transform.forward * velocityFactor;
            rb.velocity = new Vector3(xzTransform.x, rb.velocity.y, xzTransform.z);
        }
        else
        {
            rb.velocity = new Vector3(0.0f, rb.velocity.y, 0.0f);
        }

        //If the tank is moving, only then run these functions
        if (rb.velocity.magnitude > 0.1f)
        {
            CalcTurnRate();
        }
        else
        {//If not moving(or barely), max out the turn rate
            turnRate = maxTurnRate;
        }
    }

    /*
    Function to rotate the camera on the Y axis
    using the horizontal mapping keys in unity
    */
    void RotateCharacter()
    {
        EulerAngleVelocity = new Vector3(0, 100, 0);
        Quaternion deltaRotation = Quaternion.Euler((EulerAngleVelocity * Input.GetAxis("Horizontal") * Time.deltaTime) * turnRate);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    /*
    The turning rate of the tank is dependent on whether the tank is moving or not. 
    This function recalculates the turn rate
     */
    void CalcTurnRate()
    {
        //Calculate how fast the tank should turn
        //The faster it goes, the slower it turns
        if (rb.velocity.magnitude != 0)//Don't divide by 0
            turnRate = turnFactor / velocityFactor;

        //Prevent the tank from turning faster while moving than it can when standing
        if (turnRate > maxTurnRate)
        {
            turnRate = maxTurnRate;
        }
        if (turnRate <= 0f)//Don't let the turn rate be negative
            turnRate = 0f;
    }
}