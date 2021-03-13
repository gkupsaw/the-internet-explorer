using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{

    // TODO
    // add free-fall fail-safe; pause player movement if drops below view
    // constant movement

    public GameObject platformBlockPrefab;
    public int platformsSpawned;
    public GameObject enemySpawner;

    int levelWidth = 4;
    float topLevelYPos = 2.2f;
    float bottomLevelYPos = -3.2f;
    float undergroundLevelYPos = -8.2f;
    float levelHeightDifference = 5.0f;
    float blockSpawnRate = 0.2f;

    double blockPrefabWidth;
    float playerHeight;

    GameObject platformObj;
    GameObject playerObj;
    List<GameObject> tileTopRow;
    List<GameObject> allTiles;

    float levelMoveSpeed = 3.5f;
    float levelSpawnRateModifier = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        print("Screen width: " + Screen.width);
        platformsSpawned = 0;
        this.platformObj = GameObject.Find("Platform");
        this.playerObj = GameObject.Find("Player");
        this.playerHeight = this.playerObj.GetComponent<Renderer>().bounds.size.y;
        Renderer blockPrefabRenderer = platformBlockPrefab.GetComponent<Renderer>();
        blockPrefabWidth = blockPrefabRenderer.bounds.size.x;
        // levelWidth = (int) (Screen.width / blockPrefabWidth);
        // print("level width: " + levelWidth);

        allTiles = new List<GameObject>();

        generateNewLevel(topLevelYPos);
        generateNewLevel(bottomLevelYPos);

        InvokeRepeating("generateUndergroundLevel", 0f, levelMoveSpeed * levelSpawnRateModifier);
    }

    void Update()
    {
        moveUp();
    }

    void generateUndergroundLevel()
    {
        generateNewLevel(undergroundLevelYPos);
    }

    void moveUp()
    {
        float step = levelMoveSpeed * Time.deltaTime; // calculate distance to move
        foreach (GameObject tile in allTiles)
        {
            tile.transform.Translate (Vector3.up * Time.deltaTime * levelMoveSpeed);
        }
    }

    void generateNewLevel(float height) {
        // int carveGapStartBlock = Random.Range(0, levelWidth - 1);
        // randomly adjust height between bottom level and underground
        // if ((i == carveGapStartBlock) || (i == carveGapStartBlock + 1))
        // {
        //     continue;
        // }
        // GameObject tile = createTile(i, height);

        // int spawnXBlocks = (int) (Random.Range(1, levelWidth - 1));
        // float initialGap = Random.Range(0, spawnXBlocks * blockPrefabWidth);

        for (int i = 0; i < levelWidth; i++)
        {
            bool shouldSpawn = (Random.Range(0, 10) <= this.blockSpawnRate * 10);

            if (!shouldSpawn) {
                continue;
            }

            float maxHeight = height + this.levelHeightDifference / 2;
            float randomHeight = Random.Range(height, maxHeight);
            GameObject tile = createTile(i, randomHeight);
            enemySpawner.GetComponent<SpawnEnemies>().SpawnEnemyAdPair();

            allTiles.Add(tile);
        }
    }

    GameObject createTile(int xBlock, float yPos)
    {
        // double startingPoint = blockPrefabWidth * (levelWidth / 2) * (-1);
        double startingPoint = Camera.main.ViewportToWorldPoint(new Vector3(0,1,0)).x;
        // double startingPoint = 0;
        double xPos = startingPoint + (blockPrefabWidth) * xBlock;
        Vector3 tilePosition = new Vector3(Convert.ToSingle(xPos), yPos, 0);
        GameObject tile = (GameObject) Instantiate(platformBlockPrefab, tilePosition, Quaternion.identity);

        // tile.transform.localscale.y = 10;
        // RectTransform tileRT = tile.GetComponent<RectTransform>();
        // tileRT.sizeDelta = new Vector2(1, 10);
        // tile.RectTransform.sizeDelta = new Vector2(1, 2);

        tile.transform.SetParent(this.platformObj.transform);
        return tile;
    }
}
