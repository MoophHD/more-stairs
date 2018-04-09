using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public string scoreStr;
    public string hgScoreStr;
    public int score;
    public int hgScore;

    const int SCORE_PER_ADD = 1;

    public void add() {
        score += SCORE_PER_ADD;
        scoreStr = score.ToString();

        if (score > hgScore) {
            hgScore = score;
            hgScoreStr = hgScore.ToString();
        }
    }
}