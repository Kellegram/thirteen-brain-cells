using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    //amount of damage bullet does, can be changed in editor
    public float damage = 50f;

    /*
     * Start() begins when bullet object is spawned
     * Once the bullet spawns, it will die after two seconds.
     */
    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    /*
     * OnTriggerEnter() is called when the object containing this script collides with another object
     * Reduces health of tanks
     */
    private void OnTriggerEnter(Collider other)
    {
        //If the bullet hit a player, this will detect that
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            if (other.tag == "Enemy")
            {
                if (gameObject.tag != "EnemyBullet")
                {
                    playerHealth.ReduceHealth(damage);
                }
            }
            if (other.tag == "PlayerTank")
            {
                if (gameObject.tag == "EnemyBullet")
                {
                    playerHealth.ReduceHealth(damage);
                }
            }  
        }

        //Destroy the bullet
        Destroy(gameObject);
    }

}
