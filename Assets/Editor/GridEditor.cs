using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor (typeof(GridScript))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // update everytime
        base.OnInspectorGUI();


        // 업데이트하면 바꾸자.
        //GridScript grid = target as GridScript;

        //if (DrawDefaultInspector())
        //{
        //    grid.CreateTiles();
        //}
    }
}
