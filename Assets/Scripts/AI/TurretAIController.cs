using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAIController : MonoBehaviour
{
    public enum AIPersonality
    {
        Turret
    }
    public enum AIState
    {
        Scan,
        Attack
    }

    public AIPersonality currentPersonality;
    private AIState currentAIState;
    public AIState previousAIState;
    private TankData data;
    private TankMotor motor;
    public Transform target;
    private Transform tf;
    public EnemyController EC;

    public Hearing hearing;
    public Vision vision;

    public float avoidanceTime = 2f;
    private float exitTime;

    public float stateEnterTime;

    private void Start()
    {
        GameManager.Instance.Enemies.Add(this.gameObject.GetComponent<TankData>());

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
        tf = GetComponent<Transform>();
        EC = GetComponent<EnemyController>();
        hearing = GetComponent<Hearing>();
        vision = GetComponent<Vision>();
    }
    private void Update()
    {
        switch (currentPersonality)
        {
            case AIPersonality.Turret:
                TurretTankFSM();
                break;

            default:
                Debug.LogWarning("Unimplemented personality");
                break;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.Enemies.Remove(this.gameObject.GetComponent<TankData>());
    }

    public void ChangeState(AIState newState)
    {
        // Save the previous state.
        previousAIState = currentAIState;

        // Change to new state.
        currentAIState = newState;

        // Save the time we changed states at.
        stateEnterTime = Time.time;
    }

    private void TurretTankFSM()
    {
        switch (currentAIState)
        {
            case AIState.Scan:

                if (vision.CanSee(target.gameObject) == true)
                {
                    currentAIState = AIState.Attack;
                }
                else
                {
                    Scan();
                }
                break;

            case AIState.Attack:

                if (vision.CanSee(target.gameObject) == true)
                {
                    Aim();
                    Shoot();
                }
                else
                {
                    currentAIState = AIState.Scan;
                }

                break;
        }
    }

    void Aim()
    {
        motor.RotateTowards(target.position, data.rotateSpeed);
    }

    void Scan()
    {
        motor.Rotate(25f);
    }

    void Shoot()
    {
        if (data.timeBtwShots <= 0)
        {
            // Instantiate the vullet prefab
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(data.Bullet, data.shotPoint.transform.position, data.shotPoint.transform.rotation) as GameObject;

            // Retrieve the Rigidbody of the bullet 
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            // add force to the bullet
            Temporary_RigidBody.AddForce(transform.forward * data.bulletSpeed);

            // Clean up, set timer till the bullet is destroyed
            Destroy(Temporary_Bullet_Handler, data.bulletLifetime);

            data.timeBtwShots = data.startTimeBtwShots;
        }
        else
        {
            data.timeBtwShots -= Time.deltaTime;
        }
    }
}
