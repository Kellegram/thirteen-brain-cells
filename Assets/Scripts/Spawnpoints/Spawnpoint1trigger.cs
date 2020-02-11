using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint1trigger : MonoBehaviour
{
    public bool Triggered1 = false;
    public bool LightChange;
    public Light light1;
    public light1Intensity light1boi;

    public void Start()
    {

    }
    void Update()
    {



        if (Triggered1)
        {
            GameObject.Find("doorleft1").GetComponent<Animator>().SetBool("isopening", true);
            GameObject.Find("doorright1").GetComponent<Animator>().SetBool("isopening", true);
            GameObject.Find("doorleft1").GetComponent<Animator>().SetBool("isclosing", false);
            GameObject.Find("doorright1").GetComponent<Animator>().SetBool("isclosing", false);
            light1Intensity.instance.changeIntensity = true;

        }
        else
        {
            GameObject.Find("doorleft1").GetComponent<Animator>().SetBool("isclosing", true);
            GameObject.Find("doorright1").GetComponent<Animator>().SetBool("isclosing", true);
            GameObject.Find("doorleft1").GetComponent<Animator>().SetBool("isopening", false);
            GameObject.Find("doorright1").GetComponent<Animator>().SetBool("isopening", false);
            light1Intensity.instance.changeIntensity = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Triggered1 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Triggered1 = false;
        }
    }
}
