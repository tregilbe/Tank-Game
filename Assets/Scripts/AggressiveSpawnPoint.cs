using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveSpawnPoint : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.aggressiveSpawnPoints.Add(this.gameObject.GetComponent<AggressiveSpawnPoint>());
    }

    void OnDestroy()
    {
        GameManager.Instance.aggressiveSpawnPoints.Remove(this.gameObject.GetComponent<AggressiveSpawnPoint>());
    }
}
