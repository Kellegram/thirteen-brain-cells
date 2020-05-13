using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// [Deprecated Class]
public class ButtonPlayScript : MonoBehaviour
{
    /*
     * changeMenu() loads the Endless scene from the main menu
     */
    public void changeMenu(string Endless)
    {
        SceneManager.LoadScene(Endless);
    }
}
