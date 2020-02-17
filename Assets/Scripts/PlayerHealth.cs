using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    Score plScore;

    private void Start()
    {
        plScore = PlayerManager.instance.player.GetComponent<Score>();
    }

    // Update is called once per frame
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
    
    public void ReduceHealth(float f)
    {
        health -= f;
    }
}
