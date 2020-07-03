using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 90f;
    public float reverseSpeed = 7.0f;

    public float currentHealth;
    //public float health;
    public float maxHealth;
    public int damage;
    public int Score;

    public GameObject Bullet;
    public Transform shotPoint;

    public float timeBtwShots;
    public float startTimeBtwShots;
    public float bulletSpeed;
    public float bulletLifetime;

    void Start()
    {
        Score = 0;
        maxHealth = 100;
        currentHealth = 100;
    }

    void Update()
    {
        // Make sure the current health is never about the max
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Make sure the objectg is destroyed if health goes below 0
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            currentHealth -= damage;
        }
    }
}
