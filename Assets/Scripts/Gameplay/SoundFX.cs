using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public AudioClip[] sounds;

    public void Play()
    {
        if(AudioManager.instance && sounds.Lenght>0)
        {
            int r = Random.Range(0, sounds.Lenght);
            AudioMsnsger.instance.PlaySFX(sounds[r]);
        }
    }
}