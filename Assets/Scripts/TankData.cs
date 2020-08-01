using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 90f;
    public float reverseSpeed = 7.0f;

    public float currentHealth;
    public float maxHealth;

    public int damage;
    public int Score;

    public GameObject Bullet;
    public Transform shotPoint;

    public float timeBtwShots;
    public float startTimeBtwShots;
    public float bulletSpeed;
    public float bulletLifetime;

    public int scoreTransfer;

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

        // Make sure the object is destroyed if health goes below 0


        if (this.gameObject.tag == "Enemy")
        {
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (this.gameObject.name == "Player1(Clone)")
        {
            if (currentHealth <= 0)
            {
                if (GameManager.Instance.PlayerOneLives <= 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                    GameManager.Instance.PlayerOneLives -= 1;
                    GameManager.Instance.SpawnPlayerOne();
                }
            }
        }

        if (this.gameObject.name == "Player2(Clone)")
        {
            if (currentHealth <= 0)
            {
                if (GameManager.Instance.PlayerTwoLives <= 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                    GameManager.Instance.PlayerTwoLives -= 1;
                    GameManager.Instance.SpawnPlayerTwo();
                }
            }
        }

        if (GameManager.Instance.PlayerOneLives == 0 && GameManager.Instance.PlayerTwoLives == 0)
        {
            GameManager.Instance.GameOver();
            GameManager.Instance.PlayerOneLives = 3;
            GameManager.Instance.PlayerTwoLives = 3;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            currentHealth -= damage;
            collision.gameObject.GetComponent<TankData>().Score += 1;
        }
    }
}
