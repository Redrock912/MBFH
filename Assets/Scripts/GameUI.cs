using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    // Start is called before the first frame update

    public Image clearImage;
    public Image countOverImage;
    public Text gachaUI;
    public Text bombUI;
    public Text layerUI;
    public Text scoreUI;
    public Text layerTextMesh;
    public Text bombTextMesh;
    public Text gachaTextMesh;
    public CanvasGroup returnButton;

    public RawImage upperUI;
    public Image timerBar;

    public StarController starContainerPrefab;

    Animation anim;
    public float maxTime = 5.0f;
    float leftTime = 0f;

    PlayerManager playerManager;
    GridManager gridManager;

    void Start()
    {
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

        layerUI.text = (gridManager.CurrentTop + 1).ToString("D2");

        if (timerBar.enabled)
        {
            TimerBarUpdate();
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


            }
            else
            {
                // 시간을 다 쓸 시에 행동갯수를 하나씩 줄인다.
                playerManager.UseCount();
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
        scoreUI.enabled = false;
        timerBar.enabled = false;
        layerTextMesh.enabled = false;
        bombTextMesh.enabled = false;
        gachaTextMesh.enabled = false;

        returnButton.alpha = 1;
        returnButton.interactable = true;
    }



    // 다시 리셋시키자.
    void OnClick()
    {
        leftTime = maxTime;
    }

    void OnCountOver()
    {
        print("GameUI OnCountOver");
        if (countOverImage != null)
        {
            //StartCoroutine("CountOverImageAnimation");
            anim.Play("CountOverAnimation");
        }

    }




    void OnClear()
    {
        print("GameUI OnClear");


        if (clearImage != null)
        {
            //StartCoroutine("ClearImageAnimation");
            HideOtherUI();
            anim.Play("ClearAnimation");
        }

    }

    public void AfterClearAnimation()
    {
        StarController starContainer = Instantiate(starContainerPrefab, transform);
        starContainer.transform.parent = clearImage.transform;
        starContainer.tier = playerManager.currentGrid;
        starContainer.PlayAnimation();

    }

    public void ShowStarsDuringCountOverAnimation()
    {
        StarController starContainer = Instantiate(starContainerPrefab, transform);

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
