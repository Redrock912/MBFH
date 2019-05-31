using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    UnityAds unityAds;


    public Button addCountButton;

    private void Start()
    {
        unityAds = UnityAds.Instance;

        
    }

    // 유저가 눌렀을때만 확인
    public void OpenPauseMenu()
    {
        if (!unityAds.CheckForAds())
        {
            addCountButton.interactable = false;
        }
        else
        {
            addCountButton.interactable = true;
        }
    }
}
