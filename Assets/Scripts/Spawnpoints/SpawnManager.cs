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

    //Editor variables. Insert any doors/lights into script. Order matters. IntensitySpeed determines how quickly the lights flash. maxIntesity is how bright they go.
    public GameObject[] doors;
    public Light[] lights;
    public float intensitySpeed = 30f;
    public float maxIntensity = 25f;

    /*
     * OpenDoor() sets all of the variables for animating the door correctly.
     * The, PingPings the brightness and intensity of the lights.
     */
    public void OpenDoor(int doorNumber)
    {
        doors[doorNumber].transform.Find("DoorFlapLeft").gameObject.GetComponent<Animator>().SetBool("isopening", true);
        doors[doorNumber].transform.Find("DoorFlapRight").gameObject.GetComponent<Animator>().SetBool("isopening", true);
        doors[doorNumber].transform.Find("DoorFlapLeft").gameObject.GetComponent<Animator>().SetBool("isclosing", false);
        doors[doorNumber].transform.Find("DoorFlapRight").gameObject.GetComponent<Animator>().SetBool("isclosing", false);

        lights[doorNumber].intensity = Mathf.PingPong(Time.time * intensitySpeed, maxIntensity);
    }

    /*
     * CloseDoor() ends the animation of the doors and the flashing of the lights.
     */
    public void CloseDoor(int doorNumber)
    {
        doors[doorNumber].transform.Find("DoorFlapLeft").gameObject.GetComponent<Animator>().SetBool("isclosing", true);
        doors[doorNumber].transform.Find("DoorFlapRight").gameObject.GetComponent<Animator>().SetBool("isclosing", true);
        doors[doorNumber].transform.Find("DoorFlapLeft").gameObject.GetComponent<Animator>().SetBool("isopening", false);
        doors[doorNumber].transform.Find("DoorFlapRight").gameObject.GetComponent<Animator>().SetBool("isopening", false);


        lights[doorNumber].intensity = 0;
    }
}
