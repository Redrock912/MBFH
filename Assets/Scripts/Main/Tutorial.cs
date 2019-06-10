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
    int id;
    bool hasSeenFirstPage = false;

    public RectTransform middleHolder;
    public RectTransform rightHolder;

    public Button confirmButton;
    public Sprite[] buttonSprites;
    public Sprite[] buttonSelectedSprites;
    public int[] buttonPosition;

    [System.Serializable]
    public struct TutorialSprites
    {
        public int index;
        public Sprite[] spriteArrays;
    }

    public List<TutorialSprites> tutorialSprites;
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
            // 혹시나 모르니 시간을 1로 다시 바꾸자. (버그가 어디서 터지는지 모름)
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

    }

    void PlayTutorial(int tutorialId)
    {
        id = tutorialId;
        playerManagerRef.isSeeingTutorial = true;
        Sprite[] tutorialSprite = tutorialSprites[id].spriteArrays;


        hasSeenFirstPage = false;
        tutorialImage.sprite = tutorialSprite[0];


        //
        SetupButtonPosition(buttonPosition[id]);

        if(id == 0)
        {
            SetupButtonState(1);
        }
        else
        {
            SetupButtonState(0);
        }
        
        canvasGroup.interactable = true;
        canvasGroup.alpha = 1;

        // 이 부분은 나중에 애니메이션을 쓴다면 변경
        Time.timeScale = 0;
        // 봤다고 체크
        PlayerPrefs.SetInt("tutorial" + tutorialId, 1);
        playerManagerRef.tutorialList[tutorialId] = 1;


    }

    void SetupButtonState(int id)
    {
        SpriteState spriteState = new SpriteState();
        spriteState.pressedSprite = buttonSelectedSprites[id];
        confirmButton.spriteState = spriteState;
        confirmButton.image.sprite = buttonSprites[id];
    }


    void SetupButtonPosition(int direction)
    {
        // direction 
        // 0 is middle , 1 is right

        if(direction == 0)
        {
            confirmButton.transform.localPosition = middleHolder.localPosition;
        }else if (direction == 1)
        {
            confirmButton.transform.localPosition = rightHolder.localPosition;
        }

    }


    public void NextTutorial()
    {
        if (hasSeenFirstPage)
        {
            CloseTutorial();
        }
        else if (tutorialSprites[id].spriteArrays.Length > 1)
        {
            Sprite nextSprite = tutorialSprites[id].spriteArrays[1];
            tutorialImage.sprite = nextSprite;
            confirmButton.transform.localPosition = rightHolder.localPosition;

            SpriteState spriteState = new SpriteState();
            spriteState.pressedSprite = buttonSelectedSprites[0];
            confirmButton.spriteState = spriteState;
            confirmButton.image.sprite = buttonSprites[0];
            SetupButtonPosition(1);
            hasSeenFirstPage = true;
        }
        else
        {
            CloseTutorial();
        }









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
