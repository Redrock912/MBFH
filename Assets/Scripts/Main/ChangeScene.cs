using System.Collections;
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

    public void ToMainScene()
    {
        SceneManager.LoadScene("Main");
        AudioManager.instance.PlayMusic(mainTheme, 2);
    }

    public void ToStageSelectScene()
    {


        SceneManager.LoadScene("StageSelect");
        AudioManager.instance.PlayMusic(stageSelectTheme, 2);
    }


    public void AdvertiseToStageSelectScene()
    {
        UnityAds unityAds = FindObjectOfType<UnityAds>();
        if (unityAds)
        {
            unityAds.ShowRewardedAd();
        }

        SceneManager.LoadScene("StageSelect");
        AudioManager.instance.PlayMusic(stageSelectTheme, 2);
    }

    public void AdvertiseRestartScene()
    {
        UnityAds unityAds = FindObjectOfType<UnityAds>();
        if (unityAds)
        {
            unityAds.ShowRewardedAd();
        }

        SceneManager.LoadScene("Main");
        AudioManager.instance.PlayMusic(mainTheme, 2);
    }




    

    
    
    void Start()
    {
        //Camera sceneCamera = GetComponent<Camera>();
        //if(sceneCamera != null)
        //{
        //    print(sceneCamera.pixelRect);
        //    print(Screen.height);
        //    print(Screen.width);
        //}
    
    }


}
