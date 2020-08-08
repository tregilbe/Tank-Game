using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSplitter : Singleton<CameraSplitter>
{
    public List<Camera> cameras;

    // Start is called before the first frame update
    void Start()
    {
        // Handle this in the titlemenu script

        // SetCameraPositions();
    }

    public void SetCameraPositions()
    {
        //if (cameras.Count == 1)
        //{
            //cameras[0].rect = new Rect(0, 0, 1, 1);
        //}
        //else
        //{
            //cameras[1].rect = new Rect(0, 0, 1, 0.5f);
            //cameras[0].rect = new Rect(0, 0.5f, 1, 0.5f);
            // I am setting each camera as a Player, thi way it can be tracked which is which
            //cameras[1] = Player1;
            //cameras[0] = Player2;
        //}

        if (GameManager.Instance.numOfPlayers == 1)
        {
            cameras[0].rect = new Rect(0, 0, 1, 1);

            if (GameManager.Instance.Player1 == null)
            {
                cameras[cameras.Count - 1].rect = new Rect(0, 0, 1, 1);
            }
        }

         if (GameManager.Instance.numOfPlayers == 2)
        {
            // I am setting each camera as a Player, this way it can be tracked which is which


            cameras[1].rect = new Rect(0, 0, 1, 0.5f);
            cameras[0].rect = new Rect(0, 0.5f, 1, 0.5f);

            //if either cam is destroyed, check which one, then grab the newest cam and set it as that player
            if (GameManager.Instance.Player1 == null)
            {
                 cameras[cameras.Count - 1].rect = new Rect(0, 0, 1, 0.5f);           
            }

            if (GameManager.Instance.Player2 == null)
            {
                cameras[cameras.Count - 1].rect = new Rect(0, 0.5f, 1, 0.5f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetCameraPositions();
    }
}
