using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{



    Sprite baseTileSprite;
    Sprite tileSprite;
    public Sprite tile1, tile2, tile3, tile4, tile5, tile6, tile7, tile8, questionTile;
    public bool isMine;
    public bool isHidden = false;
    public bool isRevealedThisTurn = false;
    public int displayNumber=0;
    public int id;
    public int rowLength;


    // 애니메이션 용 
    Animation anim;
    Vector3 startScale;
    
    

    //public int hPoint = 3;

    // 입력 최고 권위자
    private PlayerInput playerInput;

    public SpriteRenderer[] spriteArray = new SpriteRenderer[2];
    public CubeController cubeController;



    // 현재 상태는 이것뿐 더 추가된다면 enum이 나 을 듯?
    public bool isRevealed =false;
    public bool isExploded = false;


    public GridScript parentGrid;
    public Tiles upperLeft, upper, upperRight, left, right, lowerLeft, lower, lowerRight;
    public List<Tiles> neighborTiles;

    // 비쥬얼
    public Transform explosionHolder;
    public ParticleSystem explosionEffect;


    private void Awake()
    {
        
        playerInput = GameObject.Find("GridManager").GetComponent<PlayerInput>();
        
        spriteArray = gameObject.GetComponentsInChildren<SpriteRenderer>();
        cubeController = gameObject.GetComponentInChildren<CubeController>();

        anim = GetComponent<Animation>();
        if (anim)
        {
            print(anim);
        }
        else
        {
            print("None");
        }
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
        if(InBounds(parentGrid.allTiles, id + rowLength))
        {
            lower = parentGrid.allTiles[id + rowLength];
            if(lower.isMine)
            {
                displayNumber++;
            }

            neighborTiles.Add(lower);
        }
        // ㅗ
        if (InBounds(parentGrid.allTiles, id - rowLength))
        {
            upper = parentGrid.allTiles[id - rowLength];
            if (upper.isMine)
            {
                displayNumber++;
            }
            neighborTiles.Add(upper);
        }
        // ㅏ
        if (InBounds(parentGrid.allTiles, id + 1) && (id+1)%rowLength != 0)
        {
            right = parentGrid.allTiles[id + 1];
            if (right.isMine)
            {
                displayNumber++;
            }
            neighborTiles.Add(right);
        }
        // ㅓ
        if (InBounds(parentGrid.allTiles, id - 1) && id % rowLength != 0)
        {
            left = parentGrid.allTiles[id - 1 ];
            if (left.isMine)
            {
                displayNumber++;
            }
            neighborTiles.Add(left);
        }
        // ㅜ ㅏ
        if (InBounds(parentGrid.allTiles, id + rowLength + 1) && (id + 1) % rowLength != 0)
        {
            lowerRight = parentGrid.allTiles[id + rowLength + 1];
            if (lowerRight.isMine)
            {
                displayNumber++;
            }

            neighborTiles.Add(lowerRight);
        }
        // ㅗ ㅏ
        if (InBounds(parentGrid.allTiles, id - rowLength + 1) && (id + 1) % rowLength != 0)
        {
            upperRight = parentGrid.allTiles[id - rowLength + 1];
            if (upperRight.isMine)
            {
                displayNumber++;
            }

            neighborTiles.Add(upperRight);
        }
        // ㅜ ㅓ
        if (InBounds(parentGrid.allTiles, id + rowLength - 1) && id % rowLength != 0)
        {
            lowerLeft = parentGrid.allTiles[id + rowLength - 1];
            if (lowerLeft.isMine)
            {
                displayNumber++;
            }
            neighborTiles.Add(lowerLeft);
        }

        // ㅗ ㅓ
        if (InBounds(parentGrid.allTiles, id - rowLength - 1) && id % rowLength != 0)
        {
            upperLeft = parentGrid.allTiles[id - rowLength - 1];
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


        // ? 타일이면 ?로 덮어씌우자.
        if (isHidden)
        {
            spriteArray[1].sprite = questionTile;
            
            
        }

        // 일단 숨겨놓자, isRevealed에 반응하도록
        spriteArray[1].enabled = false;


    }

    public void SetData(string name, Sprite spritePart,int rowLength)
    {
        SetBaseTile(name, spritePart);
        SetTileDisplayInfoSize();
        SetScale(rowLength);
    }

    public void SetBaseTile(string name, Sprite spritePart)
    {


        //baseTileSprite = Resources.Load<Sprite>("Spritesheets/MainScreen/Tile_B");
        
        baseTileSprite = spritePart;

        
        //SpriteRenderer[] spriteArray = new SpriteRenderer[2];
        //spriteArray = gameObject.GetComponentsInChildren<SpriteRenderer>();

        spriteArray[0].sprite = baseTileSprite;


        //// 일단 보류
        //float width = spriteArray[0].sprite.bounds.size.x;
        //float height = spriteArray[0].sprite.bounds.size.y;

        //float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        //float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;


        //// 베이스
        ////spriteArray[0].transform.localScale = new Vector3( worldScreenWidth / width,worldScreenHeight/height,1);

        //// h * (9/16) * (1/8) , 9/16 = aspect ratio, 1/8 = tile size
        //float modifiedHeight = ((worldScreenHeight / height) * (9.0f / 16.0f)) *0.1f;

        //// w * (1/8) , 1/8 = tile size
        //float modifiedWidth = (worldScreenWidth / width) *0.1f;

        //spriteArray[0].transform.localScale = new Vector3(modifiedWidth, modifiedHeight, 1);

        //// 여기 까지 보류

    }

    void SetTileDisplayInfoSize()
    {
        
        tileSprite = Resources.Load<Sprite>("Spritesheets/MainScreen/Tile_1");

        // 기준 점으로 아무나 하나 데려오자
        spriteArray[1].sprite = tileSprite;


        float width = spriteArray[1].sprite.bounds.size.x;
        float height = spriteArray[1].sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;


        // 베이스
        //spriteArray[0].transform.localScale = new Vector3( worldScreenWidth / width,worldScreenHeight/height,1);

        // h * (9/16) * (1/8) , 9/16 = aspect ratio, 1/8 = tile size
        float modifiedHeight = ((worldScreenHeight / height) * (10.0f / 16.0f)) * 0.1f ;

        // w * (1/8) , 1/8 = tile size
        float modifiedWidth = (worldScreenWidth / width) * 0.1f;

        spriteArray[1].transform.localScale = new Vector3(modifiedWidth, modifiedHeight, 1);
    }

    // SpriteArray가 아닌 이 객체의 전체 스케일 조정 
    public void SetScale(int rowLength)
    {
        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;


        // 내가 원하는 길이
        float tempLength = worldScreenWidth / (float)rowLength;

        // 현재 이 녀석의 길이
        MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
        float lengthX = meshRenderer.bounds.size.x;
  

        // 현재 비율
        float currentScaleX = worldScreenWidth / gameObject.transform.localScale.x;
        
        // 비율 공식
        float tempScaleX = (tempLength / lengthX) * currentScaleX;

        float modifier = tempScaleX / currentScaleX;

        Vector3 tempVector = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(tempVector.x * modifier, tempVector.y * modifier, 1);

        // 초기값 저장. 애니메이션 용도.
        startScale = gameObject.transform.localScale;
        
    }


    public void RevealTile()
    {
        isRevealed = true;
        isRevealedThisTurn = true;
        if (isMine)
        {
            //StartCoroutine("ExplosionEffectTimer");
            Explode();
            //ChainExplosion();
            

        }
        else
        {

            
            spriteArray[1].enabled = true;

            if (isHidden)
            {
                isHidden = false;
                SetTileNumber();
                spriteArray[1].enabled = true;
                
            }

            if(displayNumber == 0)
            {
                RevealNeighborTiles();
                spriteArray[0].color = new Color(1, 1, 1, 0.3f);
                spriteArray[1].sprite = null;
                cubeController.hideCube();
                
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
        if(!isHidden)
        isRevealed = true;

        isRevealedThisTurn = true;

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
            if (neighborTiles[i] && neighborTiles[i].isMine == false)
            {
                // 주변부도 폭탄이면 연쇄폭발, StackOverflow 
                
                //neighborTiles[i].isExploded = true;
                neighborTiles[i].displayNumber -= 1;

                
                if(neighborTiles[i].displayNumber == 0)
                {
                    //neighborTiles[i].spriteArray[0].color = new Color(1,1,1,0.3f);
                    //neighborTiles[i].spriteArray[1].sprite = null;
                    neighborTiles[i].anim.Play("TileExplosion");
                    neighborTiles[i].cubeController.hideCube();
                }
                else
                {
                    neighborTiles[i].SetTileNumber();
                    neighborTiles[i].RevealNeighborTilesWithNumber();
                }

                //neighborTiles[i].gameObject.SetActive(false);
                //tempPoint += neighborTiles[i].hPoint;
                //if (neighborTiles[i].isMine)
                //{
                //    // 하나씩 넣자
                //    if(!GridScript.explosionTiles.Contains(neighborTiles[i]))
                //    {                        
                //        GridScript.explosionTiles.Enqueue(neighborTiles[i]);
                //    }
                //}                
            }           
        }

        // 이제 자기 자신도 터트리자

        if (isExploded == false)
        {
            isExploded = true;
            //spriteArray[0].color = new Color(1,1,1,0.3f);
            //spriteArray[1].sprite = null;
            cubeController.hideCube();
            //tempPoint += hPoint;
            anim.Play("TileExplosion");
        }


        //gameObject.SetActive(false);
        // 한번에 합산.
        //StageManager.AddHPoint(tempPoint,parentGrid);


        // 아까 저장했던 애들도 다시 한번 봐보자
        //while(GridScript.explosionTiles.Count != 0)
        //{
        //    // 하나씩 빼자
        //    GridScript.explosionTiles.Dequeue().Explode();
        //}

        parentGrid.currentMines -= 1;

        // 이거 분명 나중에 이펙트랑 같이할 때 에러가 터질 것 같이 생겼다.
        //if(parentGrid.currentMines == 0)
        //{
        //    // 나머지도 다 없애자 . 깔-끔
        //    parentGrid.DestroyAllTiles();
        //}
        
    }


    //private void OnMouseOver()
    //{
    //    playerInput.OnMouseOver(this);
    //}
    
    public void SetParentGrid(GridScript gridScript)
    {
        parentGrid = gridScript;
    }


    public void Flip()
    {
        // 다시 가려
        spriteArray[1].enabled = false;
        isRevealed = false;
        
        
    }


    // 비쥬얼

    IEnumerator ExplosionEffectTimer()
    {


        // 왜 1 초 안기다리니....
        anim.Play("TileExplosion");

        if (!anim.isPlaying)
        {

        }

       
        yield return null;
        CreateExplosionEffect();

    }

    void CreateExplosionEffect()
    {
        //ParticleSystem particleSystem = Instantiate(explosionEffect, explosionHolder);
        anim.Play("TileExplosion");
    }

    void AfterExplosionAnimation()
    {
        

        spriteArray[0].color = new Color(1, 1, 1, 0.3f);
        spriteArray[1].sprite = null;

        // 으악 애니메이션이 플레이 중일때는 애니메이션 localScale 값을 덮기 때문에 아래 식이 먹히지 않는다. 멈춰 주도록 하자
        anim.Stop();
        gameObject.transform.localScale = new Vector3(startScale.x, startScale.y, startScale.z);

        if(parentGrid.isHidden)
        {
            setHidden();
        }



    }

    IEnumerator ExplosionAnimationTimer()
    {
        yield return new WaitForSeconds(2.0f);
    }

    public void setHidden()
    {
        spriteArray[0].color = new Color(1, 1, 1, 0);
        spriteArray[1].sprite = null;
        cubeController.hideCube();
    }



    public void SetScaleValue()
    {

        
        print(startScale);
    }

}
