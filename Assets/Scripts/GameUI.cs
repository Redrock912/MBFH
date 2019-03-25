using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    // Start is called before the first frame update

    public RectTransform clearImage;
    public RectTransform countOverImage;
    public Text gachaUI;
    public Text bombUI;
    public Text heartUI;
    public Text goldUI;

    public float initialTime = 100;
    float elapsedTime = 0f;
    PlayerManager playerManager;
    
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.OnCountOver += OnCountOver;
    }

    // Update is called once per frame
    void Update()
    {

       // elapsedTime += Time.deltaTime;

       // timeUI.text = (initialTime - elapsedTime).ToString("F1");

        
    }

    void OnCountOver()
    {

    }
}
