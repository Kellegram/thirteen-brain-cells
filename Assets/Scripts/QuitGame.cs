﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("p"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
