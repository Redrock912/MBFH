using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    float sfxVolumePercent = 1;
    float musicVolumePercent = 0.5f;
    float masterVolumePercent = 1;


    AudioSource[] musicSources;
    int activeMusicSourceIndex;


    public static AudioManager instance;

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
            musicSources = new AudioSource[2];
            for (int i = 0; i < 2; i++)
            {
                GameObject newMusicSource = new GameObject("Music source " + (i + 1));
                musicSources[i] = newMusicSource.AddComponent<AudioSource>();

                newMusicSource.transform.parent = transform;

            }
        }
        
    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1)
    {
        // 2 개인경우에만 이렇ㄱ ㅔ사용
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].Play();

        StartCoroutine(AnimateMusicCrossFade(fadeDuration));
    }

    public void PlaySound(AudioClip clip , Vector3 pos)
    {
        if(clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumePercent);
        }


    }
    public void PlaySound(string soundName, Vector3 pos)
    {
        if (soundName != null)
        {
          
        }


    }

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
