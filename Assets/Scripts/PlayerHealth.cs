using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    Score plScore;

    /*
     * Start() is called when the object containing this script is instantiated.
     * this function will set the score variable.
     */
    private void Start()
    {
        plScore = PlayerManager.instance.player.GetComponent<Score>();
    }

    /*
     * Update() will be called every frame
     * This function will check the heath of the tank every frame
     */
    void Update()
    {
        if (health <= 0)
        {
            //If enemy tank killed, add to player score
            if (gameObject.tag == "Enemy")
            {
                plScore.addScore(20);
            }
            if (gameObject.tag == "PlayerTank")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            Destroy(gameObject);
        }
    }
    
    /*
     * ReduceHealth() will reduce the health of the tank.
     * This function will be called by the BulletCollision script
     */
    public void ReduceHealth(float f)
    {
        health -= f;
    }
}
