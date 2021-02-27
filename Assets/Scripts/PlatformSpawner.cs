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

    int levelWidth = 10;
    float topLevelYPos = 2.2f;
    float bottomLevelYPos = -3.2f;
    float undergroundLevelYPos = -8.2f;
    float levelHeightDifference = 5.0f;
    float blockSpawnRate = 0.3f;

    double blockPrefabWidth;
    float playerHeight;

    GameObject platformObj;
    GameObject playerObj;
    List<GameObject> tileTopRow;
    List<GameObject> allTiles;

    GameObject triggerBlock;
    GameObject oldTriggerBlock;

    bool shouldStartMoving = false;
    float tileMoveSpeed = 4.5f;

    // Start is called before the first frame update
    void Start()
    {
        platformsSpawned = 0;
        this.platformObj = GameObject.Find("Platform");
        this.playerObj = GameObject.Find("Player");
        this.playerHeight = this.playerObj.GetComponent<Renderer>().bounds.size.y;
        Renderer blockPrefabRenderer = platformBlockPrefab.GetComponent<Renderer>();
        blockPrefabWidth = blockPrefabRenderer.bounds.size.x;
        allTiles = new List<GameObject>();

        generateNewLevel(topLevelYPos);
        generateNewLevel(bottomLevelYPos);
    }

    void Update()
    {

        if (this.triggerBlock) {
            float triggerBlockHeight = this.triggerBlock.GetComponent<Renderer>().bounds.size.y;
            float triggerBlockTop = this.triggerBlock.transform.position.y + triggerBlockHeight / 2;

            if (!shouldStartMoving && playerObj.transform.position.y - playerHeight / 2 <= triggerBlockTop)
            {
                this.oldTriggerBlock = this.triggerBlock;
                generateNewLevel(undergroundLevelYPos);
                this.shouldStartMoving = true;
            }
        }


        if (shouldStartMoving) {
            moveUp();
        }
    }

    void moveUp() 
    {

        float step = tileMoveSpeed * Time.deltaTime; // calculate distance to move
        foreach (GameObject tile in allTiles) 
        {
            tile.transform.Translate (Vector3.up * Time.deltaTime * tileMoveSpeed); 
        }

        // triggerBlock passed a certain height
        if (this.oldTriggerBlock.transform.position.y >= topLevelYPos) {
            this.shouldStartMoving = false;
            // destroy all blocks out of window (only the top ones)
        }

    }

    void generateNewLevel(float height) {
        // int carveGapStartBlock = Random.Range(0, levelWidth - 1);

        // randomly adjust height between bottom level and underground
        for (int i = 0; i < levelWidth; i++) 
        {
            bool shouldSpawn = (Random.Range(0, 10) <= this.blockSpawnRate * 10);
            
            if (!shouldSpawn) {
                continue;
            }

            float maxHeight = height + this.levelHeightDifference / 2;
            float randomHeight = Random.Range(height, maxHeight);
            GameObject tile = createTile(i, randomHeight);
            //
            // if ((i == carveGapStartBlock) || (i == carveGapStartBlock + 1)) 
            // {
            //     continue;
            // }
            // GameObject tile = createTile(i, height);
            allTiles.Add(tile);
        }
        this.triggerBlock = allTiles[allTiles.Count - 1];
    }

    GameObject createTile(int xBlock, float yPos) 
    {
        double startingPoint = blockPrefabWidth * (levelWidth / 2) * (-1);
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
