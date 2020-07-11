using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawnPoint : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.turretSpawnPoints.Add(this.gameObject.GetComponent<TurretSpawnPoint>());
    }

    void OnDestroy()
    {
        GameManager.Instance.turretSpawnPoints.Remove(this.gameObject.GetComponent<TurretSpawnPoint>());
    }
}
