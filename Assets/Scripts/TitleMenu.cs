using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    public Canvas CanvasObject;

    public void StartGame()
    {
        // What happens when the player presses the Start Game Button
        // Disable the entire canvas (menu)
        CanvasObject.GetComponent<Canvas>().enabled = false;
        // Start the game through the game manager
        GameManager.Instance.MG.StartGame();
        // Set Cameras
        CameraSplitter.Instance.SetCameraPositions();
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
}
