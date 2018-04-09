﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	public Text score;
	public Text hgScore;
	private Score scoreManager;

	void Awake() {
		scoreManager = Camera.main.GetComponent<Score>();
	}
	public void onRestart() {
        Camera.main.GetComponent<Manager>().gameStart();
	}

	public void onSecondChance() {

	}

    void OnEnable() {
        score.text = scoreManager.scoreStr;
		hgScore.text = scoreManager.hgScoreStr;
    }
}