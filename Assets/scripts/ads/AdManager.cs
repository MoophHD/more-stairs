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
    const int secondsBetweenAds = 0 * 60;
    int lastAdTime;
    System.DateTime epochStart;


	private InterstitialAd interstitial;
    private AdRequest request;

	private static AdManager _instance;
	public static AdManager instance {
        get {
            return _instance;
        }
    }
    int getCurSeconds() {
        return (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
    }
	void Awake() {
        _instance = this;
        DontDestroyOnLoad(this);
    }

	void Start() {
        epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        lastAdTime = getCurSeconds();

        MobileAds.Initialize(appId);

        requestInterstitial();
    }

    private AdRequest createRequest() {
        return new AdRequest.Builder().AddKeyword("game").Build();
    }

    private void requestInterstitial() {
        Debug.Log("request ad");
        if (this.interstitial != null)
        {
            Debug.Log("destroying an old ad");
            this.interstitial.Destroy();
        }

        this.interstitial = new InterstitialAd(testInterstialId);
        this.interstitial.LoadAd(this.createRequest());
    }

	public void showInterstial() {
        int now = getCurSeconds();
        if (now - lastAdTime >= secondsBetweenAds) {
            lastAdTime = now;
        } else {
            return;
        }

		Debug.Log("try show ad");
        Debug.Log("ad " + this.interstitial);

        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
            Debug.Log("just showed an ad");
            this.requestInterstitial();
        }
	}
}