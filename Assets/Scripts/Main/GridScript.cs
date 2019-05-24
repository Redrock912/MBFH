﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    public Tiles tilePrefab;

    // Amount
    public int numberOfTiles = 160;
     public float distanceX = 0.533f;
    public float distanceY = 0.867f;
    public int numberOfMines = 10;
    public int numberOfHidden = 15;
    public int numberOfFlip = 15;
    public int currentMines;
    public int rowLength = 10;


    // Tiles
    public  Tiles[]   allTiles;        
    public  ArrayList plainTiles;
    public  ArrayList mineTiles;
    public ArrayList hiddenTiles;
    public ArrayList flipTiles;
    public static Queue<Tiles> explosionTiles;

    public string stageName;
    public int currentDifficulty;
    public int stageNumber = 0;
    

    public PlayerManager playerManager;
    public Transform startingPoint;
    public Sprite[] currentSpriteSheet;

    public bool isTopGrid = false;
    public bool isHidden = false;

    public void SetupStageInfo(int i, PlayerManager playerManager)
    {
        stageName = playerManager.stageNames[playerManager.currentStage];
        //currentDifficulty = playerManager.currentDifficulty + i;
    }


    // Start is called before the first frame update
    public void MakeGrids()
    {

        // 이 그리드에서 쓰일 배경이미지를 미리 로드한다.
        LoadSprite();

        // 타일을 만든다.
        CreateTiles();

        // 지뢰를 심는다
        SetupMine();

        // 주변 타일들을 설정, 그리고 그걸 기반으로 지뢰 갯수 표시
        SetupAdjacentTiles();

        // 흠터레스팅 타일을 깐다.
        SetupHiddenTile();

        // 논리회로 타일을 깐다
        SetupFlipTile();

    }

    // 스프라이트 로드, 정직한 제목
    public void LoadSprite()
    {
        Sprite[] tempSprites;
        tempSprites = Resources.LoadAll<Sprite>("Spritesheets/MainScreen/" + stageName +"/" + stageName + "_B");

        currentSpriteSheet = tempSprites;
    }




  
    public void CreateTiles()
    {
        
        allTiles = new Tiles[numberOfTiles];
        explosionTiles = new Queue<Tiles>();


        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        

        float xOffSet = 0f;
        float yOffSet = 0f;
        int count = 0;

        for(int i=0;i<numberOfTiles;i++)
        {
            // 한 칸씩 옆으로 이동, distanceX => 1024:1080 = x*10 : 5.625 
            xOffSet += distanceX;
            //xOffSet += worldScreenWidth / (float)rowLength;

            // row row row the boat
            if (i % rowLength == 0)
            {
                xOffSet = 0;
                yOffSet += distanceY;
                //yOffSet += worldScreenWidth / (float)rowLength;
                
            }
            
            //float startingPointX = (float)(Screen.width / (numberOfMines*2));
            //float startingPointY = (float)(Screen.height / (numberOfMines * 7 / 10));
            //startingPoint.SetPositionAndRotation(new Vector3(startingPointX,startingPointY,0), Quaternion.identity);

            Tiles spawnedTile = Instantiate(tilePrefab, startingPoint.position + new Vector3(xOffSet, -yOffSet, 0), Quaternion.identity) as Tiles;

            // 이 코드로 위치를 수정할지는 모르지만 일단 보류
            //spawnedTile.transform.localScale *= 2;
            spawnedTile.GetComponent<Tiles>().SetParentGrid(this);
            spawnedTile.GetComponent<Tiles>().SetData(stageName, currentSpriteSheet[i],rowLength);
            spawnedTile.GetComponent<Tiles>().rowLength = rowLength;
            spawnedTile.GetComponent<Tiles>().id = i;

            
            allTiles[i] = spawnedTile;

       
        }
        
 
    }

    



    void SetupMine()
    {
        // 난이도에 따라 지뢰개수를 달리한다.
        numberOfMines = playerManager.minesByDifficulty[currentDifficulty];
        


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

        // 현재 지뢰 갯수 세기 시작
        currentMines = numberOfMines;
    }


    //  ? 타일 만들기. 위랑 합칠수 있지 않을까
    void SetupHiddenTile()
    {

        hiddenTiles = new ArrayList();

        
        for (int i = 0; i < numberOfHidden; i++)
        {
            Tiles currentTile = (Tiles)plainTiles[Random.Range(0, plainTiles.Count)];
            currentTile.GetComponent<Tiles>().isHidden = true;

            hiddenTiles.Add(currentTile);
            plainTiles.Remove(currentTile);
        }

        // 여기서 값을 미리 바꿔놓자
        
    }

    void SetupFlipTile()
    {
        flipTiles = new ArrayList();

        for(int i = 0; i < numberOfFlip; i++)
        {
            Tiles currentTile = (Tiles)plainTiles[Random.Range(0, plainTiles.Count)];
            
            // 생각해보니 이 부분은 없어도 될듯?
            //currentTile.GetComponent<Tiles>().isFlip = true;


            flipTiles.Add(currentTile);
            plainTiles.Remove(currentTile);
        }
    }


    void SetupAdjacentTiles()
    {
        for(int i=0;i<allTiles.Length;i++)
        {
            allTiles[i].SetNeighbor();
            allTiles[i].SetTileNumber();
        }
    }


    public void FlipTilesToBaseState()
    {
        for (int i = 0; i < flipTiles.Count; i++)
        {

            Tiles currentTile = (Tiles)flipTiles[i];

            if (currentTile.isRevealedThisTurn == true)
            {
                currentTile.isRevealedThisTurn = false;
            }
            else
            {
                currentTile.Flip();
            }
        }
    }

    
    public void HideAllTiles()
    {
        isHidden = true;

        for (int i = 0; i < allTiles.Length; i++)
        {
            // 왜 두번째 Grid에서 첫번째 Tile을 보는가?
            if (allTiles[i].gameObject)
            {
                allTiles[i].setHidden();
            }

        }
    }

    

}