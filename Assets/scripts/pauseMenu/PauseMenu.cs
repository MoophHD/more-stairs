using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	public Text score;
	public Text hgScore;
	private Score scoreManager;
	private AdManager adManager;

	void Awake() {
		scoreManager = Camera.main.GetComponent<Score>();
        adManager = Camera.main.GetComponent<AdManager>();
	}
	public void onRestart() {
        if (AdManager.instance) AdManager.instance.showInterstial();

        Camera.main.GetComponent<Manager>().gameStart();
	}

    void OnEnable() {
        score.text = scoreManager.score.ToString();
		hgScore.text = scoreManager.hgScore.ToString();
    }
}
