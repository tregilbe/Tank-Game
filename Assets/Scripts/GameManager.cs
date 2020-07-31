using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public MapGenerator MG;

    public GameObject playerPrefab;
    public GameObject turretPrefab;
    public GameObject scoutPrefab;
    public GameObject cautiousPrefab;
    public GameObject aggressivePrefab;

    public GameObject ScoutAI;

    public int demoNumber = 13;

    public TankData Player1;
    //public TankData Enemies;

    public List<TankData> Enemies;

    public List<PlayerSpawnPoint> playerSpawnPoints;
    public PlayerSpawnPoint currentPlayerSpawnPoint;

    public List<TurretSpawnPoint> turretSpawnPoints;
    public TurretSpawnPoint currentTurretSpawnPoint;

    public List<ScoutSpawnPoint> scoutSpawnPoints;
    public ScoutSpawnPoint currentScoutSpawnPoint;

    public List<CautiousSpawnPoint> cautiousSpawnPoints;
    public CautiousSpawnPoint currentCautiousSpawnPoint;

    public List<AggressiveSpawnPoint> aggressiveSpawnPoints;
    public AggressiveSpawnPoint currentAggressiveSpawnPoint;

    public List<Pickup> activePickups;

    public List<ScoreData> scores = new List<ScoreData>();

    void Start()
    {
        MG = GameObject.FindGameObjectWithTag("MG").GetComponent<MapGenerator>();

        // Start game is now done through a button
        // MG.StartGame();

        // enemies now spawn similar to player in the MapGen script
        //SpawnTurret();
        //SpawnAggressive();
        //SpawnScout();
        //SpawnCautious();

        Player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<TankData>();
        //Enemies = GameObject.FindGameObjectWithTag("Enemy").GetComponent<TankData>();
    }

    void Update()
    {

    }

    protected override void Awake()
    {
        base.Awake();
        playerSpawnPoints = new List<PlayerSpawnPoint>();
        turretSpawnPoints = new List<TurretSpawnPoint>();
        scoutSpawnPoints = new List<ScoutSpawnPoint>();
        cautiousSpawnPoints = new List<CautiousSpawnPoint>();
        aggressiveSpawnPoints = new List<AggressiveSpawnPoint>();
        activePickups = new List<Pickup>();
    }

    public void ChoosePlayerSpawn()
    {
       
    }

    public void SpawnPlayer()
    {
        currentPlayerSpawnPoint = playerSpawnPoints[Random.Range(0, playerSpawnPoints.Count - 1)]; // Use random.range to select a player spawnpoint

        Instantiate(playerPrefab, currentPlayerSpawnPoint.transform.position, currentPlayerSpawnPoint.transform.rotation);
    }

    public void SpawnTurret()
    {
        currentTurretSpawnPoint = turretSpawnPoints[Random.Range(0, turretSpawnPoints.Count - 1)];

        Instantiate(turretPrefab, currentTurretSpawnPoint.transform.position, currentTurretSpawnPoint.transform.rotation);
    }

    public void SpawnAggressive()
    {
        currentAggressiveSpawnPoint = aggressiveSpawnPoints[Random.Range(0, aggressiveSpawnPoints.Count - 1)];

        Instantiate(aggressivePrefab, currentAggressiveSpawnPoint.transform.position, currentAggressiveSpawnPoint.transform.rotation);
    }

    public void SpawnScout()
    {
        currentScoutSpawnPoint = scoutSpawnPoints[Random.Range(0, scoutSpawnPoints.Count - 1)];       

        Instantiate(scoutPrefab, currentScoutSpawnPoint.transform.position, currentScoutSpawnPoint.transform.rotation);       
    }

    public void SpawnCautious()
    {
        currentCautiousSpawnPoint = cautiousSpawnPoints[Random.Range(0, cautiousSpawnPoints.Count - 1)];

        Instantiate(cautiousPrefab, currentCautiousSpawnPoint.transform.position, currentCautiousSpawnPoint.transform.rotation);
    }
}
