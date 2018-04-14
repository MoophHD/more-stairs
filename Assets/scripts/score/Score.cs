using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    public int hgScore;

    const int SCORE_PER_ADD = 1;

    void Awake()
    {
        hgScore = PlayerPrefs.HasKey("highScore") ? PlayerPrefs.GetInt("highScore") : 0;
    }
    public void add() {
        score += SCORE_PER_ADD;
        if (score > hgScore) {
            hgScore = score;

            PlayerPrefs.SetInt("highScore", hgScore);
        }
    }
}