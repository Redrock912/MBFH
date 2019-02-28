using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

    public int washCount = 10;
    public static int hPoint = 0;
    public int level = 1;

    //
    public static void AddHPoint(int point)
    {
        hPoint += point;
        print(hPoint + " /100");
        if(hPoint >= 100)
        {
            GridScript.DestroyAllTiles();
            
        }
    }

    

    
}
