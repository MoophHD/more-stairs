using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour {
	const string appId = "ca-app-pub-1038804138558980~9877584517";
	const string bannerId = "ca-app-pub-1038804138558980/9228995055";
	const string testBannerId = "ca-app-pub-3940256099942544/6300978111";

	const string interstialId = "ca-app-pub-1038804138558980/9283522292";
	const string testInterstialId = "ca-app-pub-3940256099942544/1033173712";
	private InterstitialAd interstitial;
    private AdRequest request;

	private static AdManager _instance;
	public static AdManager instance {
        get {
            return _instance;
        }
    }
	void Awake() {
		_instance = this;
	}

	void Start () {
		MobileAds.Initialize(appId);
        interstitial = new InterstitialAd(testInterstialId);
        request = new AdRequest.Builder().Build();
	}

	public void showInterstial() {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        interstitial.Destroy();

        request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
	}
}