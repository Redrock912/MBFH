using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public Button[] buttonLists;


    public int unlockedTier=0;

    void Start()
    {


        PlayerManager playerManager = PlayerManager.Instance;

        // 일단 4번은 안씀

        if (playerManager && PlayerPrefs.HasKey("stage" + playerManager.currentStage))
        {
            unlockedTier = PlayerPrefs.GetInt("stage" + playerManager.currentStage);
            print("Tier is : " + unlockedTier);
        }


        
        for(int i = unlockedTier; i < buttonLists.Length; i++)
        {
            buttonLists[i].interactable = false;
        }
    }


}
