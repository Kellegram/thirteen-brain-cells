using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light1Intensity : MonoBehaviour
{
    #region singleton
    public static light1Intensity instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public Light light1;

    public bool changeIntensity = false;

    public float intensitySpeed = 30f;
    public float maxIntensity = 25f;

    float startTime;

    void Start()
    {
        light1 = GetComponent<Light>();
        startTime = Time.time;
    }


    // Update is called once per frame
    void Update()
    {
        if (changeIntensity)
        {
            light1.intensity = Mathf.PingPong(Time.time * intensitySpeed, maxIntensity);
        }
        else
        {
            light1.intensity = 0;
        }

    }
}
