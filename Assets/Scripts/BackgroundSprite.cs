using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSprite : MonoBehaviour
{
    public Sprite background;

    public string stageName;
    public int currentDifficulty;

    public void SetupStageInfo( PlayerManager playerManager)
    {



        stageName = playerManager.stageNames[playerManager.currentStage];
        currentDifficulty = playerManager.currentDifficulty;



    }

    public void SetupBackground()
    {
        //print(stageName);
        //print(currentDifficulty);

        background = Resources.Load<Sprite>("Spritesheets/MainScreen/" + stageName + "" + (currentDifficulty+1));
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = background;

        //if (background)
        //{
        //    print(background);
        //    print(stageName);
        //    print(currentDifficulty);

        //}
        //else
        //{
        //    print("NULLLLL");
        //}

        
        float width = background.bounds.size.x;
        float height = background.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;


        // 베이스
        //spriteArray[0].transform.localScale = new Vector3( worldScreenWidth / width,worldScreenHeight/height,1);

        // h * (9/16) * (1/8) , 9/16 = aspect ratio, 1/8 = tile size
        float modifiedHeight = ((worldScreenHeight / height));

        // w * (1/8) , 1/8 = tile size
        float modifiedWidth = (worldScreenWidth / width);

        // 여기서부터!!!

        transform.localScale = new Vector3(modifiedWidth, modifiedHeight, 1);

        //gameObject.transform.position.Set(0, Screen.height / 2, 1);
        

        

    }
}
