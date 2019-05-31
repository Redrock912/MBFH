using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;


public class UnityAds : MonoBehaviour
{
    private const string android_game_id = "3107965";
    private const string ios_game_id = "3107964";
    private const string rewarded_video_id = "rewardedVideo";

    PlayerManager playerManager;


    private void Start()
    {
        playerManager = PlayerManager.Instance;
        Initialize();
    }

    private void Initialize()
    {
        #if UNITY_ANDROID
        Advertisement.Initialize(android_game_id);
        #elif UNITY_IOS
        Advertisement.Initialize(ios_game_id);
        #endif

        
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady(rewarded_video_id))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };

            Advertisement.Show(rewarded_video_id, options);

        }
        else
        {
            
            Debug.Log("Ad is not ready");
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                {
                    Debug.Log("The ad was shown");
                    break;
                }
            case ShowResult.Skipped:
                {
                    Debug.Log("The ad was skipped");
                    break;
                }

            case ShowResult.Failed:
                {
                    Debug.LogError("Ad was failed to be shown");
                    break;
                }
        }
    }
}
