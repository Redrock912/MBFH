﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    // Start is called before the first frame update

    public Image clearImage;
    public Image countOverImage;
    public TextMeshProUGUI gachaUI;
    public TextMeshProUGUI bombUI;
    public TextMeshProUGUI layerUI;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI scoreTMP;
    public TextMeshProUGUI layerTextMesh;
    public TextMeshProUGUI bombTextMesh;
    public TextMeshProUGUI gachaTextMesh;
    public TextMeshProUGUI stageNameTMP;
    public TextMeshProUGUI timerTMP;
    public GameObject pauseButton;

    public Button countOverAddCountButton;
    public Button pauseMenuAddCountButton;

    public RectTransform countOverStarHolder;

    public CanvasGroup returnButton;
    public CanvasGroup pauseMenu;
    

    public RawImage upperUI;
    public Image timerBar;
    public Image timerBarBackground;

    public Transform[] menuLists;

    public StarController starContainerPrefab;

    Animation anim;
    public float maxTime = 5.0f;
    float leftTime = 0f;
    bool isPaused = false;
    
    PlayerManager playerManager;
    GridManager gridManager;

    Color initialTimerBarColor;

    UnityAds unityAds;

    void Start()
    {
        unityAds = UnityAds.Instance;

        playerManager = FindObjectOfType<PlayerManager>();

        playerManager.OnCountOver += OnCountOver;
        playerManager.OnClear += OnClear;
        playerManager.OnClick += OnClick;

        //GameObject clearImageObject = GameObject.FindGameObjectWithTag("ClearImage");

        //GameObject countOverImageObject = GameObject.FindGameObjectWithTag("CountOverImage");

        //clearImage = clearImageObject.GetComponent<RectTransform>();

        //countOverImage = countOverImageObject.GetComponent<RectTransform>();

        gridManager = FindObjectOfType<GridManager>();

        anim = GetComponent<Animation>();

        leftTime = maxTime;

        // 처음에는 숨겨두고
        returnButton.alpha = 0;
        returnButton.interactable = false;

        stageNameTMP.text = playerManager.stageNames[playerManager.currentStage];

        initialTimerBarColor = new Color(0.24f, 0.66f, 0.96f); 
        
    }

    // Update is called once per frame
    void Update()
    {

        // elapsedTime += Time.deltaTime;

        // timeUI.text = (initialTime - elapsedTime).ToString("F1");

        // Temporary ?
        gachaUI.text = (playerManager.count).ToString("D2");

        // 이 부분도 여러 레이어를 가정해서 만들었지만, 현재는 0 사용
        bombUI.text = (gridManager.gridScript[gridManager.CurrentTop].currentMines).ToString("D2");

        layerUI.text = (gridManager.CurrentTop + 1).ToString("D1");

        if (timerBar.enabled)
        {
            TimerBarUpdate();
        }

        scoreUI.text = (playerManager.currentScore).ToString("D6");

        if(playerManager.isCountAddShown)
        {
            countOverAddCountButton.interactable = false;
            pauseMenuAddCountButton.interactable = false;
        }
    }

    void TimerBarUpdate()
    {
        // count 가 있을 시에만 하자
        if (playerManager.count > 0 && playerManager.isAnimationPlaying == false)
        {
            if (leftTime > 0)
            {
                leftTime -= Time.deltaTime;

                timerBar.fillAmount = leftTime / maxTime;



                if(timerBar.fillAmount < 0.25)
                {
                    timerBar.color = Color.red;
                }
            }
            else
            {
                // 시간을 다 쓸 시에 행동갯수를 하나씩 줄인다.
                playerManager.UseCount(false);
                timerBar.color = initialTimerBarColor;
                leftTime = maxTime;
            }
        }

    }

    // 구에엑
    void HideOtherUI()
    {
        gachaUI.enabled = false;
        bombUI.enabled = false;
        layerUI.enabled = false;
        
        timerBar.enabled = false;
        timerBarBackground.enabled = false;
        layerTextMesh.enabled = false;
        bombTextMesh.enabled = false;
        gachaTextMesh.enabled = false;
        timerTMP.enabled = false;
        pauseButton.SetActive(false);
    }

    void HideOtherUIExceptPause()
    {
        scoreUI.enabled = false;
        gachaUI.enabled = false;
        bombUI.enabled = false;
        layerUI.enabled = false;

        scoreTMP.enabled = false;
        timerBar.enabled = false;
        timerBarBackground.enabled = false;
        layerTextMesh.enabled = false;
        bombTextMesh.enabled = false;
        gachaTextMesh.enabled = false;
        timerTMP.enabled = false;
        stageNameTMP.enabled = false;
        pauseButton.SetActive(false);
    }

    void RevealOtherUIExceptPause()
    {
        scoreUI.enabled = true;
        gachaUI.enabled = true;
        bombUI.enabled = true;
        layerUI.enabled = true;

        scoreTMP.enabled = true;
        timerBar.enabled = true;
        timerBarBackground.enabled = true;
        layerTextMesh.enabled = true;
        bombTextMesh.enabled = true;
        gachaTextMesh.enabled = true;
        timerTMP.enabled = true;
        stageNameTMP.enabled = true;
        pauseButton.SetActive(true);
    }



    // 다시 리셋시키자.
    void OnClick()
    {
        timerBar.color = initialTimerBarColor;
        leftTime = maxTime;
    }

    void OnCountOver()
    {
        

        if (countOverImage != null)
        {
            countOverImage.gameObject.SetActive(true);

            //StartCoroutine("CountOverImageAnimation");
            anim.Play("CountOverAnimation");
        }

    }


    

    void OnClear()
    {
        


        if (clearImage != null)
        {
            //StartCoroutine("ClearImageAnimation");
            HideOtherUIExceptPause();
            returnButton.alpha = 1;
            returnButton.interactable = true;
            anim.Play("ClearAnimation");
           
        }

    }

    public void AfterClearAnimation()
    {
        
        
        StarController starContainer = Instantiate(starContainerPrefab, clearImage.GetComponent<RectTransform>().GetChild(0).transform);
        starContainer.transform.parent = clearImage.transform;
        starContainer.tier = playerManager.currentGrid;
        starContainer.PlayAnimation();

    }

    public void ShowStarsDuringCountOverAnimation()
    {
        StarController starContainer = Instantiate(starContainerPrefab, countOverStarHolder);

        // 생성된 다음에 붙자. 같이 움직이게
        starContainer.transform.parent = countOverImage.transform;
        starContainer.tier = playerManager.currentGrid;
        starContainer.PlayAnimation();
    }

    public void FisnishClearAnimation()
    {
        clearImage.enabled = false;
        clearImage.GetComponentInChildren<StarController>().Vanish();



    }

    public void OnPause()
    {
        isPaused = !isPaused;

        playerManager.isPaused = isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            pauseMenu.alpha = 1;
            pauseMenu.interactable = true;
            HideOtherUIExceptPause();
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.alpha = 0;
            pauseMenu.interactable = false;
            RevealOtherUIExceptPause();
        }

        CheckUnityAds();
        

    }

    public void DisableOtherMenus(GameObject exceptThisObject)
    {
        for(int i = 0; i < menuLists.Length; i++)
        {
            if(menuLists[i].gameObject != exceptThisObject)
            {
                menuLists[i].gameObject.SetActive(false);
            }
        }
    }


    public void CheckUnityAds()
    {
        if (unityAds.CheckForAds())
        {
            countOverAddCountButton.interactable = true;
        }
        else
        {
            countOverAddCountButton.interactable = false;
        }

        //if (playerManager.isCountAddShown)
        //{
        //    countOverAddCountButton.interactable = false;
        //}
        
    }

    public void ClosePauseButtonAfterAds()
    {
        // 다시 설정하고
        playerManager.OnCountOver += OnCountOver;

        countOverImage.gameObject.SetActive(false);
        playerManager.Revive();
    }

    //IEnumerator ClearImageAnimation()
    //{
    //    float speed = 1;
    //    float animatePercent = 0;
    //    while (animatePercent < 1)
    //    {
    //        animatePercent += Time.deltaTime * speed;
    //        clearImage.anchoredPosition = Vector2.up * Mathf.Lerp(-750, 0, animatePercent);

    //        yield return null;
    //    }



    //    //yield return new WaitForSeconds(0.2f);
    //}

    //IEnumerator CountOverImageAnimation()
    //{
    //    float speed = 1;
    //    float animatePercent = 0;
    //    while (animatePercent < 1)
    //    {
    //        animatePercent += Time.deltaTime * speed;
    //        countOverImage.anchoredPosition = Vector2.up * Mathf.Lerp(-1000, 0, animatePercent);

    //        yield return null;
    //    }


    //}
}
