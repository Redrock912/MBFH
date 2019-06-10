using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DifficultySwitch : MonoBehaviour
{

    public RectTransform stageContainer;

    public Sprite[] selectedImage;
    public Sprite[] unselectedImage;

    public Image[] buttonContainer;
    PlayerManager playerManager;

    public ScrollSnapRect scrollSnap;

    public Sprite[] colorByDifficulty;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.Instance;
        
        if (playerManager.levelUnlocked < 10)
        {
            buttonContainer[1].GetComponent<Button>().interactable = false;
        }
        SelectDifficulty(0);    
    }


    public void SelectDifficulty(int i)
    {
        int k = 0;
        
        
        scrollSnap.selectedPage = colorByDifficulty[i];
        scrollSnap.SetColorForDifficulty();
        while (k < buttonContainer.Length)
        {
            if (k == i)
            {
                buttonContainer[k].sprite = selectedImage[k];
            }
            else
            {
                buttonContainer[k].sprite = unselectedImage[k];
            }

            k++;
        }

        
        for(int n = 0; n < stageContainer.childCount;  n++)
        {
            stageContainer.GetChild(n).GetComponent<Stage>().SetupStageInfo(i);
        }
       
    }

}
