using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /*
     * PlayGame() is called when the play button is pressed,
     * it will begin the next scene in the scene stack
     */
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /*
     * QuitGame() is called when the quit button is pressed,
     * it will close the application
     */
    public void QuitGame()
    {
        Application.Quit();
    }
}
