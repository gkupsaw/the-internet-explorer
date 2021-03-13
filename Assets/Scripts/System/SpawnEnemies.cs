using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public int enemySpawnProb = 5;
    public GameObject ad;
    public GameObject[] enemies;

    public GameObject platformsParent;
    public GameObject enemiesParent;
    public GameObject adsParent;

    void Start()
    {
        InvokeRepeating("SpawnEnemyAdPair", 0, 1);
    }

    void SpawnEnemyAdPair()
    {
        if (Random.Range(0, enemySpawnProb) == 0)
        {
            GameObject e = Instantiate(ChooseEnemy());
            e.transform.SetParent(enemiesParent.transform);

            if (e.tag == "WalkingGlitch")
            {
                e.GetComponent<HorizontalMovement>().platform = ChoosePlatform();
                // GameObject a = Instantiate(ad);
                // a.transform.position = new Vector3(a.transform.position.x, a.transform.position.y+3,a.transform.position.z);
                // a.transform.SetParent(adsParent);
            }
            else if (e.tag == "TeleportingGlitch")
            {
                e.GetComponent<RandomTeleport>().platformList = platformsParent;
                // GameObject a = Instantiate(ad);
                // a.transform.position = new Vector3(a.transform.position.x, a.transform.position.y+3,a.transform.position.z);
                // a.transform.SetParent(adsParent);
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
        return platformsParent.transform.GetChild(Random.Range(0, platformsParent.transform.childCount)).gameObject;
    }
}
