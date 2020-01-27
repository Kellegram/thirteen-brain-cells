﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour
{
    //Rigidbody rb represents whichever model this script is attached to
    public Rigidbody rb;

    //Public(editable) values------------------------------------------------------------
    public float forwardMovement = 20f;//Modify how fast the tank accelerates
    public float slowdownRate = 0.9f;//Modify how fast the tank slows down(logarithmic)
    public float maxTurnRate = 0.5f;//Modify how fast the tank turns
    public float turnFactor = 1.0f;
    public float velocityFactor = 5.0f;
    //-----------------------------------------------------------------------------------

    //Private values---------------------------------------------------------------------
    Vector3 EulerAngleVelocity;
    float turnRate = 0.5f;
    bool moving = false;
    //-----------------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        turnRate = maxTurnRate;
        moving = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateCharacter();
        //Moves character forward and backward
        //using vertical input axes mapping in unity

        if (Input.GetKey(KeyCode.W)) 
        {
            rb.velocity = transform.forward * velocityFactor;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            rb.velocity = transform.forward * velocityFactor * (-1);
        }
        else
        {
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
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
    The turning rate of the tank is dependant on whether the tank is moving or not. 
    This function recalculates the turn rate
     */
    void CalcTurnRate()
    {
        //Calculate how fast the tank should turn
        //The faster it goes, the slower it turns
        if (rb.velocity.magnitude != 0)//Don't divide by 0
            turnRate = turnFactor / rb.velocity.magnitude;

        //Prevent the tank from turning faster while moving than it can when standing
        if (turnRate > maxTurnRate)
        {
            turnRate = maxTurnRate;
        }
        if (turnRate <= 0f)//Placeholder(?), don't let the turnrate be negative
            turnRate = 0f;
    }
}