using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    public int hgScore;

    const int SCORE_PER_ADD = 1;
    public const int SCORE_PER_POINT_UP = 4;

    void Awake()
    {
        hgScore = PlayerPrefs.HasKey("highScore") ? PlayerPrefs.GetInt("highScore") : 0;
    }
    public void add() {
        addScore(SCORE_PER_ADD);
    }

    public void addPointUp() {
        addScore(SCORE_PER_POINT_UP);
    }

    private void addScore(int value) {
        score += SCORE_PER_ADD;
        if (score > hgScore)
        {
            hgScore = score;

            PlayerPrefs.SetInt("highScore", hgScore);
        }
    }
}