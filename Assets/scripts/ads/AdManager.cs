using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour {
	// const string appId = "ca-app-pub-1038804138558980~9877584517";
	// // const string testAppId = "ca-app-pub-3940256099942544~3347511713";

	// const string bannerId = "ca-app-pub-1038804138558980/9228995055";
	// const string testBannerId = "ca-app-pub-3940256099942544/6300978111";
	// const string rewardId = "ca-app-pub-1038804138558980/9283522292";
	// const string testRewardId = "ca-app-pub-3940256099942544/5224354917";

	// private RewardBasedVideoAd rewardBasedVideo;

	// private BannerView bannerView;
	// public bool playingAd;

	// private static AdManager _instance;
	// public static AdManager instance {
    //     get {
    //         return _instance;
    //     }
    // }
	// void Awake() {
	// 	_instance = this;
	// }

	// void Start () {
	// 	MobileAds.Initialize(appId);
	// }

	// void initAds(Scene scene, LoadSceneMode mode) {
	// 	if (scene.buildIndex != 0) return;

	// 	this.rewardBasedVideo = RewardBasedVideoAd.Instance;
	// 	rewardBasedVideo.OnAdRewarded += (a, b) => { GameActions.secondChance(); playingAd = false;};
	// 	rewardBasedVideo.OnAdFailedToLoad += fail;
	// 	rewardBasedVideo.OnAdClosed += fail;

	// 	RequestBanner();
	// 	RequestRewardedVideo();
	// }

	// private void fail(object sender, AdFailedToLoadEventArgs args) {
	// 	GameActions.restart(); playingAd = false;
	// }

	// private void fail(object sender, EventArgs args) {
	// 	GameActions.restart(); playingAd = false;
	// }

	// public void showRewardedAd() {
		
	// 	if (this.rewardBasedVideo.IsLoaded()) {
	// 		playingAd = true;
	// 		this.rewardBasedVideo.Show();
	// 		RequestRewardedVideo();
	// 	}
	// }

	// public void RequestRewardedVideo() {
	// 	AdRequest request = new AdRequest.Builder().Build();
    //     rewardBasedVideo.LoadAd(request, rewardId);
	// }
}