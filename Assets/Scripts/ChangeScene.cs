using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update



    public void SceneChange()
    {
        SceneManager.LoadScene(1);
    }
    
    void Start()
    {
        Camera sceneCamera = GetComponent<Camera>();
        if(sceneCamera != null)
        {
            print(sceneCamera.pixelRect);
            print(Screen.height);
            print(Screen.width);
        }
    
    }


}
