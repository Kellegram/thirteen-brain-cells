using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    //Public variables--------------------------------------
    public Text timer;
    public float levelTimer = 60;//Sets maximum time
    //------------------------------------------------------

    //Private variables-------------------------------------
    private float currentTime;
    //------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        currentTime = levelTimer;//Set to updated levelTimer
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime; 
        string seconds = currentTime.ToString("f0");//Don't display numbers after decimal
        timer.text = seconds;
    }
}
