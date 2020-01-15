using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    Vector3 EulerAngleVelocity;

    //Changeable value forwardMovement is the speed at which the character moves
    public float forwardMovement = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        EulerAngleVelocity = new Vector3(0, 100, 0);
        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocity * Input.GetAxis("RotateGun") * Time.deltaTime);
        transform.rotation = transform.rotation * deltaRotation;
    }
}
