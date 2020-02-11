using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint3trigger : MonoBehaviour
{
    public bool Triggered3 = false;
    public bool LightChange;
    public Light light3;
    public light3Intensity light3boi;

    public void Start()
    {
        
    }
    void Update()
    {



        if (Triggered3)
        {
            GameObject.Find("doorleft3").GetComponent<Animator>().SetBool("isopening", true);
            GameObject.Find("doorright3").GetComponent<Animator>().SetBool("isopening", true);
            GameObject.Find("doorleft3").GetComponent<Animator>().SetBool("isclosing", false);
            GameObject.Find("doorright3").GetComponent<Animator>().SetBool("isclosing", false);
            light3Intensity.instance.changeIntensity = true;

        }
        else
        {
            GameObject.Find("doorleft3").GetComponent<Animator>().SetBool("isclosing", true);
            GameObject.Find("doorright3").GetComponent<Animator>().SetBool("isclosing", true);
            GameObject.Find("doorleft3").GetComponent<Animator>().SetBool("isopening", false);
            GameObject.Find("doorright3").GetComponent<Animator>().SetBool("isopening", false);
            light3Intensity.instance.changeIntensity = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Triggered3 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Triggered3 = false;
        }
    }
}