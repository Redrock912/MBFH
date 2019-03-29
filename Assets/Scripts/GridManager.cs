using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // 그리드를 가져오자
    public GridScript[] gridScript;
    public GridScript gridScriptPrefab;
    // 임시방편
    public Transform startingPoint;

    // Restart할 경우 이미 사용한 count를 보충해줘야한다.
    PlayerManager playerManager;

    // 나중에는 다른데서 가져오자
    public int numberOfGrids = 2;

    public int count = 20;

    GridScript currentTopGrid;

    // Start is called before the first frame update
    void Awake()
    {
        gridScript = new GridScript[numberOfGrids];

        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.count = count;

        
        

        for (int i=0;i<numberOfGrids;i++)
        {
            GridScript currentGrid = Instantiate(gridScriptPrefab, startingPoint);
            currentGrid.startingPoint = startingPoint;
            currentGrid.playerManager = playerManager;
            currentGrid.startingPoint.position += new Vector3(0, 0, -i);

            currentGrid.SetupStageInfo(i,playerManager);
            //currentGrid.stageNumber = i;
            //currentGrid.currentDifficulty = playerManager.currentDifficulty+i;
            currentGrid.MakeGrids();
            
            gridScript[i] = currentGrid;
        }

        // 현재 최상단
        //currentTopGrid = gridScript[numberOfGrids - 1];
        gridScript[numberOfGrids - 1].isTopGrid = true;
        //SetGridPosition();



    }

    //private void Start()
    //{
    //    playerManager = FindObjectOfType<PlayerManager>();
    //    playerManager.count = count;
    //}



    // 쓰일지 안쓰일지 모르겠지만, 3가지 이상의 레이어를 이용하게 되면 최상단을 계속해서 바꿔주자
    public void ChangeTopGrid()
    {
        
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

}
