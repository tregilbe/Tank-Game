using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    public List<GameObject> pickupPrefabs;
    public float spawnDelay;
    private float nextSpawnTime;
    private GameObject currentPickup;
    private Transform tf;
    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        nextSpawnTime = Time.time + spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPickup == null) // If we don't have a currently spawned in pickup, we want to spawn a new one in
        {
            if (Time.time > nextSpawnTime) // Once the timer has run down, spawn in a new pickup.
            {
                pickupPrefab = pickupPrefabs[Random.Range(0, pickupPrefabs.Count)]; // Select a random pickup to spawn
                currentPickup = Instantiate(pickupPrefab, tf.position, Quaternion.identity); // Spawn it.
                nextSpawnTime = Time.time + spawnDelay; // Reset the timer.
            }
        }
        else // If we already have a pickup spawned, then we should just reset the timer.
        {
            nextSpawnTime = Time.time + spawnDelay;
        }

    }
}
