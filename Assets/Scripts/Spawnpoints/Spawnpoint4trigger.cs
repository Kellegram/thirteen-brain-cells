using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint4trigger : MonoBehaviour
{
    public bool Triggered4 = false;
    public bool LightChange;
    public Light light4;
    public light4Intensity light4boi;

    public void Start()
    {

    }
    void Update()
    {



        if (Triggered4)
        {
            GameObject.Find("doorleft4").GetComponent<Animator>().SetBool("isopening", true);
            GameObject.Find("doorright4").GetComponent<Animator>().SetBool("isopening", true);
            GameObject.Find("doorleft4").GetComponent<Animator>().SetBool("isclosing", false);
            GameObject.Find("doorright4").GetComponent<Animator>().SetBool("isclosing", false);
            light4Intensity.instance.changeIntensity = true;

        }
        else
        {
            GameObject.Find("doorleft4").GetComponent<Animator>().SetBool("isclosing", true);
            GameObject.Find("doorright4").GetComponent<Animator>().SetBool("isclosing", true);
            GameObject.Find("doorleft4").GetComponent<Animator>().SetBool("isopening", false);
            GameObject.Find("doorright4").GetComponent<Animator>().SetBool("isopening", false);
            light4Intensity.instance.changeIntensity = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Triggered4 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Triggered4 = false;
        }
    }
}
