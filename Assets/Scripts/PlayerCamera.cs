using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
    public Camera playerCamera;
    // Start is called before the first frame update
    void Start()
    {

        CameraSplitter.Instance.cameras.Add(playerCamera);
    }

    void OnDestroy()
    {
        CameraSplitter.Instance.cameras.Remove(playerCamera);
    }
}
