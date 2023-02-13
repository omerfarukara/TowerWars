using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using GameFolders.Scripts.Concretes;


public class AdManager : MonoSingleton<AdManager>
{
    private string bannerID = "ca-app-pub-3940256099942544/6300978111";
    private string intersititialID = "ca-app-pub-3940256099942544/1033173712";
    private string rewardedID = "ca-app-pub-3940256099942544/5224354917";
    private string rewardedInterstitialAd = "ca-app-pub-3940256099942544/5354046379";

    private BannerView _bannerAd;
    private InterstitialAd _interstitialAd;
    private RewardedAd _rewardedAd;
    private RewardedInterstitialAd _rewardedInterstitialAd;

    public InterstitialAd InterstitialAd => _interstitialAd;
    public RewardedAd RewardedAd => _rewardedAd;
    public RewardedInterstitialAd RewardedInterstitialAd => _rewardedInterstitialAd;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
        //RequestBanner();
        RequestInterstitial();
        RequestRewarded();
        RequestRewardedInterstitial();
    }

    #region BANNER
    private void RequestBanner()
    {
        // Create a 320x50 banner at the bottom  of the screen.
        _bannerAd = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        _bannerAd.LoadAd(request);
    }
    #endregion

    #region INTERSTITIAL

    private void RequestInterstitial()
    {
        // Initialize an InterstitialAd.
        _interstitialAd = new InterstitialAd(intersititialID);

        // Called when an ad request has successfully loaded.
        _interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        _interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        _interstitialAd.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        _interstitialAd.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the interstitial with the request.
        _interstitialAd.LoadAd(request);
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpening event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //TEKRAR REKLAM İSTEĞİ ALANI
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            RequestInterstitial();
        });
    }

    #endregion

    #region REWARDED
    private void RequestRewarded()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
        }
        // RewardedAd Catch.
        _rewardedAd = new RewardedAd(rewardedID);

        // Called when an ad request has successfully loaded.
        _rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;

        // Called when an ad request failed to load.
        _rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;

        // Called when an ad is shown.
        _rewardedAd.OnAdOpening += HandleRewardedAdOpening;

        // Called when an ad request failed to show.
        _rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;

        // Called when the user should be rewarded for interacting with the ad.
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        // Called when the ad is closed.
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the rewarded ad with the request.
        _rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
        Debug.Log("Reklam Yüklendi");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {

    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        //UpgradeCanvasController.Instance.GiveReward();
        //RequestRewarded();
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            RequestRewarded();
            Debug.Log("Yeni Reklam Yüklendi");
        });
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        //On User Earnerd Reward 
        //GİVE REWARD
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            //ÖDÜL VERME ALANI
        });
        //Debug.Log("Ödül Verildi");
    }
    #endregion

    #region REWARDEDINTERSTITIAL
    private void RequestRewardedInterstitial()
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        //Load the rewarded ad with the request.
        RewardedInterstitialAd.LoadAd(rewardedInterstitialAd, request, adLoadCallback);
    }

    private void adLoadCallback(RewardedInterstitialAd ad, AdFailedToLoadEventArgs error)
    {
        //throw new NotImplementedException();
        if (error == null)
        {
            _rewardedInterstitialAd = ad;

            _rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresent;
            _rewardedInterstitialAd.OnAdDidPresentFullScreenContent += HandleAdDidPresent;
            _rewardedInterstitialAd.OnAdDidDismissFullScreenContent += HandleAdDidDismiss;
            _rewardedInterstitialAd.OnPaidEvent += HandlePaidEvent;
        }
    }
    public void ShowRewardedInterstitialAd()
    {
        if (_rewardedInterstitialAd != null)
        {
            _rewardedInterstitialAd.Show(userEarnedRewardCallback);
        }
    }

    private void userEarnedRewardCallback(Reward reward)
    {
        //ÖDÜL VERME ALANI
        //TODO: Reward the user.
        //Give Reward!
    }

    private void HandleAdFailedToPresent(object sender, AdErrorEventArgs args)
    {

    }

    private void HandleAdDidPresent(object sender, EventArgs args)
    {

    }

    private void HandleAdDidDismiss(object sender, EventArgs args)
    {
        RequestRewardedInterstitial();
        //TEKRAR REKLAM İSTEĞİ ALANI
    }

    private void HandlePaidEvent(object sender, AdValueEventArgs args)
    {

    }

    #endregion
}
