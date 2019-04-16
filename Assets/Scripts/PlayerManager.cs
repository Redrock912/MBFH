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
    public int[] minesByDifficulty = { 5, 10, 15, 25 };
    

    // Game Over~
    public event System.Action OnCountOver;
    public event System.Action OnClear;

    GridManager gridManager;
    GridScript gridScript;


    public string[] stageNames = { "gwent", "sena" };

    private void Awake()
    {
        if(instance != null)
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
        for(int i = 0; i < maxStageNumber; i++)
        {
            if (PlayerPrefs.HasKey("stage" + i))
            {
                stageList[i] = PlayerPrefs.GetInt("stage" + i, 1);
            }
            else
            {

                PlayerPrefs.SetInt("stage" + i, 1);
                stageList[i] = PlayerPrefs.GetInt("stage" + i, 1);
            }
            
        }

    }

    public void FindGridManager()
    {
        gridManager = FindObjectOfType<GridManager>();
        // 현재 탑 그리드스크립트
        if (gridManager != null)
        {
            gridScript = gridManager.gridScript[gridManager.numberOfGrids - 1];
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
        if (gridScript.currentMines == 0)
        {
            // 다 찾으면 클리어, 
            Clear();
            if ((currentDifficulty + 1) == PlayerPrefs.GetInt("stage" + currentStage, 1) && (currentDifficulty + 1) < 4)
            {

                stageList[currentStage] += 1;
                PlayerPrefs.SetInt("stage" + currentStage, stageList[currentStage]);
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
        OnClear();
        // 초기화 해줘야 하나..?
        OnClear = null;
    }



    public void CountOver()
    {
        //if (OnCountOver != null)
        //{
        //    OnCountOver();
        //}

        OnCountOver();
        OnCountOver = null;
        
        // 일단은 부셔볼까
       // GameObject.Destroy(gameObject);
    }
}
