using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //Firepoint is set in the editor, and it represents where a bullet spawns from when shot.
    public Transform FirePoint;

    //BulletPrefab is also set in the editor, and represents a prefabrication of a bullet object.
    public GameObject BulletPrefab;

    //bulletForce is the speed at which the bullet comes out
    public float bulletForce = 80f;

    //These variables are for limiting fire rate of the player
    float waitTillNextFire = 0f;
    public float fireSpeed = 2f;

    /*
     * Start() is called when the object with this script is instatiated.
     * This function will set the animation for the gun barrel to false.
     */
    private void Start()
    {
        GameObject.Find("harambe_animation").GetComponent<Animator>().SetBool("ReloadBool", false);
    }

    /*
     * Update() is called every frame
     * This script will handle what happens when the player tries to shoot,
     * and trigger the appropriate reload animation etc...
     */
    void Update()
    {
        //Fire1 Mapping will trigger the shoot function
        if (Input.GetButtonDown("Fire1"))
        {
            if (waitTillNextFire <= 0)
            {
                FindObjectOfType<AudioManager>().Play("FireSound");
                Shoot();
                waitTillNextFire = 1;
            }
        }

        if (waitTillNextFire <= 0)
        {
            GameObject.Find("harambe_animation").GetComponent<Animator>().SetBool("ReloadBool", false);
        }
        if (waitTillNextFire > 0)
        {
            //FIRESPEED IS A PUBLIC VARIABLE SET IN EDITOR TO LIMIT FIRE RATE.
            waitTillNextFire -= Time.deltaTime * fireSpeed;
            GameObject.Find("harambe_animation").GetComponent<Animator>().SetBool("ReloadBool", true);
        }
    }

    /*
     * Shoot() will instatiate a bullet object and add force to it.
     */
    void Shoot()
    {
        //Spawns a bullet at the firepoint object location and adds a force forward.
        GameObject bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.AddForce(FirePoint.forward * bulletForce, ForceMode.Impulse);
    }
}
