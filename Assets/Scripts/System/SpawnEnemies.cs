using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public int enemySpawnProb = 5;
    public GameObject enemy;
    public GameObject enemyTwin;

    void Update()
    {
        if (Random.Range(0, enemySpawnProb) == 0)
        {
            GameObject e = Instantiate(enemy);
            // e.GetComponent<RandomTeleport>().platform = null;//platform;
            e.GetComponent<RandomTeleport>().platforms = new GameObject[0];//platforms;
            if (enemyTwin)
            {
                Instantiate(enemyTwin);
            }
        }
    }
}
