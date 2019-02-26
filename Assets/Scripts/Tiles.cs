using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{


    // Start is called before the first frame update
    Sprite[] background;
    public Sprite tile1, tile2, tile3, tile4, tile5, tile6, tile7, tile8;
    public bool isMine;
    public int displayNumber=0;
    public int id;
    public int rowLength;


    // 입력 최고 권위자
    private PlayerInput playerInput;

    // 현재 상태는 이것뿐 더 추가된다면 enum이 나 을 듯?
    public bool isRevealed =false;

    

    public Tiles upperLeft, upper, upperRight, left, right, lowerLeft, lower, lowerRight;

    void Start()
    {

        
    }

    private void Awake()
    {
        
        playerInput = GameObject.Find("Grid").GetComponent<PlayerInput>();
    }


    // 일단은 내비두자, 각 왼쪽끝과 오른쪽 끝에서 문제가 생길것같지만.
    private bool InBounds(Tiles[] allTiles, int id)
    {
        if(id<0 || id>=allTiles.Length)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    public void SetAdjacent()
    {
        // ㅜ
        if(InBounds(GridScript.allTiles, id + rowLength))
        {
            lower = GridScript.allTiles[id + rowLength];
            if(lower.isMine)
            {
                displayNumber++;
            }
        }
        // ㅗ
        if (InBounds(GridScript.allTiles, id - rowLength))
        {
            upper = GridScript.allTiles[id - rowLength];
            if (upper.isMine)
            {
                displayNumber++;
            }
        }
        // ㅏ
        if (InBounds(GridScript.allTiles, id + 1) && (id+1)%rowLength != 0)
        {
            right = GridScript.allTiles[id + 1];
            if (right.isMine)
            {
                displayNumber++;
            }
        }
        // ㅓ
        if (InBounds(GridScript.allTiles, id - 1) && id % rowLength != 0)
        {
            left = GridScript.allTiles[id - 1 ];
            if (left.isMine)
            {
                displayNumber++;
            }
        }
        // ㅜ ㅏ
        if (InBounds(GridScript.allTiles, id + rowLength + 1) && (id + 1) % rowLength != 0)
        {
            lowerRight = GridScript.allTiles[id + rowLength + 1];
            if (lowerRight.isMine)
            {
                displayNumber++;
            }
        }
        // ㅗ ㅏ
        if (InBounds(GridScript.allTiles, id - rowLength + 1) && (id + 1) % rowLength != 0)
        {
            upperRight = GridScript.allTiles[id - rowLength + 1];
            if (upperRight.isMine)
            {
                displayNumber++;
            }
        }
        // ㅜ ㅓ
        if (InBounds(GridScript.allTiles, id + rowLength - 1) && id % rowLength != 0)
        {
            lowerLeft = GridScript.allTiles[id + rowLength - 1];
            if (lowerLeft.isMine)
            {
                displayNumber++;
            }
        }

        // ㅗ ㅓ
        if (InBounds(GridScript.allTiles, id - rowLength - 1) && id % rowLength != 0)
        {
            upperLeft = GridScript.allTiles[id - rowLength - 1];
            if (upperLeft.isMine)
            {
                displayNumber++;
            }
        }
    }

    public void SetTileNumber()
    {
        SpriteRenderer[] spriteArray = new SpriteRenderer[2];
        spriteArray = gameObject.GetComponentsInChildren<SpriteRenderer>();
        

        switch (displayNumber)
        {
            case 0:
                break;
            case 1:
                spriteArray[1].sprite = tile1;
                break;
            case 2:
                spriteArray[1].sprite = tile2;
                break;
            case 3:
                spriteArray[1].sprite = tile3;
                break;
            case 4:
                spriteArray[1].sprite = tile4;
                break;
            case 5:
                spriteArray[1].sprite = tile5;
                break;
            case 6:
                spriteArray[1].sprite = tile6;
                break;
            case 7:
                spriteArray[1].sprite = tile7;
                break;
            case 8:
                spriteArray[1].sprite = tile8;

                break;
                


        }
        
        // 일단 숨겨놓자, isRevealed에 반응하도록
        spriteArray[1].enabled = false;
    }

    public void SetBackground(int x, string name)
    {
        background = Resources.LoadAll<Sprite>("Spritesheets/" + name);

        SpriteRenderer[] spriteArray = new SpriteRenderer[2];
        spriteArray = gameObject.GetComponentsInChildren<SpriteRenderer>();

        spriteArray[0].sprite = background[x];

        

      


    }

    //private void OnMouseOver()
    //{
    //    playerInput.OnMouseOver(this);
    //}

    // Update is called once per frame
    void Update()
    {
        // 이건 모바일용으로 잠시...

        //if(Input.touchCount >0)
        //{
        //    Touch touch = Input.GetTouch(0);
            
        //}

       
    }
}
