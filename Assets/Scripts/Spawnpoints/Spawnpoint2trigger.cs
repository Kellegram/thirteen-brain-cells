using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint2trigger : MonoBehaviour
{
    public bool Triggered2 = false;
    public bool LightChange;
    public Light light2;
    public light2Intensity light3boi;

    public void Start()
    {

    }
    void Update()
    {



        if (Triggered2)
        {
            GameObject.Find("doorleft2").GetComponent<Animator>().SetBool("isopening", true);
            GameObject.Find("doorright2").GetComponent<Animator>().SetBool("isopening", true);
            GameObject.Find("doorleft2").GetComponent<Animator>().SetBool("isclosing", false);
            GameObject.Find("doorright2").GetComponent<Animator>().SetBool("isclosing", false);
            light2Intensity.instance.changeIntensity = true;

        }
        else
        {
            GameObject.Find("doorleft2").GetComponent<Animator>().SetBool("isclosing", true);
            GameObject.Find("doorright2").GetComponent<Animator>().SetBool("isclosing", true);
            GameObject.Find("doorleft2").GetComponent<Animator>().SetBool("isopening", false);
            GameObject.Find("doorright2").GetComponent<Animator>().SetBool("isopening", false);
            light2Intensity.instance.changeIntensity = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Triggered2 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Triggered2 = false;
        }
    }
}
