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

    public int hPoint = 3;

    // 입력 최고 권위자
    private PlayerInput playerInput;

    private SpriteRenderer[] spriteArray = new SpriteRenderer[2];

    // 현재 상태는 이것뿐 더 추가된다면 enum이 나 을 듯?
    public bool isRevealed =false;
    public bool isExploded = false;

    

    public Tiles upperLeft, upper, upperRight, left, right, lowerLeft, lower, lowerRight;
    public List<Tiles> neighborTiles;

    void Start()
    {

        
    }

    private void Awake()
    {
        
        playerInput = GameObject.Find("GridManager").GetComponent<PlayerInput>();
        
        spriteArray = gameObject.GetComponentsInChildren<SpriteRenderer>();
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


    public void SetNeighbor()
    {
        // ㅜ
        if(InBounds(GridScript.allTiles, id + rowLength))
        {
            lower = GridScript.allTiles[id + rowLength];
            if(lower.isMine)
            {
                displayNumber++;
            }

            neighborTiles.Add(lower);
        }
        // ㅗ
        if (InBounds(GridScript.allTiles, id - rowLength))
        {
            upper = GridScript.allTiles[id - rowLength];
            if (upper.isMine)
            {
                displayNumber++;
            }
            neighborTiles.Add(upper);
        }
        // ㅏ
        if (InBounds(GridScript.allTiles, id + 1) && (id+1)%rowLength != 0)
        {
            right = GridScript.allTiles[id + 1];
            if (right.isMine)
            {
                displayNumber++;
            }
            neighborTiles.Add(right);
        }
        // ㅓ
        if (InBounds(GridScript.allTiles, id - 1) && id % rowLength != 0)
        {
            left = GridScript.allTiles[id - 1 ];
            if (left.isMine)
            {
                displayNumber++;
            }
            neighborTiles.Add(left);
        }
        // ㅜ ㅏ
        if (InBounds(GridScript.allTiles, id + rowLength + 1) && (id + 1) % rowLength != 0)
        {
            lowerRight = GridScript.allTiles[id + rowLength + 1];
            if (lowerRight.isMine)
            {
                displayNumber++;
            }

            neighborTiles.Add(lowerRight);
        }
        // ㅗ ㅏ
        if (InBounds(GridScript.allTiles, id - rowLength + 1) && (id + 1) % rowLength != 0)
        {
            upperRight = GridScript.allTiles[id - rowLength + 1];
            if (upperRight.isMine)
            {
                displayNumber++;
            }

            neighborTiles.Add(upperRight);
        }
        // ㅜ ㅓ
        if (InBounds(GridScript.allTiles, id + rowLength - 1) && id % rowLength != 0)
        {
            lowerLeft = GridScript.allTiles[id + rowLength - 1];
            if (lowerLeft.isMine)
            {
                displayNumber++;
            }
            neighborTiles.Add(lowerLeft);
        }

        // ㅗ ㅓ
        if (InBounds(GridScript.allTiles, id - rowLength - 1) && id % rowLength != 0)
        {
            upperLeft = GridScript.allTiles[id - rowLength - 1];
            if (upperLeft.isMine)
            {
                displayNumber++;
            }

            neighborTiles.Add(upperLeft);
        }
    }

    public void SetTileNumber()
    {
        //SpriteRenderer[] spriteArray = new SpriteRenderer[2];
        //spriteArray = gameObject.GetComponentsInChildren<SpriteRenderer>();
        

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

        //SpriteRenderer[] spriteArray = new SpriteRenderer[2];
        //spriteArray = gameObject.GetComponentsInChildren<SpriteRenderer>();

        spriteArray[0].sprite = background[x];

        

      


    }

    public void RevealTile()
    {
        isRevealed = true;
        if (isMine)
        {
            Explode();
            //ChainExplosion();
        }
        else
        {

            
            spriteArray[1].enabled = true;

            if(displayNumber == 0)
            {
                RevealNeighborTiles();
            }
        }

    }


    // 0 짜리는 주변으로 퍼져나가면서 밝혀나가야 함.
    void RevealNeighborTiles()
    {
        for(int i=0;i<neighborTiles.Count;i++)
        {

            
            if(!neighborTiles[i].isMine && !neighborTiles[i].isRevealed && neighborTiles[i].displayNumber==0)
            {
                // 옆 타일에 지뢰가 없고, 옆 타일이 밝혀지지 않았고, 옆 타일의 근처에도 지뢰가 없는 경우
                neighborTiles[i].RevealTile();
            }
            else if(!neighborTiles[i].isMine && !neighborTiles[i].isRevealed && neighborTiles[i].displayNumber>0)
            {
                // 옆 타일에 지뢰가 없고, 옆 타일이 밝혀지지 않았고, 옆 타일의 근처에 지뢰가 있는 경우
                neighborTiles[i].RevealNeighborTilesWithNumber();
            }
        }
    }

    // 0짜리가 퍼져나가다가 숫자를 만나면 멈춤.
    void RevealNeighborTilesWithNumber()
    {
        isRevealed = true;
        spriteArray[1].enabled = true;
    }

    void Explode()
    {
        // 임시 점수 저장소
        int tempPoint = 0;

        // 주변부위를 살펴보자
        for (int i = 0; i < neighborTiles.Count; i++)
        {

            // 주변이 존재하는가? 그리고 주변이 이미 터져있는가?
            if (neighborTiles[i] && neighborTiles[i].isExploded==false)
            {
                // 주변부도 폭탄이면 연쇄폭발, StackOverflow 
                
                neighborTiles[i].isExploded = true;
                neighborTiles[i].spriteArray[0].color = Color.red;
                //neighborTiles[i].gameObject.SetActive(false);
                tempPoint += neighborTiles[i].hPoint;
                if (neighborTiles[i].isMine)
                {
                    // 하나씩 넣자
                    if(!GridScript.explosionTiles.Contains(neighborTiles[i]))
                    {                        
                        GridScript.explosionTiles.Enqueue(neighborTiles[i]);
                    }
                }                
            }           
        }

        // 이제 자기 자신도 터트리자

        if (isExploded == false)
        {
            isExploded = true;
            spriteArray[0].color = Color.red;
            tempPoint += hPoint;
        }

        
        //gameObject.SetActive(false);
        // 한번에 합산.
        StageManager.AddHPoint(tempPoint);


        // 아까 저장했던 애들도 다시 한번 봐보자
        while(GridScript.explosionTiles.Count != 0)
        {
            // 하나씩 빼자
            GridScript.explosionTiles.Dequeue().Explode();
        }
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
