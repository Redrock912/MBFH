using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static PlayerManager instance;
    // 골드는 표시용
    int gold;

    // 카운트는 게임 로직용
    int count;

    // Game Over~
    public event System.Action OnCountOver;

    GridManager gridManager;
    

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            gridManager = FindObjectOfType<GridManager>();
            //OnCountOver = null;
            
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountOver()
    {
        if (OnCountOver != null)
        {
            OnCountOver();
        }

        // 일단은 부셔볼까
        GameObject.Destroy(gameObject);
    }
}
