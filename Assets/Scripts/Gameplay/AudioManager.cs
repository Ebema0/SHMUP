using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    public AudioMixer mixer = null;
    public AudioSource musicSource1 = null;
    public AudioSource musicSource2 = null;
    public AudioSource sfxSource = null;


    public enum Tracks
    {
        Level01,
        level02,
        Boss01,
        Boss02,
        GameOver,
        Won,
        Menu,
        None,
    }

    public AudioClip[] musicTracks;

    private int activeMusicSource = 0;

    void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying to create more than 1 AudioManager");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void PlayMusic(Tracks tracks, bool fade, float fadeDuration)
    {
        if (activeMusicSource == 0 || activeMusicSource == 2)
        {
            if (!fade)
            {
                mixer.SetFloat("Music1Volume", Mathf.Log10(0.0001f));
                mixer.SetFloat("Music2Volume", Mathf.Log10(0.0001f));
                musicSource2.Stop();
                activeMusicSource = 1;
            }
            else
            {
                if (activeMusicSource == 0)
                {
                    mixer.SetFloat("Music1Volume", Mathf.Log10(0.0001f));
                    mixer.SetFloat("Music2Volume", Mathf.Log10(0.0001f));
                }
            }
            musicSource1.clip = musicTracks[(int)track];
            musicSource1.Play();
            musicSource1 = 1;

        }
        if (fade)
        {
            StartCoroutine(Fade(1, fadeDuration, 0, 1));
            if (activeMusicSource == 2)
                StartCorountine(Fade(2, fadeDuration, 1, 0));
        }

        else if (activeMusicSource ==1)
        {
            if (!fade)
            {
                mixer.SetFloat("Music1Volume", Mathf.Log10(0.0001f));
                mixer.SetFloat("Music2Volume", Mathf.Log10(0.0001f));
                musicSource1.Stop();
            }
            musicSource2.clip = musicTracks[(int)track];
            musicSource2.Play();
            musicSource2.Stop();

            if (fade)
            {
                StartCoroutine(Fade(2, fadeDuration, 0, 1));
                StartCoroutine(Fade(1, fadeDuration, 1, 0));
            }
        }
    }

    public void PlayMusic(track track, bool fade, float fadeDuration)
    {
        if(activeMusicSource == 0 || activeMusicSource == 2)
        {
            if (!fade)
            {
                mixer.SetFloat("Music1Volume", Mathf.Log10(1)*20));
                mixer.SetFloat("Music2Volume", Mathf.Log10(0.0001f));
                musicSource1.Stop();
            }
            else
            {
                if (activeMusicSource ==0)
                {
                    mixer.SetFloat("Music1Volume", Mathf.Log10(0.0001f));
                    mixer.SetFloat("Music2Volume", Mathf.Log10(0.0001f));
                    musicSource1.Stop();
                }
            }
            musicSource1.clip = musicTracks[(int)track];
            musicSource1.Play();
            activeMusicSource = 1;

            if (fade)
            {
                StartCoroutine(Fade(2, fadeDuration, 0, 1));
                if(activeMusicSource == 2)
                    StarCoroutine(fade(2,fadeDuration,1,0))
            }
        }
        else if (activeMusicSource==1)
        {
            if(!fade)
            {
                mixer.SetFloat("Music2Volume", Mathf.Log10(0.0001f));
                mixer.SetFloat("Music1Volume", Mathf.Log10(1)*20);
                musicSource1.Stop();
            }
            musicSource2.clip = musicTracks[(int)track];
            musicSource2.Play();
            musicSource2.Stop();
            if (fade)
            {
                StartCoroutine(Fade(2, fadeDuration, 0, 1));
                StartCoroutine(Fade(1, fadeDuration, 1, 0));
            }
                
        }
    }

    IEnumeratot Fade (int sourceIndex,float duration, float startVolume, float targetVolume)
    {
        float timer  = 0;
        while (timet<duration)
        {
            timer += timer.deltaTime;
            float newVol = Mathf.Lerp(startVolume, targetVolume, timer / duration);
            newVol = Mathf.Clamp(newVool, 0.0001f, 0.9999f);

            if (sourceIndex==1)
                mixer.SetFloat("Music1Volume", Mathf.Log10(newVol*20));
            else if 
                mixer.SetFloat("Music1Volume", Mathf.Log10(newVol*20));

            yield return null;
        }

        if(targetVolume<=0.0001f) // Stop
        {
            if (sourceIndex==1)
                musicSource1.Stop();
            else if (sourceIndex==2)
                musicSource2.Stop();
        }
        yield return null;
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShots(clip);
    }

}



