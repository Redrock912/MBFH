using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{

    public Stage stagePrefab;
    PlayerManager playerManager;
    Button[] buttons;

    public int numberToCreate;


    [Tooltip("Sprite for unselected page (optional)")]
    public Sprite unselectedPage;
    [Tooltip("Sprite for selected page (optional)")]
    public Sprite selectedPage;
    [Tooltip("Container with page images (optional)")]
    public Transform pageSelectionIcons;

    // for showing small page icons
    private bool _showPageSelection;
    private int _previousPageSelectionIndex;
    // container with Image components - one Image for each page
    private List<Image> _pageSelectionImages;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();

        numberToCreate = playerManager.maxStageNumber;

        buttons = GetComponents<Button>();

        Deploy();

        print("CurrentGrid = " + playerManager.currentGrid);


    }

    void Deploy()
    {
        
        Sprite[] stageSprite;

        

        for(int i=0;i<numberToCreate;i++)
        {
            Stage tempObject = Instantiate(stagePrefab, transform) as Stage;
            stageSprite = Resources.LoadAll<Sprite>("Spritesheets/MainScreen/" + playerManager.stageNames[i]);

            print(playerManager.stageNames[i]);
            tempObject.stageID = i;

            tempObject.stageTier = PlayerPrefs.GetInt("stage" + i,0);
            print(tempObject.stageTier);


            //print("저장된 값들은 "+PlayerPrefs.GetInt("stage" + i));
            //tempObject.SetButtons();
            

            // 예외처리...
            if (tempObject.stageTier == 0)
            {
                tempObject.GetComponentInChildren<Image>().sprite = stageSprite[3];
            }
            else
            {
                tempObject.GetComponentInChildren<Image>().sprite = stageSprite[tempObject.stageTier - 1];
            }
            
            
        }

        //// 버튼 높이 조절을 이걸로 할까 수동으로 할까... 그런데 이걸로할거면 다른것도 다 이걸로 하는게 좋지않나. 하나로 통일하자
        //for(int i=0;i<buttons.Length;i++)
        //{
        //    buttons[i]
        //}
    }



    
}
