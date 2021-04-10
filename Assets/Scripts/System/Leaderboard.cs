using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public Text name;
    public Text score;
    // use Application.persistentDataPath ?
    private string _filepath = "./Data/Leaderboard.json";

    void Start()
    {
        Save();
    }

    public void Save()
    {
        Dictionary<string, string> currLeaderboard = Load();
        currLeaderboard.Add(name.text, score.text);
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
