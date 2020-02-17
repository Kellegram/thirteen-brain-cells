using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (gameObject.tag == "PlayerTank")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            Destroy(gameObject);
        }
    }
    //g
    public void ReduceHealth(float f)
    {
        health -= f;
    }
}
