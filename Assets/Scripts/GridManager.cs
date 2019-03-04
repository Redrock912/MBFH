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
    public int numberOfGrids = 4;
    


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

}
