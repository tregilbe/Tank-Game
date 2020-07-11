using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CautiousSpawnPoint : MonoBehaviour
{
    public Transform[] waypoints;

    void Awake()
    {
        GameManager.Instance.cautiousSpawnPoints.Add(this.gameObject.GetComponent<CautiousSpawnPoint>());
    }

    void OnDestroy()
    {
        GameManager.Instance.cautiousSpawnPoints.Remove(this.gameObject.GetComponent<CautiousSpawnPoint>());
    }
}
