using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinuteTimer : MonoBehaviour
{

    //Public variables--------------------------------------
    public Text timer;
    public float timeScaleFactor = 1;//Change game speed
    //------------------------------------------------------

    //Private variables-------------------------------------
    private float currentTime;
    //------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {
            Time.timeScale = timeScaleFactor;
        }else
        {
            Time.timeScale = 1.0f;
        }
        string minutes = "";
        string hours = "";
            currentTime += Time.deltaTime;
        string seconds = (currentTime % 60).ToString("f0");//Don't display numbers after decimal
        if (currentTime > 60 &&  currentTime < 3600)
        {
            minutes = (Mathf.Floor(currentTime / 60)).ToString("f0");
            timer.text = minutes + ":" + seconds;
        }else if(currentTime < 60)
        {
            timer.text = seconds;
        }else
        {
            minutes = (Mathf.Floor(currentTime / 60) - (Mathf.Floor(currentTime / 3600)) * 60).ToString("f0");
            hours = (Mathf.Floor(currentTime / 3600)).ToString("f0");
            timer.text = hours + ":" + minutes + ":" + seconds;
        }

    }
}
