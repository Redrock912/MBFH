using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallerySprite : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] gallerySprites;
    public string stageName;

    void Start()
    {
        PlayerManager playerManager = PlayerManager.Instance;
        if (playerManager)
        {
            stageName = playerManager.stageNames[playerManager.currentStage];
        }

        // 4 개만 받자
        gallerySprites = new Sprite[4];

        for(int i = 0; i < 4; i++)
        {
            gallerySprites[i] = Resources.Load<Sprite>("Spritesheets/MainScreen/" + stageName +"/" + stageName + "_" + i);
        }
        

        // 비율을 맞춰보자
        float width = gallerySprites[0].bounds.size.x;
        float height = gallerySprites[0].bounds.size.y;
        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        // 가로버젼
        //transform.localScale = new Vector3(worldScreenWidth/width, worldScreenWidth/width, 1);

        // 세로버젼
        transform.localScale = new Vector3(worldScreenHeight / height, worldScreenHeight / height, 1);

        // x1 : x2 = a : 1 ... a= x1/x2
        GetComponent<SpriteRenderer>().sprite = gallerySprites[3];



    }

    public void DisplaySelectedSprite(int i)
    {
        // 0,1,2,b 순으로 저장
        int reversedIndex = 2 - i;
        if(reversedIndex>=0)
        {
            GetComponent<SpriteRenderer>().sprite = gallerySprites[reversedIndex];
        }
        
        
    }


}
