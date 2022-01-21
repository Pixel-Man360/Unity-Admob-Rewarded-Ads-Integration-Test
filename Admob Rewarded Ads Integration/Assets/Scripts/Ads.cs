using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public static class Ads 
{
    private static string _appId = "ca-app-pub-3940256099942544/3419835294"; //Test Id
    private static string _rewardedAdId = "ca-app-pub-3940256099942544/5224354917"; //Test id
    private static RewardedAd _rewardedAd = new RewardedAd(_rewardedAdId);
    private static AdRequest _request;
    
    public static event Action OnRewardGiven;


    public static void ShowRewardedAd()
    {
        _request = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(_request);

        if(_rewardedAd.IsLoaded())
        {
           _rewardedAd.Show();
        }

        _rewardedAd.OnAdFailedToLoad += (sender, args) => Debug.Log("Failed To load Ad");
        _rewardedAd.OnAdFailedToShow += (sender, args) => Debug.Log("Failed To Show Ad");

        _rewardedAd.OnUserEarnedReward += HandleRewardEarnedEvent;
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;

    }

    private static void HandleRewardEarnedEvent(object sender, Reward args)
    {
        OnRewardGiven?.Invoke();
    }


    private static void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        _rewardedAd.OnUserEarnedReward -= HandleRewardEarnedEvent;
        _rewardedAd.OnAdClosed -= HandleRewardedAdClosed;
    }

   


}
