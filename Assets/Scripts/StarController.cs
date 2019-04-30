﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StarController : MonoBehaviour
{
    public int tier;
    int count = 0;
    Animation anim;

    
    public Image leftStar;
    public Image centerStar;
    public Image rightStar;

    

    private void Awake()
    {
        anim = GetComponent<Animation>();

        
    }



    public void PlayAnimation()
    {
        anim.Play("ShowStars");
    }


    // StageSelect Scene에서 보여주는 용도
    public void DisplayStageSelect()
    {

        // 흠... 뭐 일단 되니까
        switch (tier)
        {
            case 0:
                {
                    break;
                }
            case 1:
                {
                                      
                    leftStar.color = Color.white;
                    break;
                }
            case 2:
                {
                    leftStar.color = Color.white;
                    centerStar.color = Color.white;
                    break;
                }
            case 3:
                {
                    leftStar.color = Color.white;
                    centerStar.color = Color.white;
                    rightStar.color = Color.white;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }


    // StarAnimation 에서 티어별로 계속 체크함
    public void CheckTier()
    {
        // 당신은 얼만큼의 별을 모았는가
        if(count >= tier)
        {
            anim.Stop();
            count = 0;
        }
        else
        {
            print(count);
            count += 1;
        }
    }
}
