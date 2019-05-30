using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip mainTheme;
    public AudioClip menuTheme;
    public AudioClip stageTheme;

    public SfxLibrary sfxLibraryPrefab;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(menuTheme, 2);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlayMusic(mainTheme, 3);
        }


        if (Input.GetKeyDown(KeyCode.B))
        {
            AudioManager.Instance.PlaySound(sfxLibraryPrefab.GetClipFromID(0), transform.position);
        }
    }

    public void PlaySfx(int id)
    {
        AudioManager.Instance.PlaySound(sfxLibraryPrefab.GetClipFromID(id), transform.position);
    }
}
