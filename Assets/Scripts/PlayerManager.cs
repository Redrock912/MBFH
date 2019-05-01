using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static PlayerManager instance;
    // 골드는 표시용
    int gold;

    // 카운트는 게임 로직용, 실제로 이 값을 지정하는 로직은 GridManager
    public int count = 15;

    // 진행도, 귀찮지만 하두코딩
    public int[] stageList = { 1 };
    public int maxStageNumber = 2;
    public int currentStage;
    public int currentDifficulty;
    public int[] minesByDifficulty = { 3, 2, 1, 25 };
    public int[] countByDifficulty = { 3, 3, 3 };
    public int currentGrid = 0;

    // Game Over~
    public event System.Action OnCountOver;
    public event System.Action OnClear;

    GridManager gridManager;
    GridScript gridScript;


    public string[] stageNames = { "lila_", "lilith_" };

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(gameObject);




            //OnCountOver = null;

        }
    }

    private void Start()
    {

        Screen.SetResolution(450, 800, false);

        FindGridManager();

        stageList = new int[maxStageNumber];



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

    }

    public void FindGridManager()
    {
        gridManager = FindObjectOfType<GridManager>();
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

    public void UseCount()
    {
        count--;
        // 일단 명시적으로 해보자

        FindGridManager();
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
        // 체크
        if (PlayerPrefs.HasKey("stage" + currentStage))
        {
            // 더 크다면 바깥을 더 큰값으로 바꾸자
            if (PlayerPrefs.GetInt("stage" + currentStage) < currentGrid)
            {
                PlayerPrefs.SetInt("stage" + currentStage, currentGrid);
            }

        }

        OnClear();
        // 초기화 해줘야 하나..?
        OnClear = null;
    }



    public void CountOver()
    {

        // 체크
        if(PlayerPrefs.HasKey("stage"+currentStage))
        {
            // 더 크다면 바깥을 더 큰값으로 바꾸자
            if(PlayerPrefs.GetInt("stage"+currentStage)<currentGrid)
            {
                PlayerPrefs.SetInt("stage" + currentStage, currentGrid);
            }
            
        }
        
        OnCountOver();
        OnCountOver = null;

        // 일단은 부셔볼까
        // GameObject.Destroy(gameObject);
    }

    // 난이도에 따라 시도 횟수 배정
    public void SetCurrentCountByDifficulty(int difficulty)
    {
        count = countByDifficulty[difficulty];
    }
}
