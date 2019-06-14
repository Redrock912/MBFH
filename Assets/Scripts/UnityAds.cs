using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;


public class UnityAds : MonoBehaviour
{
    private const string android_game_id = "3183819";
    private const string ios_game_id = "3183818";
    private const string rewarded_video_id = "rewardedVideo";
    private const string video_id = "video";

    PlayerManager playerManager;

    public bool isReady = false;

    private static UnityAds instance;

    public static UnityAds Instance { get => instance; set => instance = value; }

    private void Start()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            playerManager = PlayerManager.Instance;
            Initialize();
        }


    }

    private void Initialize()
    {
        #if UNITY_ANDROID
        Advertisement.Initialize(android_game_id);
        #elif UNITY_IOS
        Advertisement.Initialize(ios_game_id);
        #endif

        
    }

    public bool CheckForAds()
    {
        if (Advertisement.IsReady(rewarded_video_id))
        {
            isReady = true;
        }
        else
        {
            isReady = false;
        }

        return isReady;
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

    public void ShowAds()
    {
        if (Advertisement.IsReady(video_id))
        {
            Advertisement.Show(video_id);
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
