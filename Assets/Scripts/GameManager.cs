using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    private const int HighScoreTableSize = 3;
    public MapGenerator MG;

    public GameObject playerPrefab;
    public GameObject playerOnePrefab;
    public GameObject playerTwoPrefab;

    public GameObject turretPrefab;
    public GameObject scoutPrefab;
    public GameObject cautiousPrefab;
    public GameObject aggressivePrefab;

    public GameObject ScoutAI;

    public int demoNumber = 13;

    public TankData Player1;
    public TankData Player2;
    public int PlayerOneLives = 3;
    public int PlayerTwoLives = 3;
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

    public Canvas MainMenuCanvas;

    public int numOfPlayers = 1;

    public float musicVolume = 1.0f;
    public float fxVolume = 1.0f;

    public Slider SfxSlider;
    public Slider MusicSlider;

    void Start()
    {
        // Set main menu canvas as enabled, and disable the game over canvas
        MainMenuCanvas.GetComponent<Canvas>().enabled = true;

        // Start game is now done through a button
        // MG.StartGame();

        // enemies now spawn similar to player in the MapGen script
        //SpawnTurret();
        //SpawnAggressive();
        //SpawnScout();
        //SpawnCautious();
    }

    void Update()
    {
        MG = GameObject.FindGameObjectWithTag("MG").GetComponent<MapGenerator>();
        MainMenuCanvas = GameObject.FindGameObjectWithTag("Menu").GetComponent<Canvas>();
        Player1 = GameObject.Find("Player1(Clone)").GetComponent<TankData>();
        Player2 = GameObject.Find("Player2(Clone)").GetComponent<TankData>();
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

        // Sort scores in order
        scores.Sort();
        scores.Reverse();
        // Limit the size of the high score table
        scores = scores.GetRange(index: 0, count: HighScoreTableSize);
    }

    public void ChoosePlayerSpawn()
    {
       
    }

    public void SpawnPlayer()
    {
        currentPlayerSpawnPoint = playerSpawnPoints[Random.Range(0, playerSpawnPoints.Count - 1)]; // Use random.range to select a player spawnpoint

        Instantiate(playerPrefab, currentPlayerSpawnPoint.transform.position, currentPlayerSpawnPoint.transform.rotation);
    }

    public void SpawnPlayerOne()
    {
        currentPlayerSpawnPoint = playerSpawnPoints[Random.Range(0, playerSpawnPoints.Count - 1)]; // Use random.range to select a player spawnpoint

        Instantiate(playerOnePrefab, currentPlayerSpawnPoint.transform.position, currentPlayerSpawnPoint.transform.rotation);
    }

    public void SpawnPlayerTwo()
    {
        currentPlayerSpawnPoint = playerSpawnPoints[Random.Range(0, playerSpawnPoints.Count - 1)]; // Use random.range to select a player spawnpoint

        Instantiate(playerTwoPrefab, currentPlayerSpawnPoint.transform.position, currentPlayerSpawnPoint.transform.rotation);
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

    public void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }
}
