using System.Collections;
using System.Collections.Generic;
using ChrisTutorials.Persistent;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip inGameMusic;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayLoop(menuMusic, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
