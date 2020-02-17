using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Destroy(gameObject);
        }
    }
    
    public void ReduceHealth(float f)
    {
        health -= f;
    }
}
