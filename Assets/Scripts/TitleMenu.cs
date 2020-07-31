using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{ 

    public void StartGame()
    {
        // What happens when the player presses the Start Game Button
        GameManager.Instance.MG.StartGame();
    }

    public void OnClickOptions()
    {
        // What happens when the player presses the options button

    }

    public void OnClickQuit()
    {
        // What happens when the player presses the quit button
        Application.Quit();
    }
}
