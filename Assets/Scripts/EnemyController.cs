using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private TankData data;
    public int myScore;
    public int killPoints;

    // Start is called before the first frame update
    void Start()
    {
        data = gameObject.GetComponent<TankData>();
        myScore = myScore = GetComponent<TankData>().Score;
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
            GameObject bulletInstance;
            bulletInstance = Instantiate(data.Bullet, data.shotPoint.position, data.shotPoint.rotation);

            bulletInstance.GetComponent<Bullet>().Shooter = this.gameObject;
            data.timeBtwShots = data.startTimeBtwShots;
        }
        else
        {
            data.timeBtwShots -= Time.deltaTime;
        }
    }
}
