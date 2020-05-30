using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerPrefab;
    public int demoNumber = 13;

    public TankData Player1;
    public TankData Enemies;

    void Start()
    {
        Player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<TankData>();
        Enemies = GameObject.FindGameObjectWithTag("Enemy").GetComponent<TankData>();
    }
}
