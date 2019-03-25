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

    // 나중에는 다른데서 가져오자
    public int numberOfGrids = 2;

    GridScript currentTopGrid;

    // Start is called before the first frame update
    void Awake()
    {
        gridScript = new GridScript[numberOfGrids];
        
        for(int i=0;i<numberOfGrids;i++)
        {
            GridScript currentGrid = Instantiate(gridScriptPrefab, startingPoint);
            currentGrid.startingPoint = startingPoint;
            currentGrid.startingPoint.position += new Vector3(0, 0, -i);
            currentGrid.stageNumber = i;
            currentGrid.MakeGrids();
            
            gridScript[i] = currentGrid;
        }

        // 현재 최상단
        //currentTopGrid = gridScript[numberOfGrids - 1];
        gridScript[numberOfGrids - 1].isTopGrid = true;
        //SetGridPosition();
       
        
    }


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
