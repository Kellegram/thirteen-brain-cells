using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    Vector3 EulerAngleVelocity;

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        //Rotate 100 around the y axis
        EulerAngleVelocity = new Vector3(0, 100, 0);
        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocity * Input.GetAxis("RotateGun") * Time.deltaTime);
        transform.rotation = transform.rotation * deltaRotation;
    }
}
