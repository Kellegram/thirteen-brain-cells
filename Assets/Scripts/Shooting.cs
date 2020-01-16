using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform FirePoint;
    public GameObject BulletPrefab;

    public float bulletForce = 200f;

    //This float isn't used yet but is shown in the editor. It may be used later.
    public float damage = 10f;

    // Update is called once per frame
    void Update()
    {
        //Fire1 Mapping will trigger the shoot function
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.AddForce(FirePoint.forward * bulletForce, ForceMode.Impulse);
    }
}
