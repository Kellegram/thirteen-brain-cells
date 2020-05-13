using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    /*
     * Update() is called every frame.
     * This function checks if the player has pressed the escape or p buttons,
     * and either quits the application or loads the main menu.
     */
    void Update()
    {
        if (Input.GetKey("p"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
