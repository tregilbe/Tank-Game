using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
    private Camera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponent<Camera>();
        CameraSplitter.Instance.cameras.Add(playerCamera);
    }
}
