using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointTrigger : MonoBehaviour
{
    //When 
    private bool triggered = false;
    public int doorNumber = 0;

    void Update()
    {
        if (triggered)
        {
            SpawnManager.instance.OpenDoor(doorNumber);
        }
        else
        {
            SpawnManager.instance.CloseDoor(doorNumber);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            triggered = false;
        }
    }
}
