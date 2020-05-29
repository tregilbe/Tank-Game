using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject shotPoint;
    public GameObject Bullet;
    public float bulletSpeed;
    public float bulletLifetime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Instantiate the vullet prefab
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, shotPoint.transform.position, shotPoint.transform.rotation) as GameObject;

            // Retrieve the Rigidbody of the bullet 
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            // add force to the bullet
            Temporary_RigidBody.AddForce(transform.forward * bulletSpeed);

            // Clean up, set timer till the bullet is destroyed
            Destroy(Temporary_Bullet_Handler, bulletLifetime);
        }
    }
}
