using System.Collections;
using System.Collections.Generic;
using ChrisTutorials.Persistent;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : Singleton<SaveManager>
{
    //public float musicVolume = 1.0f;
    //public float fxVolume = 1.0f;

    void Start()
    {
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", GameManager.Instance.musicVolume);
        PlayerPrefs.SetFloat("fxVolume", GameManager.Instance.fxVolume);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            GameManager.Instance.musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            GameManager.Instance.MusicSlider.value = GameManager.Instance.musicVolume;
            //AudioManager.Instance.SetVolume(AudioManager.AudioChannel.Music, (int)(GameManager.Instance.MusicSlider.value * 100));
            AudioManager.Instance.SetVolume(AudioManager.AudioChannel.Music, (int)(GameManager.Instance.musicVolume));
        }
        
        if (PlayerPrefs.HasKey("fxVolume"))
        {
            GameManager.Instance.fxVolume = PlayerPrefs.GetFloat("fxVolume");
            GameManager.Instance.SfxSlider.value = GameManager.Instance.fxVolume;
            AudioManager.Instance.SetVolume(AudioManager.AudioChannel.Sound, (int)(GameManager.Instance.SfxSlider.value * 100));
        }    
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
