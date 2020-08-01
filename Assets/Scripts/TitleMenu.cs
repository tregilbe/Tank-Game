using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public Canvas CanvasObject;

    public void StartGame()
    {
        // What happens when the player presses the Start Game Button
        // Disable the entire canvas (menu)
        CanvasObject.GetComponent<Canvas>().enabled = false;
        // Set player 2 lives to 0
        GameManager.Instance.PlayerTwoLives = 0;
        // Start the game through the game manager
        GameManager.Instance.MG.StartGame();
    }

    public void OnClickOptions()
    {
        // What happens when the player presses the options button
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
}
