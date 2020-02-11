using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light3Intensity : MonoBehaviour
{
    #region singleton
    public static light3Intensity instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public Light light3;

    public bool changeIntensity = false;

    public float intensitySpeed = 30f;
    public float maxIntensity = 25f;

    float startTime;

    void Start()
    {
        light3 = GetComponent<Light>();
        startTime = Time.time;
    }


    // Update is called once per frame
    void Update()
    {
        if (changeIntensity)
        {
            light3.intensity = Mathf.PingPong(Time.time * intensitySpeed, maxIntensity);
        }
        else
        {
            light3.intensity = 0;
        }

    }
}