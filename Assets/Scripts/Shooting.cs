using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //This float isn't used yet but is shown in the editor. It may be used later.
    public float damage = 10f;
    public float range = 100f;

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
        //Raycast will find the nearest object in the given range and put relevant data in the hit variable
        RaycastHit hit;

        //If the Raycast hits an object, it will do whatever is in the if statement
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log("PEW");
        }
    }
}
