using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerManager playerManagerRef;

    CanvasGroup canvasGroup;

    public Image tutorialImage;

    public int[] showTutorialStageList = { 0, 2, 4 };
    bool isShown = false;
    void Start()
    {
        playerManagerRef = PlayerManager.Instance;

        canvasGroup = GetComponent<CanvasGroup>();
        


        // 초기 설정
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0;


        for (int i = 0; i < showTutorialStageList.Length; i++)
        {
            // 현재 스테이지가 튜토리얼을 보여줘야 되는 스테이지인지 확인한 후, 튜토리얼을 보여줬었는지 확인
            if (playerManagerRef.currentStage == showTutorialStageList[i] && playerManagerRef.tutorialList[i] == 0)
            {



                PlayTutorial(i);
                isShown = true;
            }
        }


        // 실행안됬다면 없어지도록 하자
        if (!isShown)
        {
            gameObject.SetActive(false);
        }

    }

    void PlayTutorial(int tutorialId)
    {
        playerManagerRef.isSeeingTutorial = true;
        Sprite tutorialSprite = Resources.Load<Sprite>("Spritesheets/MainScreen/tutorial/tutorial" + tutorialId);
        print(tutorialImage);
        tutorialImage.sprite = tutorialSprite;


        
        canvasGroup.interactable = true;
        canvasGroup.alpha = 1;

        // 이 부분은 나중에 애니메이션을 쓴다면 변경
        Time.timeScale = 0;
        // 봤다고 체크
        PlayerPrefs.SetInt("tutorial" + tutorialId, 1);


    }

    public void CloseTutorial()
    {
        // 이 부분은 나중에 애니메이션을 쓴다면 변경
        Time.timeScale = 1;
        playerManagerRef.isSeeingTutorial = false;
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0;

        gameObject.SetActive(false);
    }


}
