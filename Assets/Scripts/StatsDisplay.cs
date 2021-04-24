
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatsDisplay : MonoBehaviour
{
    // public GameObject lifeIconPrefab;
    // public GameObject firewallIconPrefab;
    private GameObject Player;
    private GUIStyle guiStyle = new GUIStyle();
    private int firewallCount = 0;

    public float DesignWidth = 1920.0f;
    public float DesignHeight = 1080.0f;
    public Texture lifeIconTexture;
    public Texture firewallIconTexture;
    public Font font;

    // Start is called before the first frame update
    void Start()
    {
        // Gui style 
        guiStyle.fontSize = 50; //change the font size
        guiStyle.normal.textColor = Color.white; //change the font color
        guiStyle.alignment = TextAnchor.UpperCenter;
        guiStyle.font = font;
        Player = GameObject.Find("Player");

        // GameObject lifeIcon = (GameObject) Instantiate(lifeIconPrefab, new Vector2(100, 45), Quaternion.identity);
        // lifeIcon.transform.SetParent(this.transform);
    }

    void OnGUI()
    {
        float resX = (float) (Screen.width) / DesignWidth;
        float resY = (float) (Screen.height) / DesignHeight;
        //Set matrix
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(resX, resY, 1));



        string lifeText = "x" + Player.GetComponent<LifeManager>().lives;
        string firewallText = "x" + firewallCount;
        // GUI.Label(new Rect(130, 45, 100, 40), lifeText, guiStyle);
        Vector3 temp = (transform.position);
        print(temp);

        int xPos = 180;
        int imageYPos = 40;
        int textYPos = 70;

        int iconTextGap = 100;
        GUI.DrawTexture(new Rect(xPos, imageYPos, 100, 100), lifeIconTexture, ScaleMode.ScaleToFit, true);
        GUI.Label(new Rect(xPos + iconTextGap, textYPos, 100, 40), lifeText, guiStyle);

        xPos += 260;
        GUI.DrawTexture(new Rect(xPos, imageYPos, 100, 100), firewallIconTexture, ScaleMode.ScaleToFit, true);
        GUI.Label(new Rect(xPos + iconTextGap, textYPos, 100, 40), firewallText, guiStyle);
    }

    public void incrementFirewallCount(int incValue) {
        this.firewallCount += incValue;
    }
}


