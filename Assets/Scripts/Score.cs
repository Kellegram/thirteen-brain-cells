using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    //Public variables
    public Text scoreDisplay;

    //Private variables
    private int playerScore;
    private int waveCounter;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        waveCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
       // scoreDisplay.text = "Score: " + playerScore.ToString() + "\nWave: " + waveCounter.ToString();
        scoreDisplay.text = waveCounter.ToString();
    }

    /*
     Setters to call from other functions
     -Raise player score
     -
         */
    public void addScore(int points){playerScore += points;}
    public void incrementWave(){waveCounter++;}

    /*
     Call this from any script to reset player's score
     */
    public void ResetScore()
    {
        playerScore = 0;
        waveCounter = 0;
    }
}
