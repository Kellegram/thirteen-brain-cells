using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject BulletPrefab;

    float waitTillNextFire = 0f;
    public float fireSpeed = 2f;
    public float bulletForce = 200f;

    // Update is called once per frame
    void Update()
    {
        //Fire1 Mapping will trigger the shoot function
        if(Input.GetButtonDown("Fire1"))
        {
            if (waitTillNextFire <= 0)
            {
                Shoot();
                waitTillNextFire = 1;
            }
        }
        waitTillNextFire -= Time.deltaTime * fireSpeed;
    }

    void Shoot()
    {
        //Spawns a bullet at the firepoint object location and adds a force forward.
        GameObject bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.AddForce(FirePoint.forward * bulletForce, ForceMode.Impulse);
    }
}
