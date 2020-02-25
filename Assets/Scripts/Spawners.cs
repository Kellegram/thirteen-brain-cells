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


    // Start is called before the first frame update
    private void Start()
    {
        //ensures game isn't checking if enemies are dead every frame
        StartCoroutine("EnemyDeathCheckWithDelay", .2f);

        //Map is added to player manager and detected by this script
        map = PlayerManager.instance.map;

        //Add all navpoints when enemy is spawned
        Transform[] allChildren = map.GetComponentsInChildren<Transform>();
        
        //Looks at all the child objects of the map object to find spawn points
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

    // Update is called once per frame
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

    void EnemyDeathCheck()
    {
        //checks whether an enemy has been killed and reduces the size of the enemies list
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }
    }
}
