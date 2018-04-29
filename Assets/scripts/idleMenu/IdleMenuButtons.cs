using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdleMenuButtons : MonoBehaviour {
	const float LERP_TIME = 0.125f;
	private float t;
 	private GameObject bg;
	private SpriteRenderer bgSprite;
	private bool isBgColorWhite;
	private Color bgClFrom;
	private Color bgClTo;
	private Color white = new Color(1,1,1);
    // #4b4b4b
    private Color black = new Color(0.295f, 0.295f, 0.295f);
	public Image toggleClBtnSprite;
	public Sprite moon;
	public Sprite sun;

	void Awake () {
		isBgColorWhite = true;
        t = 1f;
		bg = Camera.main.transform.Find("bg").gameObject;
        bgSprite = bg.GetComponent<SpriteRenderer>();
        bgClFrom = white;
		// 333 
        bgClFrom = black;
	}

	public void flip() {
		Camera.main.GetComponent<CameraController>().flip();
	}

	public void toggleBgColor() {
		if (isBgColorWhite) {
            // bg to black
            bgClFrom = white;
            bgClTo = black;
            toggleClBtnSprite.sprite = sun;
		} else {
            //to white
            bgClFrom = black;
            bgClTo = white;
            toggleClBtnSprite.sprite = moon;
        }

        isBgColorWhite = !isBgColorWhite;
		t = 0f;
	}

	void Update() {
		if (t < 1) {
            t += Time.deltaTime / LERP_TIME;
            Color newCl = Color.Lerp(bgClFrom, bgClTo, t);

            bgSprite.color = newCl;
        }
	}
}
