﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{

    private LineRenderer lr;

    public Material noEnemy;
    public Material yesEnemy;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                if (hit.collider.tag == "Enemy")
                {
                    lr.material = yesEnemy;
                }
                else
                {
                    lr.material = noEnemy;
                }
                lr.SetPosition(1, new Vector3(0, 0, hit.distance));
            }
            else
            {
                lr.SetPosition(1, new Vector3(0, 0, 5000));
            }
        }
    }
}
