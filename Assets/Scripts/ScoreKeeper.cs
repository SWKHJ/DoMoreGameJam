using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int BestScore = 0;
    int totalScore = 0;

    public TextMeshProUGUI ScoreUI;
    public int TotalScore
    {
        get { return totalScore; }
        set { totalScore = value; }
    }
    private void Awake()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            BestScore = PlayerPrefs.GetInt("BestScore");
        }
    }
    // Start is called before the first frame update
    void Update()
    {

    }
    public void UpdateScore(int score)
    {
        totalScore += score;
        PlayerPrefs.SetInt("CurScore", totalScore);
        ScoreUI.text = totalScore.ToString();
    }

    public void changeBestScore()
    {
        BestScore = totalScore;
        PlayerPrefs.SetInt("BestScore", BestScore);
    }
}
