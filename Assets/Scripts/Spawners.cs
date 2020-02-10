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

    /*
    Update()
    is called once per frame.

    This function is used to calculate the next wave. Once the enemy count is less than zero (meaning all the enemies have died),
    the wave number is increased by 1, and depending on certain conditions, the wave number is calculated.

    Once the wave is calculated, that number of enemies is spawned.
    // */
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
                GameObject enemy = Instantiate(EnemyPrefab, spawnPoints[spawnNumber].position, spawnPoints[spawnNumber].rotation);
                enemies.Add(enemy);
            }
        }
    }

    /*
    EnemyDeathCheck()
    function will loop through all of the enemies in the list, and remove
    them if their value is null. If the value of the enemy is null, the enemy
    will be removed from the list.

    This function is needed because c# does not automatically remove null objects from
    lists.
    // */
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
