﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update

    // 여기서는 정보를 넘기지말고 이동만하자

    public AudioClip mainTheme;
    public AudioClip stageSelectTheme;
    public AudioClip menuTheme;


    UnityAds unityAds;


    public void ToMainScene()
    {
        SceneManager.LoadScene("Main");
        AudioManager.Instance.PlayMusic(mainTheme, 2);
    }

    public void ToStageSelectScene()
    {


        SceneManager.LoadScene("StageSelect");
        AudioManager.Instance.PlayMusic(stageSelectTheme, 2);
    }

    public void FindUnityAds()
    {

        if (!unityAds)
        {
            unityAds = UnityAds.Instance;
        }

        if (unityAds)
        {
            unityAds.ShowRewardedAd();
        }
    }


    public void AdvertiseToStageSelectScene()
    {
        FindUnityAds();
        Time.timeScale = 1;
        SceneManager.LoadScene("StageSelect");
        AudioManager.Instance.PlayMusic(stageSelectTheme, 2);
    }

    public void AdvertiseRestartScene()
    {
        //UnityAds unityAds = FindObjectOfType<UnityAds>();
        //if (unityAds)
        //{
        //    unityAds.ShowRewardedAd();
        //}
        FindUnityAds();
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
        AudioManager.Instance.PlayMusic(mainTheme, 2);
    }


    public void ShowAdThanAddCount()
    {
        FindUnityAds();

        PlayerManager playerManager = PlayerManager.Instance;
        playerManager.AddCount();
        
    }

    

    
    
    void Start()
    {

    
    }


}
