using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxLibrary : MonoBehaviour
{


    public AudioClip[] sfxList;

    

    public AudioClip GetClipFromID(int id)
    {

        return sfxList[id];
    }

    public void PlayClipFromID(int id)
    {
        AudioManager.Instance.PlaySound(sfxList[id], transform.position);
    }
}
