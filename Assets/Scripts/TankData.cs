using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 90f;
    public float reverseSpeed = 7.0f;

    public int health;
    public int damage;
    public int Score;

    public GameObject Bullet;
    public Transform shotPoint;

    public float timeBtwShots;
    public float startTimeBtwShots;

    void Start()
    {
        Score = 0;
    }

    void Update()
    {
        // Make sure the objectg is destroyed if health goes below 0
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health -= damage;
        }
    }

}
