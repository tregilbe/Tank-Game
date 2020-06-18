using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Rigidbody rb;

    public float bulletSpeed;
    public float bulletLifeTime;

    public GameObject Shooter;

    public GameObject destroyEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        // Destroy bullet after a set amount of time
        Invoke("DestroyBullet", bulletLifeTime);

        // Shooter = GameObject.GetComponentInParent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * bulletSpeed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        //Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
