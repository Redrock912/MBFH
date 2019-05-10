using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public bool isPaused = false;

    public Transform pauseMenu;

    public void OnClick()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            print(pauseMenu.parent.name);
        }
        else
        {
            Time.timeScale = 1;
            
        }
    }
}
