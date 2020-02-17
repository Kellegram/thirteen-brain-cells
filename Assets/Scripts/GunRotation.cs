using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    Vector3 EulerAngleVelocity, pushBackEuler;

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        //Rotate 100 around the y axis
        EulerAngleVelocity = new Vector3(0, 100, 0);
        pushBackEuler = new Vector3(0, -5f, 0);

        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocity * Input.GetAxis("RotateGun") * Time.deltaTime);
        deltaRotation *= transform.rotation;
        if (transform.localEulerAngles.y < 90 || transform.localEulerAngles.y > 270)
        {
            transform.rotation = deltaRotation;
        }
        else
        {
            if (transform.localEulerAngles.y >= 90 && transform.localEulerAngles.y <= 180)
            {
                Quaternion pushBackRotation = Quaternion.Euler(pushBackEuler * Time.deltaTime);
                transform.rotation *= pushBackRotation;
            }
            else if (transform.localEulerAngles.y <= 270 && transform.localEulerAngles.y >= 260)
            {
                Quaternion pushBackRotation = Quaternion.Euler(-pushBackEuler * Time.deltaTime);
                transform.rotation *= pushBackRotation;
            }

        }
    }
}
