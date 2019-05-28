using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // 그리드를 가져오자
    public GridScript[] gridScript;
    public GridScript gridScriptPrefab;
    public BackgroundSprite[] backgroundSprite;
    public BackgroundSprite backgroundSpritePrefab;
    // 임시방편, 임시방편2
    public Transform startingPoint;
    public Transform backgroundStartingPoint;

    // Restart할 경우 이미 사용한 count를 보충해줘야한다.
    PlayerManager playerManager;

    //public event System.Action OnGridCleared;

    // 갯수
    public int numberOfGrids = 3;
    public int count = 5;

    int currentTop;
    
    GridScript currentTopGrid;
    public int CurrentTop { get => currentTop; set => currentTop = value; }
    

    // Start is called before the first frame update
    void Awake()
    {
        //초 기 화
        gridScript = new GridScript[numberOfGrids];
        backgroundSprite = new BackgroundSprite[numberOfGrids];

        playerManager = FindObjectOfType<PlayerManager>();
        // 스테이지 정보 초기화 (별 갯수)
        playerManager.currentGrid = 0;
        playerManager.isPaused = false; 
        playerManager.ResetScore();
        playerManager.ResetGridClearEvents();

      

        for(int i=0;i<numberOfGrids;i++)
        {
            
            BackgroundSprite currentBackgroundSprite = Instantiate(backgroundSpritePrefab, backgroundStartingPoint);

            currentBackgroundSprite.SetupStageInfo(playerManager,this,i);
            currentBackgroundSprite.SetupBackground();
            // 여기는 Instantiate 시에 위의 positionObject의 자식으로 되기때문에 로컬 좌표계를 움직여야한다.
            currentBackgroundSprite.transform.position += new Vector3(0, 0, -2 * i);

            // 리스트로 관리하자
            backgroundSprite[i] = currentBackgroundSprite;
        }

        
        for (int i=0;i<numberOfGrids;i++)
        {
            GridScript currentGrid = Instantiate(gridScriptPrefab, startingPoint);
            currentGrid.currentDifficulty = i;
            currentGrid.startingPoint = startingPoint;
            currentGrid.playerManager = playerManager;
            currentGrid.startingPoint.position += new Vector3(0, 0, -2);

            currentGrid.SetupStageInfo(i,playerManager);
            //currentGrid.stageNumber = i;
            //currentGrid.currentDifficulty = playerManager.currentDifficulty+i;
            currentGrid.MakeGrids();

            // 리스트로 관리하자
            gridScript[i] = currentGrid;
        }

        // 현재 최상단
        //currentTopGrid = gridScript[numberOfGrids - 1];
        CurrentTop = numberOfGrids - 1;
        gridScript[CurrentTop].isTopGrid = true;
        backgroundSprite[CurrentTop].isTopGrid = true;

        // 현재 스테이지에 따른 카운트값 주기
        playerManager.SetCurrentCountByDifficulty(playerManager.currentStage);
        

        //SetGridPosition();



    }


    void SetGridPosition()
    {
        for(int i=0;i<gridScript.Length;i++)
        {
            gridScript[i].startingPoint = startingPoint;
            gridScript[i].startingPoint.position += new Vector3(0, 0, -i);
            gridScript[i].MakeGrids();
        }
    }


    public void SetNextGridToTheTopGrid()
    {
        if(CurrentTop > 0)
        {
            //
            

            CurrentTop -= 1;
            backgroundSprite[CurrentTop].isTopGrid = true;
            gridScript[CurrentTop].isTopGrid = true;

            

            //GridCleared();
        }
        else
        {
            print("There is no other grid left");
            
        }

    }

    //void GridCleared()
    //{
    //    OnGridCleared();
        
        
    //}
}
