using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class StageButton : MonoBehaviour
{
    public int stageID;
    public int difficultyLevel;
    PlayerManager playerManager;

    public AudioClip mainMusic;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void SetupStageInfo()
    {
        playerManager.currentDifficulty = difficultyLevel;
        
        playerManager.currentStage = stageID;




        AudioManager.Instance.PlayMusic(mainMusic, 2);

        


        if (playerManager.isGalleryMode)
        {
            SceneManager.LoadScene("Gallery");
        }
        else
        {
            SceneManager.LoadScene("Main");
        }
        
    }
}
