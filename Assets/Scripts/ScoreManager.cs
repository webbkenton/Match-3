﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private Board board;
    public Text scoreText;
    public int score;
    public Image scoreBar;
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        scoreText.text = "" + score;
    }
    void Update()
    {
        scoreText.text = score.ToString();

    }
    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        UpdateBar();
    }

    private void UpdateBar()
    {
        if (board != null && scoreBar != null)
        {
            int length = board.scoreGoals.Length;
            scoreBar.fillAmount = (float)score / (float)board.scoreGoals[length - 1];
        }
    }
}
