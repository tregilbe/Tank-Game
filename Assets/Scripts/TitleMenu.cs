﻿using System.Collections;
using System.Collections.Generic;
using ChrisTutorials.Persistent;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    public Canvas CanvasObject;

    public GameObject MainMenu;
    public GameObject Options;

    public AudioClip buttonClip;

    public Slider SfxSlider;
    public Slider MusicSlider;

    public void StartGame()
    {
        // What happens when the player presses the Start Game Button
        if (GameManager.Instance.numOfPlayers == 1)
        {
            // Disable the entire canvas (menu)
            CanvasObject.GetComponent<Canvas>().enabled = false;
            // Set player 2 lives to 0
            GameManager.Instance.PlayerTwoLives = 0;
            // Start the game through the game manager
            GameManager.Instance.MG.StartGame();
        }
        else if (GameManager.Instance.numOfPlayers == 2)
        {
            CanvasObject.GetComponent<Canvas>().enabled = false;
            GameManager.Instance.MG.StartGame();
            GameManager.Instance.SpawnPlayerTwo();
            CameraSplitter.Instance.SetCameraPositions();
        }
        else
        {
            Debug.Log("GameManger numOfPlayers not set!");
        }
    }

    public void OnClickOptions()
    {
        // What happens when the player presses the options button
        // Not Needed
    }

    public void OnClickQuit()
    {
        // What happens when the player presses the quit button
        Debug.Log("QUIT!");
        Application.Quit();
    }

     public void OnClickRetry()
    {
        SceneManager.LoadScene("Main");
    }

    public void SinglePlayer()
    {

    }

    public void Multiplayer()
    {
        CanvasObject.GetComponent<Canvas>().enabled = false;
        GameManager.Instance.MG.StartGame();
        GameManager.Instance.SpawnPlayerTwo();
        CameraSplitter.Instance.SetCameraPositions();
    }

    public void SetPlayersToOne()
    {
        GameManager.Instance.numOfPlayers = 1;
    }

    public void SetPlayersToTwo()
    {
        GameManager.Instance.numOfPlayers = 2;
    }

    public void ButtonPress()
    {
        AudioManager.Instance.Play(buttonClip, transform);
    }

    public void UpdateSfxVolume()
    {
        GameManager.Instance.fxVolume = SfxSlider.value;
    }

    public void UpdateMusicVolume()
    {
        GameManager.Instance.musicVolume = MusicSlider.value;
    }
}
