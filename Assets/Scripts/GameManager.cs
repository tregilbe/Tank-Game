using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerPrefab;
    public int demoNumber = 13;

    public TankData Player1;
    public TankData Enemies;

    public List<PlayerSpawnPoint> playerSpawnPoints;

    void Start()
    {
        Player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<TankData>();
        Enemies = GameObject.FindGameObjectWithTag("Enemy").GetComponent<TankData>();
    }

    protected override void Awake()
    {
        base.Awake();
        playerSpawnPoints = new List<PlayerSpawnPoint>();
    }

    public void SpawnPlayer()
    {
        // TODO: Write code to spawn the player at a random spawn point.
    }
}
