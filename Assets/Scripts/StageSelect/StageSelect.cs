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

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();

        numberToCreate = playerManager.maxStageNumber;

        buttons = GetComponents<Button>();

        Deploy();
    }

    void Deploy()
    {
        
        Sprite stageSprite;

        

        for(int i=0;i<numberToCreate;i++)
        {
            Stage tempObject = Instantiate(stagePrefab, transform) as Stage;
            stageSprite = Resources.Load<Sprite>("Spritesheets/sample" + (i+1));

            tempObject.stageID = i;

            tempObject.unlockedDifficulties = PlayerPrefs.GetInt("stage" + i,1);
            //print("저장된 값들은 "+PlayerPrefs.GetInt("stage" + i));
            tempObject.SetButtons();
            tempObject.GetComponent<Image>().sprite= stageSprite;
        }

        //// 버튼 높이 조절을 이걸로 할까 수동으로 할까... 그런데 이걸로할거면 다른것도 다 이걸로 하는게 좋지않나. 하나로 통일하자
        //for(int i=0;i<buttons.Length;i++)
        //{
        //    buttons[i]
        //}
    }

    
}
