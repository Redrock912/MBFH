using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

    //public int washCount = 10;
    //public static int hPoint = 0;
    //public int level = 1;



    //// 과거의 유산
    //public static void AddHPoint(int point, GridScript gridScript)
    //{
    //    hPoint += point;

    //    if(hPoint >= 100)
    //    {

    //        gridScript.DestroyAllTiles();
    //        hPoint = 0;
    //    }
    //}


    // 여긴 정말 데이터 저장용으로만 쓰는 곳
    

    //public static StageManager instance;

    


    

    public string[] stageList = { "sena", "gwent" };

    //private void Awake()
    //{
    //    if (instance != null)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        instance = this;

    //        DontDestroyOnLoad(gameObject);
    //    }
    //}
}
