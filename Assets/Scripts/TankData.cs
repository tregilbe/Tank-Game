using System.Collections;
using System.Collections.Generic;
using ChrisTutorials.Persistent;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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

    public GameObject closest;

    
    public AudioClip tankDeath;
    public AudioClip bulletHit;

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
                AudioManager.Instance.Play(tankDeath, transform);
                Destroy(gameObject);
            }
        }

        if (this.gameObject.name == "Player1(Clone)")
        {
            if (currentHealth <= 0)
            {
                if (GameManager.Instance.PlayerOneLives <= 0)
                {
                    AudioManager.Instance.Play(tankDeath, transform);
                    Destroy(gameObject);
                }
                else
                {
                    AudioManager.Instance.Play(tankDeath, transform);
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
                    AudioManager.Instance.Play(tankDeath, transform);
                    Destroy(gameObject);
                }
                else
                {
                    AudioManager.Instance.Play(tankDeath, transform);
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
            AudioManager.Instance.Play(bulletHit, transform);
            currentHealth -= damage;
        }
    }

    public GameObject FindClosestPlayer()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public void OnDestroy()
    {
        if (this.gameObject.tag == "Enemy")
        {
            FindClosestPlayer().GetComponent<TankData>().Score += 10;
        }
    }
}
