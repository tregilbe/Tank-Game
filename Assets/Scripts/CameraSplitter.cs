using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSplitter : Singleton<CameraSplitter>
{
    public List<Camera> cameras;

    public Camera Player1;
    public Camera Player2;
    // Start is called before the first frame update
    void Start()
    {
        // Handle this in the titlemenu script

        // SetCameraPositions();
    }

    public void SetCameraPositions()
    {
        if (cameras.Count == 1)
        {
            cameras[0].rect = new Rect(0, 0, 1, 1);
        }
        else
        {
            cameras[1].rect = new Rect(0, 0, 1, 0.5f);
            cameras[0].rect = new Rect(0, 0.5f, 1, 0.5f);
            // I am setting each camera as a Player, thi way it can be tracked which is which
            cameras[1] = Player1;
            cameras[0] = Player2;
        }

        // if either cam is destroyed, check which one, then grab the newest cam and set it as that player
        if (Player1 == null)
        {
            cameras[cameras.Count - 1] = Player1;
            cameras[cameras.Count - 1].rect = new Rect(0, 0, 1, 0.5f);           
        }

        if (Player2 == null)
        {
            cameras[cameras.Count - 1] = Player2;
            cameras[cameras.Count - 1].rect = new Rect(0, 0.5f, 1, 0.5f);            
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetCameraPositions();
    }
}
