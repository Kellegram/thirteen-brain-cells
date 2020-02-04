using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    public GameObject EnemyPrefab;
    GameObject map;
    public List<Transform> spawnPoints;

    // Start is called before the first frame update
    private void Start()
    {
        map = PlayerManager.instance.map;

        //Add all navpoints when enemy is spawned
        Transform[] allChildren = map.GetComponentsInChildren<Transform>();

        foreach (Transform child in allChildren)
        {
            if (child.gameObject.tag == "Spawnpoint")
            {
                spawnPoints.Add(child);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameObject enemy = Instantiate(EnemyPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
        }
    }
}
