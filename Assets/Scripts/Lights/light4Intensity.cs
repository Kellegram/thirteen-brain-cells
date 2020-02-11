using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light4Intensity : MonoBehaviour
{
    #region singleton
    public static light4Intensity instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public Light light4;

    public bool changeIntensity = false;

    public float intensitySpeed = 30f;
    public float maxIntensity = 25f;

    float startTime;

    void Start()
    {
        light4 = GetComponent<Light>();
        startTime = Time.time;
    }


    // Update is called once per frame
    void Update()
    {
        if (changeIntensity)
        {
            light4.intensity = Mathf.PingPong(Time.time * intensitySpeed, maxIntensity);
        }
        else
        {
            light4.intensity = 0;
        }

    }
}
