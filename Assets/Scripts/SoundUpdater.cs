using ChrisTutorials.Persistent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUpdater : MonoBehaviour
{
    public AudioManager.AudioChannel channel;
    public Slider slider;


    public void UpdateSoundLevels()
    {
        int sliderValue = (int)(slider.value * 100);

        AudioManager.Instance.SetVolume(channel, sliderValue);
    }
    public void Update()
    {
        UpdateSoundLevels();
    }
}
