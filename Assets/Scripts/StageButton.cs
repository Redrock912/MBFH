using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class StageButton : MonoBehaviour
{
    public int stageID;
    public int difficultyLevel;
    PlayerManager playerManager;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void SetupStageInfo()
    {
        playerManager.currentDifficulty = difficultyLevel;
        
        playerManager.currentStage = stageID;

        






        SceneManager.LoadScene("Main");
    }
}
