using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update

    // 여기서는 정보를 넘기지말고 이동만하자

    public void ToMainScene()
    {
        SceneManager.LoadScene("Main");
        
    }

    public void ToStageSelectScene()
    {
        SceneManager.LoadScene("StageSelect");
    }




    

    
    
    void Start()
    {
        //Camera sceneCamera = GetComponent<Camera>();
        //if(sceneCamera != null)
        //{
        //    print(sceneCamera.pixelRect);
        //    print(Screen.height);
        //    print(Screen.width);
        //}
    
    }


}
