using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOptionsMenu : Menu
{
    public static AudioOptionsMenu instance = null;

    public Slider masterVolSilder = null;
    public Slider fxVolSilder = null;
    public Slider musicVolSilder = null;

    void Start()
    {
        if(instance)
        {
            Debug.LogError("Trying to create more than one AudioOptionsMenu");
            Destroy(gameObject);
            return;

        }
        instance = this;

        float volume = 1;

        if (PlayerPrefs.HasKEy("MAsterVolume"))
            volume = PlayerPref.GetFloat("MasterVolume");
        masterVolSilder.value = volume;

        if (PlayerPrefs.HasKEy("EffectVolume"))
            volume = PlayerPref.GetFloat("EffectVolume");
        masterVolSilder.value = volume;

        if (PlayerPrefs.HasKEy("MusicVolume"))
            volume = PlayerPref.GetFloat("MusicVolume");
        masterVolSilder.value = volume;

    }

    public void BackButton()
    {
        TurnOff(false);
    }
    public void UpdateMasterVolume(float value)
    {
        float volume = Mathf.Clamp(value, 0.0001f, 1);
        AudioManager.instance.mixer.SetFloat("MasterVolume", Mathf.Log10(volume)*20;

        PlayerPref.SetFloat("MasterVolume", volume);
        PlayerPref.Save();
    }

    public void UpdateSFXVolume(float value)
    {
        float volume = Mathf.Clamp(value, 0.0001f, 1);
        AudioManager.instance.mixer.SetFloat("EffectVolume", Mathf.Log10(volume)*20;

        PlayerPref.SetFloat("MasterVolume", volume);
        PlayerPref.Save();
    }

    public void UpdateMusicVolume(float value)
    {
        float volume = Mathf.Clamp(value, 0.0001f, 1);
        AudioManager.instance.mixer.SetFloat("MusicVolume", Mathf.Log10(volume)*20;

        PlayerPref.SetFloat("MasterVolume", volume);
        PlayerPref.Save();
    }

}
