using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GallerySpriteWithScroll : MonoBehaviour
{
    PlayerManager playerManager;

    public int unlockedTier;
    public Image[] galleryImages;
    Sprite[] gallerySprites;
    public TextMeshProUGUI stageNameTMP;
    string stageName;

    private void Start()
    {
        // Init
        playerManager = PlayerManager.Instance;
        unlockedTier = PlayerPrefs.GetInt("stage" + playerManager.currentStage);
        unlockedTier -= 1;
        stageName = playerManager.stageNames[playerManager.currentStage];

        // 설정
        stageNameTMP.text = stageName;

        gallerySprites = new Sprite[4];
        for (int i = 0; i <= 3; i++)
        {
            gallerySprites[i] = Resources.Load<Sprite>("Spritesheets/MainScreen/" + stageName + "/" + stageName + "_" + i);
        }
        

        for(int i = 0; i < 3; i++)
        {
            if (unlockedTier >= i)
            {
                galleryImages[i].sprite = gallerySprites[2-i];
            }
            else
            {
                galleryImages[i].sprite = gallerySprites[3];
            }
            
        }
    }
}
