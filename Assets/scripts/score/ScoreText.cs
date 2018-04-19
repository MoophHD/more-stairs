using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {
    private Text scoreTextTarget;
    private Score score;
    public bool isUpdating = false;
    void Awake() {
        score = Camera.main.GetComponent<Score>();
        scoreTextTarget = GetComponent<Text>();

        setHgScoreMode(true);
    }

    void Update() {
        if (isUpdating) scoreTextTarget.text = score.score.ToString();
    }

    public void setHgScoreMode(bool isMode) {
        if (isMode) {
            scoreTextTarget.fontSize = 75;
            scoreTextTarget.text = "Hg score " + score.hgScore;
        } else {
            scoreTextTarget.fontSize = 85;
            scoreTextTarget.text = score.score.ToString();
        }
    }
}