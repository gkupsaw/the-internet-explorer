using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public int enemySpawnProb = 5;
    public GameObject[] enemies;
    public GameObject platformList;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, 1);
    }

    void SpawnEnemy()
    {
        if (Random.Range(0, enemySpawnProb) == 0)
        {
            GameObject e = Instantiate(ChooseEnemy());
            Debug.Log(e.tag);

            if (e.tag == "WalkingGlitch")
            {
                e.GetComponent<HorizontalMovement>().platform = ChoosePlatform();
            }
            else if (e.tag == "TeleportingGlitch")
            {
                e.GetComponent<RandomTeleport>().platformList = platformList;
            }
            else
            {
                Debug.Log("Invalid enemy tag");
                Destroy(e);
            }
        }
    }

    GameObject ChooseEnemy()
    {
        return enemies[Random.Range(0, enemies.Length)];
    }

    GameObject ChoosePlatform()
    {
        return platformList.transform.GetChild(Random.Range(0, platformList.transform.childCount)).gameObject;
    }
}
