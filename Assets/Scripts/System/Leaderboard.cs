using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// disallowed chars: {}\",:

public class Leaderboard : MonoBehaviour
{
    public InputField _playername;
    public InputField _score;
    public GameObject _valueUIElements;
    // use Application.persistentDataPath ?
    private string _filepath = "./Data/Leaderboard.json";
    private float _rawScore;

    void Start()
    {
        // Save();
        _rawScore = Mathf.Ceil(GetComponent<ScoreManager>().GetScore());
        _score.text = "Score: " + _rawScore.ToString();
    }

    public void Save()
    {
        Debug.Log("Saving leaderboard...");
        Dictionary<string, string> currLeaderboard = Load();

        string playername = _playername.text.Trim();
        string score = _rawScore.ToString();
        if (currLeaderboard.ContainsKey(playername))
        {
            currLeaderboard.Remove(playername);
        }
        currLeaderboard.Add(playername, score);

        System.IO.File.WriteAllText(_filepath, Serialize(currLeaderboard));
        Debug.Log("Saved leaderboard!");
        Destroy(_valueUIElements);
    }

    Dictionary<string, string> Load()
    {
        using (StreamReader r = new StreamReader(_filepath))
        {
            string data = r.ReadToEnd();
            Dictionary<string, string> currLeaderboard = Deserialize(data);
            return currLeaderboard;
        }
    }

    Dictionary<string, string> Deserialize(string data)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        string[] nameScores = data.Substring(1, data.Length - 2).Split(',');

        foreach (string nameScore in nameScores)
        {
            string[] ns = nameScore.Split(':');

            if (ns.Length == 1)
            {
                return dict;
            }

            dict.Add(ns[0].Replace('"', ' ').Trim(), ns[1].Replace('"', ' ').Trim());
        }

        return dict;
    }

    string Serialize(Dictionary<string, string> dict)
    {
        string res = "";
        int i = 0;
        int sz = dict.Keys.Count;
        foreach (KeyValuePair<string, string> kvp in dict)
        {
            string playername = kvp.Key.Trim();
            string score = kvp.Value.Trim();
            res += "\"" + playername + "\"" + " : " + "\"" + score + "\"";
            if (i < sz - 1)
            {
                res += ",";
            }
            res += "\n";
            i++;
        }
        return "{\n" + res + "}";
    }
}
