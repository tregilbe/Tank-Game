using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutSpawnPoint : MonoBehaviour
{
    public Transform[] waypoints;

    void Awake()
    {
        GameManager.Instance.scoutSpawnPoints.Add(this.gameObject.GetComponent<ScoutSpawnPoint>());
    }

    void OnDestroy()
    {
        GameManager.Instance.scoutSpawnPoints.Remove(this.gameObject.GetComponent<ScoutSpawnPoint>());
    }
}
