using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light2Intensity : MonoBehaviour
{
    #region singleton
    public static light2Intensity instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public Light light2;

    public bool changeIntensity = false;

    public float intensitySpeed = 30f;
    public float maxIntensity = 25f;

    float startTime;

    void Start()
    {
        light2 = GetComponent<Light>();
        startTime = Time.time;
    }


    // Update is called once per frame
    void Update()
    {
        if (changeIntensity)
        {
            light2.intensity = Mathf.PingPong(Time.time * intensitySpeed, maxIntensity);
        }
        else
        {
            light2.intensity = 0;
        }

    }
}
