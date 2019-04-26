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
    GridManager gridManager;
    
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        
        playerManager.OnCountOver += OnCountOver;
        playerManager.OnClear += OnClear;

        GameObject clearImageObject = GameObject.FindGameObjectWithTag("ClearImage");

        GameObject countOverImageObject = GameObject.FindGameObjectWithTag("CountOverImage");

        clearImage = clearImageObject.GetComponent<RectTransform>();

        countOverImage = countOverImageObject.GetComponent<RectTransform>();

        gridManager = FindObjectOfType<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {

        // elapsedTime += Time.deltaTime;

        // timeUI.text = (initialTime - elapsedTime).ToString("F1");

        // Temporary ?
        gachaUI.text = (playerManager.count).ToString("D2");

        // 이 부분도 여러 레이어를 가정해서 만들었지만, 현재는 0 사용
        bombUI.text = (gridManager.gridScript[gridManager.CurrentTop].currentMines).ToString("D2");
        
    }

    void OnCountOver()
    {
        print("GameUI OnCountOver");
        if (countOverImage != null)
        {
            StartCoroutine("CountOverImageAnimation");
        }
        
    }

    void OnClear()
    {
        print("GameUI OnClear");

        if(clearImage != null)
        {
            StartCoroutine("ClearImageAnimation");
        }
        
    }

    IEnumerator ClearImageAnimation()
    {
        float speed = 1;
        float animatePercent = 0;
        while (animatePercent < 1)
        {
            animatePercent += Time.deltaTime * speed;
            clearImage.anchoredPosition = Vector2.up * Mathf.Lerp(-750, 0, animatePercent);

            yield return null;
        }

      

        //yield return new WaitForSeconds(0.2f);
    }

    IEnumerator CountOverImageAnimation()
    {
        float speed = 1;
        float animatePercent = 0;
        while (animatePercent < 1)
        {
            animatePercent += Time.deltaTime * speed;
            countOverImage.anchoredPosition = Vector2.up * Mathf.Lerp(-1000, 0, animatePercent);

            yield return null;
        }
        

    }
}
