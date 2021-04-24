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
    public GameObject[] _rows;
    // public GameObject _rowPrefab;
    // use Application.persistentDataPath ?
    private string _filepath = "./Data/Leaderboard.json";
    private float _rawScore;

    void Start()
    {
        // Save();
        _rawScore = Mathf.Ceil(GetComponent<ScoreManager>().GetScore());
        _score.text = "Score: " + _rawScore.ToString();
        Render();
    }

    public void Save()
    {
        string playername = _playername.text.Trim();
        if (playername.Length == 0) return;

        Debug.Log("Saving leaderboard...");
        Dictionary<string, string> currLeaderboard = Load();

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
    void Render()
    {
        Transform display = transform.Find("Display");
        Dictionary<string, string> leaderboard = Load();
        GameObject row;

        int i = 0, MAX_SCORES = _rows.Length;
        foreach (KeyValuePair<string, string> kvp in leaderboard)
        {
            string playername = kvp.Key.Trim();
            string score = kvp.Value.Trim();
            row = _rows[i];
            row.transform.Find("Col1").GetComponent<TMP_Text>().text = playername;
            row.transform.Find("Col2").GetComponent<TMP_Text>().text = score;
            row.transform.parent = display;
            i++;

            if (i >= MAX_SCORES) break;
        }
    }
}
