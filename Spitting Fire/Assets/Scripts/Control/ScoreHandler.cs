using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

/// <summary>
/// UI score updater
/// </summary>
public class ScoreHandler : MonoBehaviour
{
    private int killCount;
    [SerializeField] TextMeshProUGUI scoreText;
    

    private void Awake()
    {
        killCount = 0;
    }
    public void AddScore()
    {
        killCount++;
        UpdateScoreUI();
        

    }

    internal void Restart()
    {
        killCount = 0;
        UpdateScoreUI();
    }

    public float GetScore() => killCount;
    

    private void UpdateScoreUI()
    {

        scoreText.text = killCount.ToString();
    }
}
