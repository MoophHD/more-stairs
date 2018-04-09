using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {
    private Text scoreTextTarget;
    private Score score;
    void Awake() {
        score = Camera.main.GetComponent<Score>();
        scoreTextTarget = GetComponent<Text>();
    }

    void Update() {
        if (gameObject.activeSelf) 
            scoreTextTarget.text = score.scoreStr;
    }
}