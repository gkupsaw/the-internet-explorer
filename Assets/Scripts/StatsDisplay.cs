
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatsDisplay : MonoBehaviour
{
    public GameObject lifeIconPrefab;
    public GameObject firewallIconPrefab;
    private GameObject Player;
    private GUIStyle guiStyle = new GUIStyle();
    public Font font;
    private int firewallCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Gui style 
        guiStyle.fontSize = 32; //change the font size
        guiStyle.normal.textColor = Color.white; //change the font color
        guiStyle.alignment = TextAnchor.UpperCenter;
        guiStyle.font = font;
        Player = GameObject.Find("Player");
    }

    void OnGUI()
    {
        string lifeText = "x" + Player.GetComponent<LifeManager>().lives;
        string firewallText = "x" + firewallCount;
        GUI.Label(new Rect(130, 45, 100, 40), lifeText, guiStyle);
        GUI.Label(new Rect(130 + 140, 45, 100, 40), firewallText, guiStyle);
    }

    public void incrementFirewallCount(int incValue) {
        this.firewallCount += incValue;
    }
}


