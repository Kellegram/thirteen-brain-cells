using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    //amount of damage bullet does, can be changed in editor
    public float damage = 50f;
    private void Start()
    {
        //Destroys bullet if it didn't hit any objects within 2 seconds
        Destroy(gameObject, 2f);
    }

    /*
    OnTriggerEnter()
    in this script will detect when the bullet collides with an object.
    if the object contains a PlayerHealth component. If it does, it will
    reduce the value of the health by the damage variable.

    The bullet (gameObject) is then destroyed immediately.
    //*/
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<AudioManager>().Play("explosion");
        //If the bullet hit a player, this will detect that
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.ReduceHealth(damage);
        }

        
        //Instantiate(HitEffect, transform.position, Quaternion.identity); (placeholder for a hit explosion or effect)

        //Destroy the bullet
        Destroy(gameObject);
    }

}
