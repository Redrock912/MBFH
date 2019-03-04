using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

    public int washCount = 10;
    public static int hPoint = 0;
    public int level = 1;

    //
    public static void AddHPoint(int point, GridScript gridScript)
    {
        hPoint += point;
        
        if(hPoint >= 100)
        {
            
            gridScript.DestroyAllTiles();
            hPoint = 0;
        }
    }

    

    
}
