using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform tf;
    private TankData data;
    public int myScore;
    public int killPoints;

    float timer;
    public int waitingTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        data = gameObject.GetComponent<TankData>();
        myScore = myScore = GetComponent<TankData>().Score;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > waitingTime)
        {
            //Action
            //Shoot();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    void Shoot()
    {
        //GameObject bulletInstance;
        //bulletInstance = 
            Instantiate(data.Bullet, data.shotPoint.position, data.shotPoint.rotation);

        //bulletInstance.GetComponent<Bullet>().Shooter = this.gameObject;

    }

}
