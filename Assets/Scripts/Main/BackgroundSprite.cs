using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BackgroundSprite : MonoBehaviour
{
    public Sprite background;

    public string stageName;
    public int currentDifficulty;

    public BoxCollider boxCollider;

    Animation anim;

    public bool isInteractive = false;
    public bool isTopGrid = false;

    public Transform localTransform;
    public Vector3 localPosition;


    GridManager parentGridManager;
    PlayerManager playerManagerRef;
    public int id = 0;
    int stageNameIndex;

    public string[] featureTextList;

    public TextMeshProUGUI stageCharacterName;
    public TextMeshProUGUI featureText;

    GameUI currentGameUI;

    public SfxLibrary sfxLibraryPrefab;

    private void Start()
    {
        currentGameUI = FindObjectOfType<GameUI>();


      
    }

    public void SetupTextsForAnimation()
    {
        
       
        
    }



    public void SetupStageInfo( PlayerManager playerManager, GridManager gridManager, int number)
    {

        print(playerManager.currentStage);
        parentGridManager = gridManager;
        stageName = playerManager.stageNames[playerManager.currentStage];
        stageNameIndex = playerManager.currentStage;
        currentDifficulty = playerManager.currentDifficulty;
        boxCollider = GetComponent<BoxCollider>();
        playerManagerRef = playerManager;
        anim = GetComponent<Animation>();
        id = number;
        localTransform = transform;

        stageCharacterName.text = stageName;
        featureText.text = featureTextList[stageNameIndex];

        playerManagerRef.OnGridCleared += OnGridCleared;

        SetupAnimation();
    }

    void SetupAnimation()
    {
        AnimationClip animClip = Resources.Load<AnimationClip>("Animations/Animation_Export/" + stageName + "Animation");
        anim.AddClip(animClip, stageName);
        
        //anim.AddClip(animClip, stageName);
    }


    public void SetupBackground()
    {
    



        background = Resources.Load<Sprite>("Spritesheets/MainScreen/" + stageName +"/" + stageName + "_" + id);
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = background;


        
        float width = background.bounds.size.x;
        float height = background.bounds.size.y;

        SetupCubeSize(width, height);

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;




        // 베이스
        //spriteArray[0].transform.localScale = new Vector3( worldScreenWidth / width,worldScreenHeight/height,1);

        // h * (9/16) * (1/8) , 9/16 = aspect ratio, 1/8 = tile size
        float modifiedHeight = ((worldScreenHeight / height));

        // w * (1/8) , 1/8 = tile size
        float modifiedWidth = (worldScreenWidth / width);
        modifiedWidth = modifiedWidth * 0.948f;
        modifiedHeight = modifiedHeight * 0.867f;
        // 여기서부터!!!

        transform.localScale = new Vector3(modifiedWidth, modifiedHeight, 1);

        //gameObject.transform.position.Set(0, Screen.height / 2, 1);
        

        

    }

    void SetupCubeSize(float x, float y)
    {
        boxCollider.size = new Vector3(x, y, 1);
    }

    public void HideOtherUI()
    {
        currentGameUI.gameObject.SetActive(false);
        AudioManager.Instance.PlaySound(sfxLibraryPrefab.GetClipFromID(3), new Vector3(0, 0, 0));
    }

    void OnGridCleared()
    {
        if (isTopGrid)
        {
            playerManagerRef.isAnimationPlaying = true;
            StartCoroutine("ClearAnimation");
        }
        
    }

    IEnumerator ClearAnimation()
    {

        //localTransform.localPosition += localPosition;
        //anim.Play("LiiaClear1");
        //print(localPosition);


        anim.Play(stageName);
        
        yield return new WaitForSeconds(2.0f);
        

    }


    void AnimationFinished()
    {
        isInteractive = true;


        currentGameUI.gameObject.SetActive(true);
    }

    public void ShowNextGrid()
    {
        if (id != 0)
        {
            // 백그라운드는 사라지도록 하자
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;

            // 다음 카운트 갯수를 보시오 -> 로직 변경(한 스테이지에서는 하나의 카운트로만 사용)
            //playerManagerRef.SetCurrentCountByDifficulty(id-1);


            playerManagerRef.isAnimationPlaying = false;

            // 남은 갯수를 총합에 추가해주자
            playerManagerRef.AddScore(playerManagerRef.count * 2);
        }
        else
        {
            // 스테이지를 아예 깻다면
            // 남은 갯수를 총합에 추가해주자
            playerManagerRef.AddScore(playerManagerRef.count * 2);
            
        }

    }
}
