using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private TankData data;
    private GameObject myPlayer;
    public int killPoints;

    // Start is called before the first frame update
    void Start()
    {
        data = gameObject.GetComponent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (data.timeBtwShots <= 0)
        {                       
            Instantiate(data.Bullet, data.shotPoint.position, transform.rotation);
            data.timeBtwShots = data.startTimeBtwShots;            
        }
        else
        {
            data.timeBtwShots -= Time.deltaTime;
        }
    }

    void OnDestroy()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player");
        myPlayer.GetComponent<TankData>().Score += killPoints;
    }
}
