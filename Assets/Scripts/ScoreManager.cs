using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // https://www.youtube.com/watch?v=9HvTwtfBaYM
    public TMP_Text scoreText;
    public float scoreCount;
    public float pointsPerSecond;
    public bool scoreIncreasing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreCount  += pointsPerSecond * Time.deltaTime;
        scoreText.text = "Score: " + Mathf.Round(scoreCount);
    }
}