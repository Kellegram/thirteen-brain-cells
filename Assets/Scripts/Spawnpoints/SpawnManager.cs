using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region singleton

    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject[] doors;

    public Light[] lights;
    public float intensitySpeed = 30f;
    public float maxIntensity = 25f;

    public void OpenDoor(int doorNumber)
    {
        doors[doorNumber].transform.Find("DoorFlapLeft").gameObject.GetComponent<Animator>().SetBool("isopening", true);
        doors[doorNumber].transform.Find("DoorFlapRight").gameObject.GetComponent<Animator>().SetBool("isopening", true);
        doors[doorNumber].transform.Find("DoorFlapLeft").gameObject.GetComponent<Animator>().SetBool("isclosing", false);
        doors[doorNumber].transform.Find("DoorFlapRight").gameObject.GetComponent<Animator>().SetBool("isclosing", false);

        lights[doorNumber].intensity = Mathf.PingPong(Time.time * intensitySpeed, maxIntensity);
    }

    public void CloseDoor(int doorNumber)
    {
        doors[doorNumber].transform.Find("DoorFlapLeft").gameObject.GetComponent<Animator>().SetBool("isclosing", true);
        doors[doorNumber].transform.Find("DoorFlapRight").gameObject.GetComponent<Animator>().SetBool("isclosing", true);
        doors[doorNumber].transform.Find("DoorFlapLeft").gameObject.GetComponent<Animator>().SetBool("isopening", false);
        doors[doorNumber].transform.Find("DoorFlapRight").gameObject.GetComponent<Animator>().SetBool("isopening", false);


        lights[doorNumber].intensity = 0;
    }
}
