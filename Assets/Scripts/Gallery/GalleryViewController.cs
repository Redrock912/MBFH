using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GalleryViewController : MonoBehaviour
{
    public PlayerManager playerManager;
    public TextMeshProUGUI galleryText;
    public TextMeshProUGUI stageSelectText;
    public Button modeSwitchButton;

    public SfxLibrary sfxLibraryPrefab;

    public Sprite[] backgroundImageStorages;
    public Image backgroundImage;

    int index = 0;
    
    private void Start()
    {
        playerManager = PlayerManager.Instance;
        print(playerManager.isGalleryMode);
        modeSwitchButton.interactable = true;


        UpdateDisplay();
    }


    // 버튼에 OnClick() 으로 할당할 시에 Prefab을 하지말고 실제로 Scene위에 올라가는 객체를 할당하자. 그래야 Start 함수가 작동해서 별일없이 작동한다. 그리고 Prefab으로 작동시키면 왜인지 모르겠지만 playerManager = PlayerManager.Instance; 를 할 시에 인스턴스를 못 찾아낸다
    public void ChangeMode()
    {

        playerManager.isGalleryMode = !playerManager.isGalleryMode;
        AudioManager.Instance.PlaySound(sfxLibraryPrefab.GetClipFromID(5), new Vector3(0, 0, 0));

        index = 1 - index;
        backgroundImage.sprite = backgroundImageStorages[index];

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        if (playerManager.isGalleryMode)
        {
            galleryText.color = new Color(1, 1, 1, 1f);
            stageSelectText.color = new Color(1, 1, 1, 0.3f);
        }
        else
        {
            galleryText.color = new Color(1, 1, 1, 0.3f);
            stageSelectText.color = new Color(1, 1, 1, 1f);
        }
    }
}
