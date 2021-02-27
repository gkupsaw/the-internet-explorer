using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{

    public GameObject platformBlockPrefab;
    public int platformsSpawned;

    double blockPrefabWidth;
    int levelWidth = 10;
    float topLevelYPos = 2.2f;
    float bottomLevelYPos = -3.2f;

    GameObject platformObj;
    GameObject playerObj;
    List<GameObject> tileTopRow;
    List<GameObject> tileBottomRow;

    bool shouldStartMoving = false;
    int tileMoveSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        platformsSpawned = 0;
        this.platformObj = GameObject.Find("Platform");
        this.playerObj = GameObject.Find("Player");
        Renderer blockPrefabRenderer = platformBlockPrefab.GetComponent<Renderer>();
        blockPrefabWidth = blockPrefabRenderer.bounds.size.x;
        tileBottomRow = new List<GameObject>();
        tileTopRow = new List<GameObject>();


        print("block prefab width: " + blockPrefabWidth);
        print("start platform spawner");

        for (int i = 0; i < levelWidth; i++) 
        {
            if (i == 4 || i == 5) 
                continue;
            GameObject tileTop = createTile(i, topLevelYPos);
            tileTopRow.Add(tileTop);
        }

        generateNewLevel();
    }

    void Update()
    {
        // if (Input.GetButtonDown("Jump")) 
        if (playerObj.transform.position.y <= bottomLevelYPos)
        {
            this.shouldStartMoving = true;
        }

        if (shouldStartMoving) {
            moveUp();
        }
    }

    void moveUp() 
    {

        float step = tileMoveSpeed * Time.deltaTime; // calculate distance to move

        foreach (GameObject tile in tileTopRow) 
        {
            tile.transform.Translate (Vector3.up * Time.deltaTime * tileMoveSpeed); 
        }
        foreach (GameObject tile in tileBottomRow) 
        {
            tile.transform.Translate (Vector3.up * Time.deltaTime * tileMoveSpeed); 
        }

        // bottom row passed a certain height
        // * change to triggerBlock instead of bottomRow
        if (tileBottomRow[0].transform.position.y >= topLevelYPos) {
            this.shouldStartMoving = false;
            // set new triggerBlock block
            // generateNewLevel's
            // destroy all blocks out of window (only the top ones)
        }

    }

    void generateNewLevel() {
        int carveGapStartBlock = Random.Range(0, levelWidth - 1);
        for (int i = 0; i < levelWidth; i++) 
        {
            if ((i == carveGapStartBlock) || (i == carveGapStartBlock + 1)) 
            {
                continue;
            }
            GameObject tile = createTile(i, bottomLevelYPos);
            tileBottomRow.Add(tile);
        }
    }

    GameObject createTile(int xBlock, float yPos) 
    {
        double startingPoint = blockPrefabWidth * (levelWidth / 2) * (-1);
        double xPos = startingPoint + (blockPrefabWidth) * xBlock;
        Vector3 tilePosition = new Vector3(Convert.ToSingle(xPos), yPos, 0);
        GameObject tile = (GameObject) Instantiate(platformBlockPrefab, tilePosition, Quaternion.identity);
        tile.transform.parent = this.platformObj.transform;
        return tile;
    }
}
