using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maket_Mgr : MonoBehaviour
{
    PlayerController playerController;
    ScoreKeeper scoreKeeper;

    int[] cropScore = new int[] { 100, 200, 300, 500, 1000 };
    public int bonusScore = 5000;
    int scoreSum = 0;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            calScore();
            scoreKeeper.UpdateScore(scoreSum);
            playerController.ClearCropNum();
            scoreSum = 0;
        }
    }
    void calScore()
    {
        for (int i = 0; i < cropScore.Length; i++)
        {
            scoreSum += cropScore[i] * playerController.cropNum[i];
            if (playerController.checkIsFull())
            {
                scoreSum += bonusScore;
            }
        }
    }

}
