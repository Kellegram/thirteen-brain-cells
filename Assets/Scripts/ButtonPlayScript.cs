using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPlayScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void changeMenu(string Endless)
    {
        SceneManager.LoadScene(Endless);
    }
}
