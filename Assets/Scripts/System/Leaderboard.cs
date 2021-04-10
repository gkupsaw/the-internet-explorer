using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public TMP_Text playername;
    public TMP_Text score;
    // use Application.persistentDataPath ?
    private string _filepath = "./Data/Leaderboard.json";

    void Start()
    {
        Save();
    }

    public void Save()
    {
        Dictionary<string, string> currLeaderboard = Load();
        currLeaderboard.Add(playername.text, score.text);
        Debug.Log(currLeaderboard.Get(playername.text));
        Debug.Log(JsonUtility.ToJson(currLeaderboard));
        System.IO.File.WriteAllText(_filepath, JsonUtility.ToJson(currLeaderboard));
    }

    Dictionary<string, string> Load()
    {
        using (StreamReader r = new StreamReader(_filepath))
        {
            string json = r.ReadToEnd();
            Dictionary<string, string> items = JsonUtility.FromJson<Dictionary<string, string>>(json);
            return items;
        }
    }
}
