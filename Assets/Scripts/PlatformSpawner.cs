using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{

    // public GameObject platformObj;
    public GameObject platformBlockPrefab;
    public int platformsSpawned;

    int levelWidth = 10;
    float TopLevelYPos = 2.2f;
    float bottomLevelYPos = -3.2f;
    double blockPrefabWidth;
    List<GameObject> tileTopRow;
    List<GameObject> tileBottomRow;

    // Start is called before the first frame update
    void Start()
    {
        platformsSpawned = 0;
        // platformObj = GameObject.Find("Platform");
        Renderer blockPrefabRenderer = platformBlockPrefab.GetComponent<Renderer>();
        blockPrefabWidth = blockPrefabRenderer.bounds.size.x;
        tileBottomRow = new List<GameObject>();


        print("block prefab width: " + blockPrefabWidth);
        print("start platform spawner");

        for (int i = 0; i < levelWidth; i++) 
        {
            if (i == 4 || i == 5) 
                continue;
            GameObject tileTop = createTile(i, TopLevelYPos);
        }

        generateNewLevel();
    }

    void Update()
    {
        moveUp();
    }

    void moveUp() 
    {
        if (Input.GetButtonDown("Jump")) 
        {
            foreach (GameObject tile in tileBottomRow) 
            {
                Rigidbody2D tileBody = tile.GetComponent<Rigidbody2D>();
                tileBody.velocity = new Vector2(0, 10);
                // tileBody.gravityScale = 0;
                // tileBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
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
        Rigidbody2D tileBody = tile.GetComponent<Rigidbody2D>();
        tileBody.velocity = Vector2.zero;
        tileBody.gravityScale = 0;
        tileBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        tileBody.isKinematic = false;

        return tile;
    }
}
