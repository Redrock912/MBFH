using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    float sfxVolumePercent = 2;
    float musicVolumePercent = 0.5f;
    float masterVolumePercent = 1;


    AudioSource[] musicSources;
    int activeMusicSourceIndex;

    int maxMusicSources =2 ;

    private static AudioManager instance;

    public static AudioManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            musicSources = new AudioSource[maxMusicSources];
            for (int i = 0; i < maxMusicSources; i++)
            {
                GameObject newMusicSource = new GameObject("Music source " + (i + 1));
                musicSources[i] = newMusicSource.AddComponent<AudioSource>();

                newMusicSource.transform.parent = transform;

            }
        }
        
    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1)
    {
        // 사용하는 것과 사용하지 않는것들 끼리 자연스럽게 바뀌게 하기위해 사용
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        //print("current music index is " + activeMusicSourceIndex);


        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].Play();




        StartCoroutine(AnimateMusicCrossFade(fadeDuration));
    }

    public void PlaySound(AudioClip clip , Vector3 pos)
    {
        if(clip != null)
        {
            
            AudioSource.PlayClipAtPoint(clip, new Vector3(0,0,-5), 1);
        }


    }
    //public void PlaySound(string soundName, Vector3 pos)
    //{
    //    if (soundName != null)
    //    {
          
    //    }


    //}

    IEnumerator AnimateMusicCrossFade(float duration)
    {
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);
            musicSources[1-activeMusicSourceIndex].volume = Mathf.Lerp( musicVolumePercent * masterVolumePercent,0, percent);

            yield return null;

        }
        
    }
}
