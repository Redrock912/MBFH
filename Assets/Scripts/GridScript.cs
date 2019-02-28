using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    public Tiles tilePrefab;

    public int numberOfTiles = 100;
    public float distanceX = .65f;
    public float distanceY = .65f;
    public int numberOfMines = 10;
    public int rowLength = 10;
    public Transform startingPoint;

    public static Tiles[]   allTiles;        
    public static ArrayList plainTiles;
    public static ArrayList mineTiles;

    public static Queue<Tiles> explosionTiles;


    // Start is called before the first frame update
    public void MakeGrids()
    {
        // 타일을 만든다.
        CreateTiles();

        // 지뢰를 심는다
        SetupMine();

        // 주변 타일들을 설정, 그리고 그걸 기반으로 지뢰 갯수 표시
        SetupAdjacentTiles();

        


    }


  
    void CreateTiles()
    {
        print(startingPoint.transform.position);
        allTiles = new Tiles[numberOfTiles];
        explosionTiles = new Queue<Tiles>();

        float xOffSet = 0f;
        float yOffSet = 0f;
        int count = 0;
        //for(int i=0;i<numberOfTiles;i++)
        //{
            
            

        //    for(int j=0;j<numberOfTiles;j++)
        //    {

             

        //        Tiles spawnedTile = Instantiate(tilePrefab, startingPoint.position + new Vector3(xOffSet, -yOffSet, 0), Quaternion.identity) as Tiles;

        //        spawnedTile.GetComponent<Tiles>().SetBackground(count,  "flaminica");


        //        allTiles[i, j] = spawnedTile;
        //        xOffSet += distanceX;
        //        count++;
        //    }
        //    xOffSet = 0f;
        //    yOffSet += distanceY;

        //}


        for(int i=0;i<numberOfTiles;i++)
        {
            // 한 칸씩 옆으로 이동
            xOffSet += distanceX;

            // row row row the boat
            if (i % rowLength == 0)
            {
                xOffSet = 0;
                yOffSet += distanceY;
            }


            Tiles spawnedTile = Instantiate(tilePrefab, startingPoint.position + new Vector3(xOffSet, -yOffSet, 0), Quaternion.identity) as Tiles;


            spawnedTile.GetComponent<Tiles>().SetBackground(i, "flaminica");
            spawnedTile.GetComponent<Tiles>().rowLength = rowLength;
            spawnedTile.GetComponent<Tiles>().id = i;

            
            allTiles[i] = spawnedTile;
            
        }

       
    }

    


    void SetupMine()
    {
        plainTiles = new ArrayList(allTiles);
        
        // why is this null?
        // plainTiles.CopyTo(allTiles);

        mineTiles = new ArrayList();
        
        for(int i=0;i<numberOfMines;i++)
        {
            
            Tiles currentTile = (Tiles)plainTiles[Random.Range(0,plainTiles.Count)];
            currentTile.GetComponent<Tiles>().isMine = true;
            

            mineTiles.Add(currentTile);
            plainTiles.Remove(currentTile);
        }

        print(plainTiles.Count);
        print(allTiles.Length);
    }

    void SetupAdjacentTiles()
    {
        for(int i=0;i<allTiles.Length;i++)
        {
            allTiles[i].SetNeighbor();
            allTiles[i].SetTileNumber();
        }
    }

    public static void DestroyAllTiles()
    {
        for(int i=0;i<allTiles.Length;i++)
        {
            if (allTiles[i].gameObject)
            {
                Destroy(allTiles[i].gameObject);
            }
            
        }
    }


}
