using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2f);
    }
    //public GameObject HitEffect;
    private void OnCollisionEnter(Collision collision)
    {
        //Instantiate(HitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
