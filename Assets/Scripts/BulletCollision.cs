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
              playerHealth.ReduceHealth(damage);
        }

        //Instantiate(HitEffect, transform.position, Quaternion.identity);

        //Destroy the bullet
        Destroy(gameObject);
    }

}
