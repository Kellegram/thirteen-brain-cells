using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    public GameObject EnemyPrefab;

    //Map stores all navpoints and spawnpoints
    GameObject map;

    //List of spawnPoints automatically detected in this script
    [HideInInspector]
    public List<Transform> spawnPoints;

    [HideInInspector]
    public List<GameObject> enemies;
    int waveNumber = 0;
    int numTanks = 1;

    //Reference Score script to increment the stageCounter
    Score stageCnt;

    /* Start() is called when the object with this script on it is instatiated.
     * This function will define all of the spawn points for the spawners.
     */
    private void Start()
    {
        StartCoroutine("EnemyDeathCheckWithDelay", .2f);
        map = PlayerManager.instance.map;
        Transform[] allChildren = map.GetComponentsInChildren<Transform>();
        
        foreach (Transform child in allChildren)
        {
            if (child.gameObject.tag == "Spawnpoint")
            {
                spawnPoints.Add(child);
            }
        }

        stageCnt = PlayerManager.instance.player.GetComponent<Score>();
    }

    IEnumerator EnemyDeathCheckWithDelay(float delay)
    {
        while (true)
        {
            //wait .2f seconds before running FindVisibleTargets() again
            yield return new WaitForSeconds(delay);
            EnemyDeathCheck();
        }
    }
 
    /* Update() is called each frame
     * In this case, Update() will check every frame when all of the enemies are dead.
     * Once they are dead, it will instantiate a new wave of enemies.
     */
    void Update()
    {
        if (enemies.Count <= 0)
        {
            waveNumber++;
            if (waveNumber >= 3)
            {
                numTanks = waveNumber - Random.Range(0, 2);
            }
            else
            {
                numTanks = waveNumber;
            }
            for (int i = 0; i < numTanks; i++)
            {
                int spawnNumber = Random.Range(0, spawnPoints.Count);
                Vector3 spawnPointRandomizer = new Vector3(Random.Range(-20,20), spawnPoints[spawnNumber].position.y, Random.Range(-20,20));
                GameObject enemy = Instantiate(EnemyPrefab, spawnPoints[spawnNumber].position + spawnPointRandomizer, spawnPoints[spawnNumber].rotation);
                enemies.Add(enemy);
            }

            stageCnt.incrementWave();
        }
    }

    /*
     * EnemyDeathCheck() Checks whether an enemy has been killed and reduces the size of the enemies list
     * Takes in no variables
     * Returns no variables
     */
    void EnemyDeathCheck()
    {
        
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }
    }
}
