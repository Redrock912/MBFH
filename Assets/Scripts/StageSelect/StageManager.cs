using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

    // StageSelect 신에서 Canvas->Container안에 부착. 여기서 PlayerManager를 보고서 언락된 스테이지가 있다면 동작을 수행(애니메이션 포함), 후에 PlayerManager에서 isLevelUnlocked를 false로 바꿔준다. PlayerPrefs관련된 내용은 이미 PlayerManager에서 처리

    PlayerManager playerManager;
    


    private void Start()
    {
        playerManager = PlayerManager.Instance;


        // 시작 위치는 ScrollSnapRect에서 이미 진행되고 있다.
        if (playerManager.isLevelUnlocked)
        {
            
        }
    }
}
