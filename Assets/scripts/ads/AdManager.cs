using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour {
	const string appId = "ca-app-pub-3940256099942544~3347511713";

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

	void Start() {
        Debug.Log("ad manager init");
        MobileAds.Initialize(appId);

        requestInterstitial();
    }

    private AdRequest createRequest() {
        return new AdRequest.Builder().AddKeyword("game").Build();
    }

    private void requestInterstitial() {
        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }

        this.interstitial = new InterstitialAd(testInterstialId);
        this.interstitial.LoadAd(this.createRequest());
    }

	public void showInterstial() {
 

		Debug.Log("try show ad");
        Debug.Log("ad " + this.interstitial);
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();

            this.requestInterstitial();
        }

		Debug.Log("requesting & loading new ad");
	}
}