using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static PlayerManager instance;
    // 골드는 표시용
    int gold;
    

    // 카운트는 게임 로직용, 실제로 이 값을 지정하는 로직은 GridManager
    public int count = 15;
    public int currentScore = 0;

    // 진행도, 귀찮지만 하두코딩
    public int[] stageList = { 1 };
    public int maxStageNumber = 20;
    public int levelUnlocked = 0;
 

    public int[] tutorialList = { 0 };
    public int maxTutorialNumber = 3;

    public int currentStage;
    public int currentDifficulty;
    public int currentGrid = 0;
    public int[] minesByDifficulty = { 3, 2, 1};
    public int[] countByDifficulty = { 3, 15, 9 };
    public int[] hiddensByDifficulty = { 0, 0, 0 };
    public int[] flipsByDifficulty = { 0, 0, 0 };
  

    // 타일들의 사용여부를 확인하기 위한 용도
    // 0 = 노말타일
    // 1 = 숨겨진 타일
    // 2 = 플립타일
    // 3 = 복합
    public enum State { Normal, Qtype,Flip,Hybrid};
    public State[] mineStateByStage;


    // 혼자만 알지말고 다른 애들도 좀 알려줘라
    public event System.Action OnCountOver;
    public event System.Action OnClear;
    public event System.Action OnClick;
    public event System.Action OnGridCleared;

    public GridManager gridManager;
    GridScript gridScript;

    // 조건값 
    public bool isAnimationPlaying = false;
    public bool isGalleryMode = false;
    public bool isPaused = false;
    public bool isSeeingTutorial = false;
    public bool isLevelUnlocked = false;
    public bool isCountAddShown = false;
    public int stageDifficulty = 0;
    

    // 왜 프리팹으로 만들어서 고생하는가..
    public string[] stageNames = { "liia", "lilith","msg","megu","wasabi","yuki","sena","mayu","eione","pepper" };

    public static PlayerManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);




            //OnCountOver = null;

        }
    }

    private void Start()
    {

        Screen.SetResolution(Screen.width, (Screen.width * 16)/9, true);

        

        stageList = new int[maxStageNumber];
        tutorialList = new int[maxTutorialNumber];


        // 플레이어 정보가져오기 (게임 시작 시 사용할 용도) , 뒤에 파라미터는 디폴트값
        for (int i = 0; i < maxStageNumber; i++)
        {
            if (PlayerPrefs.HasKey("stage" + i))
            {
                
                stageList[i] = PlayerPrefs.GetInt("stage" + i, 0);
            }
            else
            {
               
                PlayerPrefs.SetInt("stage" + i, 0);
                stageList[i] = PlayerPrefs.GetInt("stage" + i, 0);
            }

        }

        // 튜토리얼을 어디까지 보았는지 체크
        for(int i = 0; i < maxTutorialNumber; i++)
        {
            if (PlayerPrefs.HasKey("tutorial" + i))
            {
                tutorialList[i] = PlayerPrefs.GetInt("tutorial" + i, 0);
            }
            else
            {
                PlayerPrefs.SetInt("tutorial" + i, 0);
                tutorialList[i] = PlayerPrefs.GetInt("tutorial" + i, 0);
            }
        }

        // 레벨은 어디까지 뚫었는가
        if (PlayerPrefs.HasKey("level"))
        {
            levelUnlocked = PlayerPrefs.GetInt("level", 0);
        }
        else
        {
            PlayerPrefs.SetInt("level", 0);
            levelUnlocked = PlayerPrefs.GetInt("level", 0);
        }
    }

    public void FindCurrentTopGrid()
    {
        
       
        // 현재 탑 그리드스크립트
        if (gridManager != null)
        {
            
            gridScript = gridManager.gridScript[gridManager.CurrentTop];
        }

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerPrefs.DeleteAll();
        }

    }

    public void UseCount(bool isMine)
    {
        if (!isMine)
        {
            count--;
        }
        
        // 일단 명시적으로 해보자

        FindCurrentTopGrid();
        if (gridScript.currentMines == 0 )
        {
            // 다 찾으면 클리어, 
            print("CurrentTop = " + gridManager.CurrentTop);
            print("CurrentGrid's difficulty = " + gridScript.currentDifficulty);

            gridScript.HideAllTiles();

            // 현재처럼 딱 3번만 깨면 되면, 별 갯수를 올릴 때 이것만 해주면 된다
            currentGrid += 1;


            if (gridManager.CurrentTop == 0)
            {
                Clear();

            }
            else
            {
                GridCleared();
                gridManager.SetNextGridToTheTopGrid();
                
                //gridScript = gridManager.gridScript[gridManager.CurrentTop];
                //print("Grid's difficulty = " + gridScript.currentDifficulty);
            }
            
            
            

        }
        else if (count == 0)
        {
            CountOver();
            // 일단 클리어 시에 다른거 터치 막아놓기
            gridScript.isTopGrid = false;
            
        }
    }

    public void Clear()
    {
        int realCurrentStage = currentStage + currentDifficulty * 10;

        // 체크
        if (PlayerPrefs.HasKey("stage" + realCurrentStage))
        {
            // 더 크다면 바깥을 더 큰값으로 바꾸자
            if (PlayerPrefs.GetInt("stage" + realCurrentStage) < currentGrid)
            {
                PlayerPrefs.SetInt("stage" + realCurrentStage, currentGrid);
            }

        }

        // 언락 레벨
        UnlockLevel(realCurrentStage);

        OnClear();
        // 초기화 해줘야 하나..?
        OnClear = null;
    }


    public void CountOver()
    {

        // 체크
        int realCurrentStage = currentStage + currentDifficulty * 10;

        if(PlayerPrefs.HasKey("stage"+realCurrentStage))
        {


            // 더 크다면 바깥을 더 큰값으로 바꾸자
            if(PlayerPrefs.GetInt("stage"+realCurrentStage)<currentGrid)
            {
                PlayerPrefs.SetInt("stage" + realCurrentStage, currentGrid);

                UnlockLevel(realCurrentStage);

            }
            
        }
        
        OnCountOver();
        OnCountOver = null;
       

        // 일단은 부셔볼까
        // GameObject.Destroy(gameObject);
    }


    // 처음으로 클리어 시에 다음 레벨을 언락한다.
    void UnlockLevel(int currentLevel)
    {

        print("Inside UnlockLevel function!");

        // 최종 레벨이면 하지말자, 그리고 현재 가장 높은 레벨을 기준으로 하자
        if(currentLevel < maxStageNumber-1 && levelUnlocked == currentLevel)
        {

            print("Really Inside!");
            if(PlayerPrefs.HasKey("stage" + (currentLevel + 1)))
            {
                print("INSIDEINSIDE!!!");
                isLevelUnlocked = true;
                levelUnlocked += 1;
                PlayerPrefs.SetInt("level", levelUnlocked);
            }
        }
    }

    public void Click()
    {
        OnClick();
    }

    public void GridCleared()
    {
        OnGridCleared();
    }

    public void ResetGridClearEvents(GridManager currentGridManager)
    {
        gridManager = currentGridManager;
        OnGridCleared = null;
    }





    // 난이도에 따라 시도 횟수 배정
    public void SetCurrentCountByDifficulty(int difficulty)
    {
        


        count = countByDifficulty[difficulty];
    }



    // 스코어 관련

    public void ResetScore()
    {
        currentScore = 0;
    }

    public void AddScore(int score)
    {
        currentScore += score;
    }

    // 광고 보상용도
    public void AddCount()
    {
        count += 5;
        isCountAddShown = true;
    }

    // 광고 보고 난 후에 다시 플레이 (게임 오버 당한 경우)
    public void Revive()
    {
        gridScript.isTopGrid = true;
    }
}
