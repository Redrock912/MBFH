﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Stage : MonoBehaviour
{
    PlayerManager playerManager;
    public int stageID;
    public int unlockedDifficulties =1;

    public Button[] buttonLists;

    private void Start()
    {

        // 작은 버튼들에게도 소속감을 주자
        for(int i=0;i<buttonLists.Length;i++)
        {
            buttonLists[i].GetComponent<StageButton>().stageID = stageID;
        }
        
    }


    public void SetButtons()
    {
        int count = 0;
        while(count < unlockedDifficulties)
        {
            //에디터에서만 사이즈를 설정하고 여기선안했는데 그래도 작동할까? 결과: 작동됨
            buttonLists[count].interactable = true;
            
            // 색 정하는 부분 아직 미정
            //buttonLists[count].GetComponent<Image>().color = new Color(255, 255, 255, 50);
            count++;
        }
    }

    // StageSelect에서 버튼을 클릭했을 때 발동.
    //void StartStage()
    //{
    //    // 현재 무슨 스테이지를 풀레이중인지 무슨 난이도를 하는지 알려줌
    //    playerManager.currentStage = stageID;
        
    //}

}
